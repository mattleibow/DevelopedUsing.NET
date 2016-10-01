using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;

namespace DevelopedUsingDotNet
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			if (env.IsDevelopment())
			{
				builder.AddApplicationInsightsSettings(developerMode: true);
			}

			builder.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddApplicationInsightsTelemetry(Configuration);
			services.AddMvc();
			services.AddSwaggerGen();
			services.ConfigureSwaggerGen(options =>
			{
				options.SingleApiVersion(new Info
				{
					Version = "v1",
					Title = "Developed Using .NET API",
				});
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseApplicationInsightsRequestTelemetry();

			if (env.IsDevelopment())
			{
				// dev exceptions
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				// pretty exceptions
				app.UseExceptionHandler("/Home/Error");
			}

			// errors (such a 404)
			app.UseStatusCodePagesWithReExecute("/Home/Error", "?code={0}");

			// specific case for json requests
			app.UseStatusCodePages(new Func<StatusCodeContext, Task>(async context =>
			{
				if (context.HttpContext.Request.ContentType == "application/json")
				{
					var code = (HttpStatusCode)context.HttpContext.Response.StatusCode;
					var msg = Encoding.UTF8.GetBytes(code.ToString());
					await context.HttpContext.Response.Body.WriteAsync(msg, 0, msg.Length);
				}
				else
				{
					await context.Next(context.HttpContext);
				}
			}));
			app.UseExceptionHandler(new ExceptionHandlerOptions
			{
				ExceptionHandler = async context =>
				{
					if (context.Request.ContentType == "application/json")
					{
						context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
						var msg = Encoding.UTF8.GetBytes("InternalServerError");
						await context.Response.Body.WriteAsync(msg, 0, msg.Length);
					}
				}
			});

			app.UseApplicationInsightsExceptionTelemetry();

			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			app.UseSwagger();
			app.UseSwaggerUi();
		}
	}
}
