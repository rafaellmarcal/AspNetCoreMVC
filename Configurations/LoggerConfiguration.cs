using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Text;

namespace AspNetCoreMVC.Configurations
{
    public static class LoggerConfiguration
    {
        public static IServiceCollection AddLoggerConfiguration(this IServiceCollection services)
        {
            services.AddLogging(logging =>
            {
                logging.AddKissLog();
            });

            return services;
        }

        public static IApplicationBuilder UseLoggerConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseKissLogMiddleware(options =>
            {
                ConfigureKissLog(options, configuration);
            });

            return app;
        }

        private static void ConfigureKissLog(IOptionsBuilder options, IConfiguration configuration)
        {
            // optional KissLog configuration
            options.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is System.NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }

                    return sb.ToString();
                });

            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            RegisterKissLogListeners(options, configuration);
        }

        private static void RegisterKissLogListeners(IOptionsBuilder options, IConfiguration configuration)
        {
            // multiple listeners can be registered using options.Listeners.Add() method

            options.Listeners.Add(
                new RequestLogsApiListener(
                    new Application(
                        configuration["KissLog.OrganizationId"],    //  "fc6127b1-f33c-45d0-8e23-8cf193bb21df"
                        configuration["KissLog.ApplicationId"])     //  "b9c69601-205a-49e6-aa58-752779ec5d2c"
                )
                {
                    ApiUrl = configuration["KissLog.ApiUrl"]    //  "https://api.kisslog.net"
                }
            );
        }
    }
}
