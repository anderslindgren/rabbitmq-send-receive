using System;

namespace RabbitREPL
{
    internal class UnknownCommand : ICommand
    {
        public string Description =>
            "Unknown";
        public string DetailedDescription =>
            @"";
        private readonly string command;

        public UnknownCommand(string command)
        {
            this.command = command;
        }

        public void Execute()
        {
            Console.WriteLine("Could not execute your command {0}", command);
        }
    }
}