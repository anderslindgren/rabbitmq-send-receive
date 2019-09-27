using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitREPL
{
    class ChannelCommand : ICommand
    {
        public string Description => 
            @"Declare a channel";

        public string DetailedDescription =>
            @"";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public ChannelCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }

        public void Execute()
        {
            Context.Channel = Context.Connection.CreateModel();
        }
    }
}
