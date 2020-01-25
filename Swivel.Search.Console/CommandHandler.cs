using Swivel.Search.Common;
using Swivel.Search.Model.Domain;
using Swivel.Search.Service.Interface;
using System;

namespace Swivel.Search.ConsoleApp
{
    public class CommandHandler
    {
        internal static void HandleDefaultCommand()
        {
            Console.WriteLine(TextResource.NOT_SUPPORTED);
            Console.ReadLine();
            Console.Clear();
        }

        internal static void HandleEnterCommand()
        {
            Console.Clear();
        }

        internal static void HandlerQuitCommand()
        {
            Environment.Exit(1);
        }

        internal static void HandleSearchUsersCommand(IRenderService renderService, IUserService userService, Model.Domain.CommandOutput command)
        {
            var output = userService.Search(command.Param1, command.Param2);
            Console.Clear();
            renderService.Render(output, RenderType.USER);
            Console.ReadLine();
            Console.Clear();
        }

        internal static void HandleSearchTicketsCommand(IRenderService renderService, ITicketService ticketService, CommandOutput command)
        {
            var output = ticketService.Search(command.Param1, command.Param2);
            Console.Clear();
            renderService.Render(output, RenderType.TICKET);
            Console.ReadLine();
            Console.Clear();
        }

        internal static void HandleSearchOrganizationsCommand(IRenderService renderService, IOrganizationService orgService, CommandOutput command)
        {
            var output = orgService.Search(command.Param1, command.Param2);
            Console.Clear();
            renderService.Render(output, RenderType.ORGANIZATION);
            Console.ReadLine();
            Console.Clear();
        }

        internal static void HandleViewFieldsCommand(IRenderService renderService, IUserService userService, IOrganizationService orgService, ITicketService ticketService, CommandOutput command)
        {
            var userOutput = userService.GetSearchOptions();
            var orgOutput = orgService.GetSearchOptions();
            var ticketOutput = ticketService.GetSearchOptions();

            Console.Clear();
            renderService.Render(userOutput, RenderType.USER);
            renderService.Render(orgOutput, RenderType.ORGANIZATION);
            renderService.Render(ticketOutput, RenderType.TICKET);
            Console.ReadLine();
            Console.Clear();
        }
    }
}
