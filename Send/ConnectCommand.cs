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
        public string[] Args { get; set; }
        public Options Options { get; set; }
        public string Description =>
            "(admin | client [user password])";
        public void Execute(ref Context context)
        {
            string firstParameter = Args.First();
            string[] connectionArgs = Args.Skip(1).ToArray();
            if (firstParameter.Equals("admin"))
            {
                context.RestClient = GetRestClient(Options);
                GetOverview(context);
            }
            else if (firstParameter.Equals("client"))
            {
                if (context.User == null)
                {
                    context.User = new User()
                    {
                        Name = connectionArgs[0],
                        Password = connectionArgs[1],
                    };
                }
                context.Connection = GetClientConnection(Options, context);
                PrintServerProperties(context.Connection);
            }
        }

        private IConnection GetClientConnection(Options options, Context context)
        {
            var factory = new ConnectionFactory()
            {
                HostName = options.Hostname,
                UserName = context.User.Name,
                Password = context.User.Password,
                VirtualHost = context.VirtualHost
            };
            Console.WriteLine("Connecting to: {0} with: {1} and virtual host: {2}", factory.HostName, factory.UserName, factory.VirtualHost);

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

        private RestClient GetRestClient(Options options)
        {
            var url = string.Format("http://{0}:{1}/api", options.Hostname, options.AdminPort);
            var client = new RestClient(url)
            {
                Authenticator = new HttpBasicAuthenticator(options.AdminUser, options.AdminPassword)
            };
            return client;
        }

        private void GetOverview(Context context)
        {
            RestRequest request = new RestRequest("overview");
            IRestResponse<Overview> userResponse = context.RestClient.Execute<Overview>(request);
            Overview value = userResponse.Data;
            Console.WriteLine(value.ToString());
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