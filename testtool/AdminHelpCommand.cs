using System;
using System.Collections.Generic;

namespace RabbitREPL
{
    class AdminHelpCommand : ICommand
    {
        public string Description =>
            "gives you this help";

        public string DetailedDescription =>
            @"";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public AdminHelpCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }

        public void Execute()
        {
            if (Args.Length == 0)
            {
                Console.WriteLine("Connects to a RabbitMQ Server or Cluster, both via Admin REST API and via the C# library");
                Console.WriteLine();
                Dictionary<string, Type> commands = Context.Commands;
                foreach (string command in commands.Keys)
                {
                    Type type = commands[command];
                    object[] objectArgs = new object[] { Context, new string[0] };
                    ICommand instance = (ICommand)Activator.CreateInstance(type, objectArgs);
                    Console.WriteLine("  {0,-15} - {1}", command, instance.Description);
                }
                Console.WriteLine();
                Console.WriteLine("Type \"help <command>\" to get more extensive help about a specific command.");
            }
            else
            {
                Dictionary<string, Type> commands = Context.Commands;
                string command = Args[0];
                Type type = commands[command];
                object[] objectArgs = new object[] { Context, new string[0] };
                ICommand instance = (ICommand)Activator.CreateInstance(type, objectArgs);
                Console.WriteLine("{0} - {1}\n{2}", command, instance.Description, instance.DetailedDescription);
            }
        }
    }
}
