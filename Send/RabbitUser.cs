namespace RabbitREPL
{

    internal class RabbitUser
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string PasswordHash { get; set; }

        public string Tags { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", Name, PasswordHash, Tags);
        }
    }
}

