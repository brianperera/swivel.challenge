using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swivel.Search.Common;
using Swivel.Search.Data;
using Swivel.Search.Repo;
using Swivel.Search.Repo.Interface;
using Swivel.Search.Service;
using Swivel.Search.Service.Interface;
using System;

namespace Swivel.Search.ConsoleApp
{
    public static class Startup
    {
        internal static void InitCoreSupportModules()
        {
            //Logging
            Log.Logger = new LoggerConfiguration().WriteTo.File(Constants.LOG_PATH).CreateLogger();

            //Global exception handling
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
        }

        internal static void ConfigureServices(IServiceCollection services)
        {
            SetDatasource(services);

            services.AddLogging(configure => configure.AddSerilog())
                    .AddSingleton<IUserService, UserService>()
                    .AddSingleton<IOrganizationService, OrganizationService>()
                    .AddSingleton<ITicketService, TicketService>()
                    .AddSingleton<IRenderService, RenderService>();
        }

        internal static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddSerilog())
                    .AddSingleton<IUserRepo, UserRepo>()
                    .AddSingleton<IOrganizationRepo, OrganizationRepo>()
                    .AddSingleton<ITicketRepo, TicketRepo>();
        }

        #region Helpers

        private static void SetDatasource(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddSerilog())
                    .AddSingleton<IDataStore, JsonStore>();
        }

        private static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine($"{TextResource.UNEXPECTED_ERROR_OCCURED}. {TextResource.PRESS_ENTER_CONTINUE}");

            //TODO: Trigger email to notify dev team.
            Console.ReadLine();
            Environment.Exit(1);
        }

        #endregion
    }
}
