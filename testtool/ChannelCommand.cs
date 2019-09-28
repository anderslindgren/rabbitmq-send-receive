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
@"Channels are also meant to be long-lived but since many recoverable protocol errors will result in channel closure, 
channel lifespan could be shorter than that of its connection. 
Closing and opening new channels per operation is usually unnecessary but can be appropriate. 
When in doubt, consider reusing channels first.";

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
