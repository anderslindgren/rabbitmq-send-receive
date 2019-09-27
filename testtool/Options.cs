using CommandLine;
using RabbitMQ.Client;

namespace RabbitREPL
{
    public class Options
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
}