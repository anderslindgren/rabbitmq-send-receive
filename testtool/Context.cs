using System;
using System.Collections.Generic;
using RabbitMQ.Client;
using RestSharp;

namespace RabbitREPL
{
    internal class Context
    {
        public RestClient AdminClient { get; internal set; }
        public IConnection Connection { get; internal set; }
        public IModel Channel { get; internal set; }
        public User AdminUser { get; }
        public User User { get; internal set; }
        public string Hostname { get; }
        public int AdminPort { get; }
        public string VirtualHost { get; internal set; }
        public Dictionary<string, Type> Commands { get; }

        public Context(Options options, Dictionary<string, Type> commands)
        {
            Commands = commands;
            AdminUser = new User()
            {
                Username = options.AdminUser,
                Password = options.AdminPassword,
            };
            User = new User()
            {
                Username = options.User,
                Password = options.Password,
            };
            Hostname = options.Hostname;
            AdminPort = options.AdminPort;
            if (string.IsNullOrEmpty(options.VirtualHost))
            {
                VirtualHost = ConnectionFactory.DefaultVHost;
            }
            else
            {
                VirtualHost = options.VirtualHost;
            }
        }

        internal string GetPrompt()
        {
            string result = "TestTool";
            if (User != null)
            {
                result += GetConnetionStatus();
            }
            return result;
        }

        internal string GetConnetionStatus() => Connection == null || !Connection.IsOpen ? " [Not Connected]" : " [Connected]";
    }
}