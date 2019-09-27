using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitREPL
{
    internal class UserCommand : ICommand
    {
        public string Description =>
            "User commands";
        public string DetailedDescription =>
@"
  select            - Select a user already defined in the system. You will be prompted for the password.
  list              - list all defined users in the system.
  add <username>    - Add new user.
  delete <username> - Delete a user.";
        private string[] Args { get; set; }
        private Context Context { get; set; }

        public UserCommand(Context context, string[] args)
        {
            Context = context;
            Args = args;
        }

        public void Execute()
        {
            string firstParameter = Args.First();
            string[] userArgs = Args.Skip(1).ToArray();
            if (firstParameter.Equals("select"))
            {
                Context.User = GetUser(Context.AdminClient);
                if (string.IsNullOrEmpty(Context.User.Password))
                {
                    Console.Write("Please enter the password for user [{0}]: ", Context.User.Username);
                    Context.User.Password = Console.ReadLine();
                }
            }
            else if (firstParameter.Equals("add"))
            {

            }
            else if (firstParameter.Equals("delete"))
            {

            }
        }

        private User GetUser(RestClient rest)
        {
            RestRequest request = new RestRequest("users");
            IRestResponse<List<RabbitUser>> userResponse = rest.Execute<List<RabbitUser>>(request);
            List<RabbitUser> users = userResponse.Data;

            Console.WriteLine(" Index\tName\tTags");
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine(" [{0}]\t{1}\t{2}", i, users[i].Name, users[i].Tags);
            }
            Console.Write("Which user would you like to use? [0]: ");
            string userNo = Console.ReadLine();
            Int32.TryParse(userNo, out int index);
            return new User() {
                Username = users[index].Name
            };
        }
    }
}