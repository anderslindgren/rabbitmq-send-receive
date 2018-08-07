using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Collections.Generic;

class Receive
{
    public static void Main(string[] args)
    {
        var factory = new ConnectionFactory() 
        {
            HostName = args?[0] ?? "localhost", 
            UserName = args?[1] ?? "guest", 
            Password = args?[2] ?? "guest", 
            VirtualHost = args?[3] ?? "/" 
        };

        Console.WriteLine("Connecting to: {0} using user: {1} and virtual host: {2}", factory.HostName, factory.UserName, factory.VirtualHost);
        factory.RequestedHeartbeat = 300;

        using (var connection = factory.CreateConnection())
        {
            IDictionary<string, object> serverProperties = connection.ServerProperties;
            string clusterName = GetValueFromDict(serverProperties, "cluster_name");
            string product = GetValueFromDict(serverProperties, "product");
            string version = GetValueFromDict(serverProperties, "version");
            Console.WriteLine("Connected to cluster: {0} [{1} - {2}]", clusterName, product, version);

            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello-from-ehg", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };

                channel.BasicConsume(queue: "hello-from-ehg", autoAck: true, consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
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