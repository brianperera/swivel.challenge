using Microsoft.Extensions.DependencyInjection;
using Swivel.Search.Common;
using Swivel.Search.Service.Interface;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Swivel.Search.ConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(TextResource.LOADING);

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(Constants.AppSettingFile, false)
            .Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(configuration.GetSection(Constants.AppSettingsSection));

            //Configure DI container
            Startup.ConfigureServices(serviceCollection);
            Startup.ConfigureRepositories(serviceCollection);
            
            //Get service from DI container
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var notificationService = serviceProvider.GetService<INotificationService>();
            var renderService = serviceProvider.GetService<IUIService>();
            var userService = serviceProvider.GetService<IUserService>();
            var orgService = serviceProvider.GetService<IOrganizationService>();
            var ticketService = serviceProvider.GetService<ITicketService>();
            
            Startup.InitCoreSupportModules(notificationService);

            Console.Clear(); //Loading complete

            bool exitSession = false;

            do
            {
                var command = renderService.ResolveCommand();

                switch (command.Command)
                {
                    case Commands.SEARCH_USERS:
                        CommandHandler.HandleSearchUsersCommand(renderService, userService, command);
                        break;
                    case Commands.SEARCH_TICKETS:
                        CommandHandler.HandleSearchTicketsCommand(renderService, ticketService, command);
                        break;
                    case Commands.SEARCH_ORGANIZATIONS:
                        CommandHandler.HandleSearchOrganizationsCommand(renderService, orgService, command);
                        break;
                    case Commands.VIEW_FIELDS_ALL:
                        CommandHandler.HandleViewFieldsCommand(renderService, userService, orgService, ticketService, command);
                        break;
                    case Commands.QUIT:
                        CommandHandler.HandlerQuitCommand();
                        break;
                    case Commands.ENTER:
                        CommandHandler.HandleEnterCommand();
                        break;
                    default:
                        CommandHandler.HandleDefaultCommand();
                        break;
                }

            } while (!exitSession);
        }
    }
}
