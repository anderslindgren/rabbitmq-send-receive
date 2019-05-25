using System;
using System.Collections.Generic;

namespace RabbitREPL
{
    class HelpCommand : ICommand
    {
        public string Description =>
            "gives you this help";

        private string[] Args { get; set; }
        private Context Context { get; set; }

        public HelpCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }


        public void Execute()
        {
            Array.ForEach(Args, arg => Console.WriteLine("Was called with: " + arg));
            Console.WriteLine("Connects to a RabbitMQ Server or Cluster, both via Admin REST API and via the C# library");
            Console.WriteLine();
            Dictionary<string, Type> commands = Context.Commands;
            foreach (string command in commands.Keys)
            {
                Type type = commands[command];
                object[] objectArgs = new object[] { Context, new string[0] };
                ICommand instance = (ICommand) Activator.CreateInstance(type, objectArgs);
                Console.WriteLine("{0} - {1}", command, instance.Description);
            }
        }
    }
}
