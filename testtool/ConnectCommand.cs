using RabbitMQ.Client;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitREPL
{
    internal class ConnectCommand : ICommand
    {
        public string Description =>
            "Connects the current user to the amqp port";
        public string DetailedDescription =>
            @"";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public ConnectCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }

        public void Execute()
        {
            Context.Connection = GetClientConnection();
            PrintServerProperties(Context.Connection);
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

            Console.WriteLine(factory.Endpoint.ToString());

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