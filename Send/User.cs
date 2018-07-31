using RestSharp.Deserializers;

partial class Send
{
    private class User
    {
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        [DeserializeAs(Name = "password_hash")]
        public string PasswordHash { get; set; }

        [DeserializeAs(Name = "tags")]
        public string Tags { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", Name, PasswordHash, Tags);
        }
    }
}

