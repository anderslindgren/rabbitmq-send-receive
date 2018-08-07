using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitREPL
{
    internal class GetCommand : ICommand
    {
        public string[] Args { get; set; }
        public Options Options { get; set; }
        public string Description =>
            "user";
        public void Execute(ref Context context)
        {
            string firstParameter = Args.First();
            string[] connectionArgs = Args.Skip(1).ToArray();
            if (firstParameter.Equals("user"))
            {
                context.User = GetUser(context);
                if (context.User.Password == null)
                {
                    Console.Write("Please enter the password for user [{0}]: ", context.User.Name);
                    context.User.Password = Console.ReadLine();
                }
            }
        }

        private User GetUser(Context context)
        {
            RestRequest request = new RestRequest("users");
            IRestResponse<List<User>> userResponse = context.RestClient.Execute<List<User>>(request);
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
    }
}