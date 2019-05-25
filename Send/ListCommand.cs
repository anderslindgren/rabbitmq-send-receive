using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitREPL
{
    class ListCommand : ICommand
    {
        public string Description =>
@"List different properties from the RabbitMQ cluster
supported commands:
  list users";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public ListCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }

        public void Execute()
        {
            string firstParameter = Args.First();
            string[] connectionArgs = Args.Skip(1).ToArray();
            if (firstParameter.Equals("users"))
            {
                ListUsers();
            }
            else
            {
                Console.WriteLine("Sorry, no can do that.");
            }
        }

        private void ListUsers()
        {
            RestRequest request = new RestRequest("users");
            IRestResponse<List<RabbitUser>> userResponse = Context.RestClient.Execute<List<RabbitUser>>(request);
            List<RabbitUser> users = userResponse.Data;

            Console.WriteLine(" Name\tTags");
            foreach (RabbitUser user in users)
            {
                Console.WriteLine(" [{0}]\t{1}", user.Name, user.Tags);
            }
        }
    }
}
