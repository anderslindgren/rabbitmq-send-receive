using RabbitMQ.Client;
using System.Collections.Generic;
using System.Linq;

namespace RabbitREPL
{
    internal class ExchangeCommand : ICommand
    {
        public string Description =>
            "Declares an Exchange";
        public string DetailedDescription =>
            @"Declare an Exchange

Syntax:
  exchange create <exchangeName> <exchangeType> <durable> <autoDelete>
  exchange delete <exchangeName

  where 
    exchangeName - is the name of the new exchange
    exhangeType  - is one of direct, topic, fanout and headers
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
            string command = Args[0];
            string exhangeName = Args[1];

            if (command.Equals("create"))
            {
                // TODO: Check number of arguments and add default values
                string type = Args[2];
                bool durable = bool.Parse(Args[3]);
                bool autoDelete = bool.Parse(Args[4]);
                Context.Channel.ExchangeDeclare(
                    exchange: exhangeName, 
                    type: type, 
                    durable: durable, 
                    autoDelete: autoDelete);
            }
            else if (command.Equals("delete"))
            {
                // TODO: Check number of arguments and add default values
                bool ifUnused = bool.Parse(Args[2]);
                Context.Channel.ExchangeDelete(exchange: exhangeName, ifUnused: ifUnused);
            }
        }
    }
}