using RabbitMQ.Client;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitREPL
{
    internal class OverviewCommand : ICommand
    {
        public string Description =>
            "get system overview";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public OverviewCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }

        public void Execute()
        {
            //string firstParameter = Args.First();
            //string[] args = Args.Skip(1).ToArray();
            
            GetOverview(Context);
        }

        private void GetOverview(Context context)
        {
            RestRequest request = new RestRequest("overview");
            IRestResponse<Overview> userResponse = context.AdminClient.Execute<Overview>(request);
            if (userResponse.IsSuccessful)
            {
                Overview overview = userResponse.Data;
                Console.WriteLine("Cluster:            {0}", overview.ClusterName);
                Console.WriteLine("Node:               {0}", overview.Node);
                Console.WriteLine("RabbitMQ Version:   {0}", overview.RabbitmqVersion);
                Console.WriteLine("Erlang Version:     {0}", overview.ErlangVersion);
                Console.WriteLine("Management Version: {0}", overview.ManagementVersion);
            }
            else
            {
                Console.WriteLine("Status Code: {0}", userResponse.StatusCode);
            }
        }

    }
}