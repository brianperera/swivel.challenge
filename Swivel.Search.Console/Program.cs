using Microsoft.Extensions.DependencyInjection;
using Swivel.Search.Common;
using Swivel.Search.Service.Interface;
using System;

namespace Swivel.Search.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(TextResource.LOADING);

            Startup.InitCoreSupportModules();
            var serviceCollection = new ServiceCollection();
            Startup.ConfigureServices(serviceCollection); //Dependency inject services
            Startup.ConfigureRepositories(serviceCollection); //Dependency inject repos
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var renderService = serviceProvider.GetService<IRenderService>();
            var userService = serviceProvider.GetService<IUserService>();
            var orgService = serviceProvider.GetService<IOrganizationService>();
            var ticketService = serviceProvider.GetService<ITicketService>();

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
