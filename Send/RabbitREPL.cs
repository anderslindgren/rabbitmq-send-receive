using CommandLine;
using CommandLine.Text;
using System;
using RabbitMQ.Client;
using System.Text;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json.Linq;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using System.Linq;

namespace RabbitREPL
{

    public class RabbitREPL
    {
        private Context context;
        private readonly Dictionary<string, ICommand> commands;

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to RabbitMQ test tool");
            Console.WriteLine("Type 'help' for help");
            RabbitREPL repl = new RabbitREPL();
            var result = Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options => repl.StartREPL(options))
                .WithNotParsed(errors =>
                {
                    Console.WriteLine(errors);
                    Environment.Exit(1);
                });
        }

        public RabbitREPL()
        {
            commands = new Dictionary<string, ICommand>
            {
                { "connect", new ConnectCommand() },
                { "add", new AddCommand() },
                { "purge", new PurgeCommand() },
                { "whoami", new WhoAmICommand() },
                { "remove", new RemoveCommand() },
                { "test", new TestCommand() },
                { "get", new GetCommand() },
                { "set", new SetCommand() },
                { "bind", new BindCommand() },
                { "send", new SendCommand() },
                { "list", new ListCommand() },
                { "help", new HelpCommand() }
            };

            context = new Context(commands);
        }

        private void StartREPL(Options options)
        {
            string response = "";
            string prompt = "";
            do
            {
                Console.Write("\n{0}> ", prompt);
                response = Console.ReadLine();

                if (!string.IsNullOrEmpty(response) && !response.Equals("exit"))
                {
                    ICommand command = ParseRespone(response);
                    command.Options = options;
                    command.Execute(ref context);
                }

                /*
                restClient = GetRestClient(options);
                GetOverview();
                User user = GetUser();

                Console.WriteLine("Selected user: {0} [{1}]", user.Name, user.Tags);

                using (var connection = LoginUser(user, options))
                {
                    PrintServerProperties(connection);
                }
                */

                prompt = context.GetPrompt();

            } while (response != "exit");
        }

        private ICommand ParseRespone(string response)
        {
            string[] args = response.Split(' ');
            string commandName = args[0];
            ICommand command = new UnknownCommand(commandName);
            if (commands.ContainsKey(commandName))
            {
                command = commands[commandName];
            }
            command.Args = args.Skip(1).ToArray();
            return command; 
        }


        private IConnection LoginUser(User user, Options options)
        {
            Console.Write("Enter password for user {0}: ", user.Name);
            string password = Console.ReadLine();
            var factory = new ConnectionFactory()
            {
                HostName = options.Hostname,
                UserName = user.Name,
                Password = password,
                VirtualHost = options.VirtualHost
            };
            Console.WriteLine("Connecting to: {0} with: {1}", factory.HostName, factory.UserName);

            return factory.CreateConnection();
        }



        private void Run(Options options)
        {
            var factory = new ConnectionFactory()
            {
                HostName = options.Hostname,
                UserName = options.User,
                Password = options.Password,
                VirtualHost = options.VirtualHost
            };
            Console.WriteLine("Connecting to: {0} using user: {1} and virtual host: {2}", factory.HostName, factory.UserName, factory.VirtualHost);

            using (var connection = factory.CreateConnection())
            {

                using (var channel = connection.CreateModel())
                {
                    var qs = channel.QueueDeclarePassive("hello-from-ehg");
                    Console.WriteLine("{0} {1} {2}", qs.QueueName, qs.MessageCount, qs.ConsumerCount);
                    /*
                    channel.QueueDeclare(queue: "hello-from-ehg", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    string message = "Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    for (int i = 0; i < 10; i++)
                    {
                        channel.BasicPublish(exchange: "", routingKey: "hello-from-ehg", basicProperties: null, body: body);
                        Console.WriteLine(" [x] Sent {0}", message);
                    }
                    */
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

    }

}