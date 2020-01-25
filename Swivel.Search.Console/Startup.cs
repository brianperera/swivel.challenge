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
        private static INotificationService _notificationService;

        internal static void InitCoreSupportModules(INotificationService notificationService)
        {
            //Notifications
            _notificationService = notificationService;

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
                    .AddSingleton<IUIService, UIService>()
                    .AddSingleton<INotificationService, MockNotificationService>(); //We will setup a mock notification service
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
            Console.WriteLine($"{TextResource.UNEXPECTED_ERROR_OCCURED}. {TextResource.PRESS_ENTER_CONTINUE}");
            _notificationService.InternalEmail(TextResource.UNEXPECTED_ERROR_OCCURED, e.ExceptionObject.ToString());
            Console.ReadLine();
            Environment.Exit(1);
        }

        #endregion
    }
}
