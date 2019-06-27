using CommandLine;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitREPL
{

    public class RabbitREPL
    {
        private readonly Context context;

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to RabbitMQ test tool");
            Console.WriteLine("Type 'help' for help");
            var result = Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options =>
                {
                    RabbitREPL repl = new RabbitREPL(options);
                    repl.Connect();
                    repl.StartREPL();
                })
                .WithNotParsed(errors =>
                {
                    Console.WriteLine(errors);
                    Environment.Exit(1);
                });
        }

        public RabbitREPL(Options options)
        {
            Dictionary<string, Type> commands = new Dictionary<string, Type>
            {
                { "connect", typeof(ConnectCommand)},
                { "overview", typeof(OverviewCommand)},
                { "add", typeof(AddCommand) },
                { "purge", typeof(PurgeCommand) },
                { "remove", typeof(RemoveCommand) },
                { "test", typeof(TestCommand) },
                { "get", typeof(GetCommand) },
                { "set", typeof(SetCommand) },
                { "bind", typeof(BindCommand) },
                { "send", typeof(SendCommand) },
                { "list", typeof(ListCommand) },
                { "whoami", typeof(WhoAmICommand) },
                { "help", typeof(HelpCommand) }
            };

            context = new Context(options, commands);

        }

        private void StartREPL()
        {
            string commandline;
            string prompt = context.GetPrompt();

            try
            {
                do
                {
                    Console.Write("{0}> ", prompt);
                    commandline = Console.ReadLine();

                    if (!string.IsNullOrEmpty(commandline) && !commandline.Equals("exit"))
                    {
                        ICommand command = ParseCommand(commandline , context);
                        try
                        {
                            command.Execute();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("shoot, I ran into some problems: " + e.Message);
                            Console.WriteLine(e.StackTrace);
                        }
                    }

                    /*
                    restClient = GetRestClient(options);
                    GetOverview();
                    User user = GetUser();

                    Console.WriteLine("Selected user: {0} [{1}]", user.Username, user.Tags);

                    using (var connection = LoginUser(user, options))
                    {
                        PrintServerProperties(connection);
                    }
                    */

                    prompt = context.GetPrompt();

                } while (commandline != "exit");

            }
            finally
            {
                if (context.Connection != null)
                {
                    context.Connection.Close();
                }
            }
        }

        private void Connect()
        {
            ConnectCommand cc = new ConnectCommand(context, new string[0]);
            context.AdminClient = cc.GetRestClient();
            //context.Connection = cc.GetClientConnection();
            //PrintServerProperties(context.Connection);
        }

        private ICommand ParseCommand(string commandline, Context context)
        {
            string[] args = commandline.Split(' ');
            string commandName = args.First();
            string[] commandArgs = args.Skip(1).ToArray();
            Dictionary<string, Type> commands = context.Commands;
            ICommand command;
            if (commands.ContainsKey(commandName))
            {
                Type type = commands[commandName];
                object[] arguments = new object[] { context, commandArgs };
                command = (ICommand)Activator.CreateInstance(type, arguments);
            }
            else
            {
                command = new UnknownCommand(commandline);
            }
            return command;
        }

        private static void PrintServerProperties(IConnection connection)
        {
            IDictionary<string, object> serverProperties = connection.ServerProperties;
            string clusterName = GetValueFromDict(serverProperties, "cluster_name");
            string product = GetValueFromDict(serverProperties, "product");
            string version = GetValueFromDict(serverProperties, "version");
            Console.WriteLine("Connected to cluster: {0} [{1} - {2}]", clusterName, product, version);
        }

        private static string GetValueFromDict(IDictionary<string, object> dict, string key)
        {
            string result = "";
            if (dict.TryGetValue(key, out object value))
            {
                result = Encoding.UTF8.GetString((byte[])value);
            }

            return result;
        }


        private IConnection LoginUser(User user, Options options)
        {
            Console.Write("Enter password for user {0}: ", user.Username);
            string password = Console.ReadLine();
            var factory = new ConnectionFactory()
            {
                HostName = options.Hostname,
                UserName = user.Username,
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