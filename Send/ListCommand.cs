using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitREPL
{
    class ListCommand : ICommand
    {
        public string[] Args { get; set; }
        public Options Options { get; set; }
        public string Description =>
            "list users";
        public void Execute(ref Context context)
        {
            string firstParameter = Args.First();
            string[] connectionArgs = Args.Skip(1).ToArray();
            if (firstParameter.Equals("users"))
            {
                ListUsers(context);
            }
        }

        private void ListUsers(Context context)
        {
            RestRequest request = new RestRequest("users");
            IRestResponse<List<User>> userResponse = context.RestClient.Execute<List<User>>(request);
            List<User> users = userResponse.Data;

            Console.WriteLine(" Name\tTags");
            foreach (User user in users)
            {
                Console.WriteLine(" [{0}]\t{1}", user.Name, user.Tags);
            }
        }
    }
}
