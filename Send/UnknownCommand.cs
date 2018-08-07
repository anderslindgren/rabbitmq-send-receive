using System;

namespace RabbitREPL
{
    internal class UnknownCommand : ICommand
    {
        private readonly string command;

        public string[] Args { get; set; }
        public Options Options { get; set; }
        public string Description =>
            "Unknown";

        public UnknownCommand(string command)
        {
            this.command = command;
        }

        public void Execute(ref Context context)
        {
            Console.WriteLine("Could not execute your command {0} {1}", command, string.Join(" ", Args));
        }
    }
}