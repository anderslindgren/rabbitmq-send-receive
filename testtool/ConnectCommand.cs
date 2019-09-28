using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitREPL
{
    internal class ConnectCommand : ICommand
    {
        public string Description =>
            "Connects the current user to the amqp port";
        
        public string DetailedDescription =>
@"Connections are meant to be long-lived. The underlying protocol is designed and optimized for long running connections. 
That means that opening a new connection per operation, e.g. a message published, is unnecessary and strongly discouraged 
as it will introduce a lot of network roundtrips and overhead.";

        private string[] Args { get; set; }
        private Context Context { get; set; }

        public ConnectCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }

        public void Execute()
        {
            IConnection conn = GetClientConnection();
            PrintServerProperties(conn);

            Console.Write("Creating a default Channel... ");
            IModel channel = conn.CreateModel();
            Context.Connection = conn;
            Context.Channel = channel;
            Console.WriteLine("done.");
        }

        public IConnection GetClientConnection()
        {
            var factory = new ConnectionFactory()
            {
                HostName = Context.Hostname,
                UserName = Context.User.Username,
                Password = Context.User.Password,
                VirtualHost = Context.VirtualHost,
                Port = 5672
            };
            Console.WriteLine("Connecting to: {0} with user: {1} [{2}] and virtual host: {3}", 
                factory.HostName, 
                factory.UserName, 
                factory.Password, 
                factory.VirtualHost);

            return factory.CreateConnection();
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


    }
}