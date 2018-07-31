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

partial class Send
{
    private RestClient restClient;

    class Options
    {
        [Option('h', HelpText = "Name of the RabbitMQ Cluster node to connect to", Default = "localhost")]
        public string Hostname { get; set; }

        [Option('a', HelpText = "Admin port", Default = 15672)]
        public int AdminPort { get; set; }

        [Option('A', HelpText = "Admin User to use when connecting to the RabbitMQ admin api", Default = ConnectionFactory.DefaultUser)]
        public string AdminUser { get; set; }

        [Option('P', HelpText = "Admin User password", Default = ConnectionFactory.DefaultPass)]
        public string AdminPassword { get; set; }

        [Option('u', HelpText = "User to use when connecting to the RabbitMQ cluster", Default = ConnectionFactory.DefaultUser)]
        public string User { get; set; }

        [Option('p', HelpText = "User password", Default = ConnectionFactory.DefaultPass)]
        public string Password { get; set; }

        [Option('v', HelpText = "Which virtual host to use", Default = ConnectionFactory.DefaultVHost)]
        public string VirtualHost { get; set; }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to RabbitMQ test tool");
        Send send = new Send();
        var result = Parser.Default.ParseArguments<Options>(args)
            .WithParsed(options => send.RunRestApi(options))
            .WithNotParsed(errors => Environment.Exit(1));

    }

    private void RunRestApi(Options options)
    {
        restClient = GetClient(options);
        GetOverview();
        User user = GetUser();

        Console.WriteLine("Selected user: {0} [{1}]", user.Name, user.Tags);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }

    private void GetOverview()
    {
        RestRequest request = new RestRequest("overview");
        IRestResponse<Overview> userResponse = restClient.Execute<Overview>(request);
        var value = userResponse.Data;
        Console.WriteLine(value.ToString());
    }

    private User GetUser()
    {
        RestRequest request = new RestRequest("users");
        IRestResponse<List<User>> userResponse = restClient.Execute<List<User>>(request);
        List<User> users = userResponse.Data;

        Console.WriteLine(" Index\tName\tTags");
        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine(" [{0}]\t{1}\t{2}", i, users[i].Name, users[i].Tags);
        }
        Console.Write("Which user would you like to use? [0]: ");
        string userNo = Console.ReadLine();
        Int32.TryParse(userNo, out int index);
        return users[index];
    }

    private static RestClient GetClient(Options options)
    {
        var url = string.Format("http://{0}:{1}/api", options.Hostname, options.AdminPort);
        var client = new RestClient(url)
        {
            Authenticator = new HttpBasicAuthenticator(options.AdminUser, options.AdminPassword)
        };
        return client;
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
            IDictionary<string, object> serverProperties = connection.ServerProperties;
            string clusterName = GetValueFromDict(serverProperties, "cluster_name");
            string product = GetValueFromDict(serverProperties, "product");
            string version = GetValueFromDict(serverProperties, "version");
            Console.WriteLine("Connected to cluster: {0} [{1} - {2}]", clusterName, product, version);

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

