using CommandLine;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    repl.ConnectAdmin();
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
                { "connect",  typeof(ConnectCommand) },
                { "channel",  typeof(ChannelCommand) },
                { "overview", typeof(OverviewCommand) },
                { "user",     typeof(UserCommand) },
                { "add",      typeof(AddCommand) },
                { "purge",    typeof(PurgeCommand) },
                { "remove",   typeof(RemoveCommand) },
                { "test",     typeof(TestCommand) },
                { "set",      typeof(SetCommand) },
                { "bind",     typeof(BindCommand) },
                { "send",     typeof(SendCommand) },
                { "receive",  typeof(ReceiveCommand) },
                { "list",     typeof(ListCommand) },
                { "whoami",   typeof(WhoAmICommand) },
                { "help",     typeof(HelpCommand) }
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
                            Console.WriteLine("!!!>>>: {0}", e.Message);
                            Console.WriteLine(e.StackTrace);
                        }
                    }

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

        private void ConnectAdmin()
        {
            string url = string.Format("http://{0}:{1}/api", context.Hostname, context.AdminPort);
            Console.WriteLine("Setting up Admin interface at \"{0}\" with user: {1}", url, context.AdminUser);
            context.AdminClient = new RestClient(url)
            {
                Authenticator = new HttpBasicAuthenticator(context.AdminUser.Username, context.AdminUser.Password)
            };
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
    }
}