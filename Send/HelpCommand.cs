using System;
using System.Collections.Generic;

namespace RabbitREPL
{
    class HelpCommand : ICommand
    {
        public string Description =>
            "gives you this help";

        public string[] Args { get; set; }
        public Options Options { get; set; }

        public void Execute(ref Context context)
        {
            Array.ForEach(Args, arg => Console.WriteLine("Was called with: " + arg));
            Console.WriteLine("Connect to a RabbitMQ Cluster bith via REST and via C# lib");
            Console.WriteLine();
            foreach (string command in context.Commands.Keys)
            {
                Console.WriteLine("{0} - {1}", command, context.Commands[command].Description);
            }
        }
    }
}
