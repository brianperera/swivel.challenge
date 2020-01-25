namespace Swivel.Search.Common
{
    public static class Constants
    {
        //Paths
        public const string LOG_PATH = "Logs/consoleapp.log";
    }

    public static class TextResource
    {
        public const string NO_RESULTS_FOUND = "No results found";
        public const string UNEXPECTED_ERROR_OCCURED = "Unexpected error occured. Error logs are sent to the support team.";
        public const string PRESS_ENTER_CONTINUE = "Press Enter to continue";
        public const string LOADING = "Loading....";
        public const string INSTRUCTIONS_GENERAL = "Type 'quit' to exit at any time, press 'enter' to continue. " +
                                            "\n\n\n Select search options: \n\t * Press 1 to search " +
                                            "\n\t * Press 2 to view a list of searchable fields \n\t * Type 'quit' to exit";

        public const string INSTRUCTIONS_SEARCH_CRITERIA = "Select \n 1) Users \n 2) Tickets \n 3) Organizations";
        public const string INSTRUCTIONS_SEARCH_STEP_1 = "Enter search term";
        public const string INSTRUCTIONS_SEARCH_STEP_2 = "Enter search value";
        public const string NOT_SUPPORTED = "Command not supported, please try again";
    }

    public static class Commands
    {
        public const string RESTART = "restart";
        public const string QUIT = "quit";
        public const string ENTER = "";
        public const string SEARCH = "1";
        public const string VIEW_FIELDS = "2";
        public const string SEARCH_USERS = "1";
        public const string SEARCH_TICKETS = "2";
        public const string SEARCH_ORGANIZATIONS = "3";
        public const string VIEW_FIELDS_ALL = "4";
    }

    public static class RenderType
    {
        public const string USER = "Users";
        public const string TICKET = "Tickets";
        public const string ORGANIZATION = "Organizations";
    }

    public static class EntityKey
    {
        //User
        public const string ID = "_id";
        public const string NAME = "name";
        public const string ASSIGNED_USERS = "assigned_users";
        public const string SUBMITTED_USERS = "submitted_users";

        //Organization
        public const string ORGANIZATION_ID = "organization_id";
        public const string ORGANIZATION_NAME = "organization_name";
        
        //Ticket
        public const string ASSIGNEE_ID = "assignee_id";
        public const string SUBMITTER_ID = "submitter_id";
        public const string SUBJECT = "subject";
        public const string ASSIGNED_TICKET = "assigned_ticket";
        public const string SUBMITTED_TICKET = "submitted_ticket";
    }
}
