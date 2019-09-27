using System;

namespace RabbitREPL
{
    internal class WhoAmICommand : ICommand
    {
        public string Description =>
            "Gives the systems view of who you are.";
        public string DetailedDescription =>
            @"";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public WhoAmICommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }

        public void Execute()
        {
            Console.WriteLine("Admin User: {0}\nClient User: {1}", Context.AdminUser, Context.User);
        }
    }
}