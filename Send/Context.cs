using System;
using System.Collections.Generic;
using RabbitMQ.Client;
using RestSharp;

namespace RabbitREPL
{
    internal class Context
    {
        public RestClient RestClient { get; internal set; }
        public IConnection Connection { get; internal set; }
        public User User { get; internal set; }
        public string VirtualHost { get; internal set; }
        public Dictionary<string, ICommand> Commands { get; }

        public Context(Dictionary<string, ICommand> commands)
        {
            this.Commands = commands;
            VirtualHost = ConnectionFactory.DefaultVHost;
        }

        internal string GetPrompt()
        {
            string result = "";
            if (RestClient != null)
            {
                result = string.Format("Rest: [{0}]\n", RestClient.BaseUrl);
            }
            if (Connection == null && User != null)
            {
                result += string.Format("Client: [{0}]\n", User.Name);
            }
            else if (Connection != null && User != null)
            {
                result += string.Format("Client: [{0} connected]\n", User.Name);
            }
            return result;
        }
    }
}