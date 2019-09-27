using RestSharp.Deserializers;
namespace RabbitREPL
{

    internal class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return Username;
        }
    }
}

