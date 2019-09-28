﻿using RestSharp;
using System;
using System.Linq;

namespace RabbitREPL
{
    internal class AliveCommand : ICommand
    {
        public string Description =>
            "run the built-in aliveness-test";
        public string DetailedDescription =>
@"Creates a aliveness-test queue and sends a message to it. Returns OK if it can read the message as well.

  Syntax:
    alive <vhost>

  Example:
    alive /";
        private string[] Args { get; set; }
        private Context Context { get; set; }
        private readonly string vhost;

        public AliveCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
            vhost = Args.Count() == 0 ? "/" : Args.First();
            if (vhost == "/")
            {
                vhost = "%2F";
            }
        }

        public void Execute()
        {
            //string vhost = Args.FirstOrDefault(mumbo jumbo);
            RestRequest request = new RestRequest("aliveness-test/" + vhost);
            IRestResponse<Alive> userResponse = Context.AdminClient.Execute<Alive>(request);
            if (userResponse.IsSuccessful)
            {
                Alive value = userResponse.Data;
                Console.WriteLine($"VHost {(vhost == "%2F" ? "/" : vhost)} test result is: {value}");
            }
            else
            {
                Console.WriteLine($"VHost {(vhost == "%2F" ? "/" : vhost)} error: \"{userResponse.StatusDescription}\"");
            }
        }

        internal class Alive
        {
            public string Status { get; set; }

            public override string ToString() => Status;
        }
    }
}