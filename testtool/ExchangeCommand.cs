using RabbitMQ.Client;
using System.Collections.Generic;
using System.Linq;

namespace RabbitREPL
{
    internal class ExchangeCommand : ICommand
    {
        public string Description =>
            "bind exhanges and queues";
        public string DetailedDescription =>
            @"Declare an Exchange

Syntax:
  exchange <exchangename> <exchangetype> <durable> <autoDelete>

  where 
    exchangename - is the name of the new exchange
    exhangetype  - is one of direct, topic, fanout and headers
    durable      - true if the exchange should be durable
    autoDelete   - true if the exchange should automatically delete messages
            
    ";
    
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public ExchangeCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }

        public void Execute()
        {
            string exhange = Args[0];
            string type = Args[1];
            bool durable = bool.Parse(Args[2]);
            bool autoDelete = bool.Parse(Args[3]);
            Context.Channel.ExchangeDeclare(
                exchange: exhange, 
                type: type, 
                durable: durable, 
                autoDelete: autoDelete);
        }
    }
}