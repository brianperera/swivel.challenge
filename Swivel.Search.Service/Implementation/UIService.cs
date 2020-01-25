using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swivel.Search.Common;
using Swivel.Search.Model.Domain;
using Swivel.Search.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Swivel.Search.Service
{
    public class UIService : BaseService, IUIService
    {
        public UIService(IOptions<AppSettings> settings, ILogger<UIService> logger): base(logger, settings)
        {
        }

        public void Render(List<GenericEntity> result, string type)
        {
            if (result == null || !result.Any())
            {
                Console.WriteLine(TextResource.NO_RESULTS_FOUND);
                return;
            }
            
            int count = 1;

            foreach (var parentItem in result)
            {
                var sectionHeader = $"\n\n*** {type} - {count} ***\n";
                Console.WriteLine(sectionHeader);
                _logger.LogDebug(sectionHeader);

                foreach (var childItem in parentItem.Properties)
                {
                    var bodyRow = $"{childItem.Key}: {childItem.Value}";
                    Console.WriteLine(bodyRow);
                    _logger.LogDebug(bodyRow);
                }

                count++;
            }   
        }

        public void Render(List<string> items, string type)
        {
            if (items == null || !items.Any())
            {
                Console.WriteLine(TextResource.NO_RESULTS_FOUND);
                return;
            }

            var sectionHeader = $"\n\n*** {type} ***\n";
            Console.WriteLine(sectionHeader);
            _logger.LogDebug(sectionHeader);

            foreach (var item in items)
            {
                Console.WriteLine(item);
                _logger.LogDebug(item);
            }
        }

        public CommandOutput ResolveCommand()
        {
            var command = HandleUserInputCommand(TextResource.INSTRUCTIONS_GENERAL);
            CommandOutput output;

            switch (command)
            {
                case Commands.SEARCH:
                    output = SelectSearchOptionsCommand();
                    break;
                case Commands.VIEW_FIELDS:
                    output = new CommandOutput() { Command = Commands.VIEW_FIELDS_ALL };
                    break;
                case Commands.QUIT:
                    output = new CommandOutput() { Command = Commands.QUIT };
                    break;
                case Commands.ENTER:
                    output = new CommandOutput() { Command = Commands.ENTER };
                    break;
                default:
                    output = new CommandOutput() { Command = Commands.RESTART };
                    break;
            }

            return output;
        }

        #region Helpers

        private CommandOutput SelectSearchOptionsCommand()
        {
            var command = HandleUserInputCommand(TextResource.INSTRUCTIONS_SEARCH_CRITERIA);
            CommandOutput output;
            string field, value;

            switch (command)
            {
                case Commands.SEARCH_USERS:
                    field = HandleUserInputCommand(TextResource.INSTRUCTIONS_SEARCH_STEP_1);
                    value = HandleUserInputCommand(TextResource.INSTRUCTIONS_SEARCH_STEP_2);
                    output = new CommandOutput() { Command = Commands.SEARCH_USERS, Param1 = field, Param2 = value };
                    break;
                case Commands.SEARCH_TICKETS:
                    field = HandleUserInputCommand(TextResource.INSTRUCTIONS_SEARCH_STEP_1);
                    value = HandleUserInputCommand(TextResource.INSTRUCTIONS_SEARCH_STEP_2);
                    output = new CommandOutput() { Command = Commands.SEARCH_TICKETS, Param1 = field, Param2 = value };
                    break;
                case Commands.SEARCH_ORGANIZATIONS:
                    field = HandleUserInputCommand(TextResource.INSTRUCTIONS_SEARCH_STEP_1);
                    value = HandleUserInputCommand(TextResource.INSTRUCTIONS_SEARCH_STEP_2);
                    output = new CommandOutput() { Command = Commands.SEARCH_ORGANIZATIONS, Param1 = field, Param2 = value };
                    break;
                case Commands.QUIT:
                    output = new CommandOutput() { Command = Commands.QUIT };
                    break;
                default:
                    output = new CommandOutput() { Command = Commands.RESTART };
                    break;
            }

            return output;
        }

        private string HandleUserInputCommand(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine().ToLower();
        }

        #endregion
    }
}
