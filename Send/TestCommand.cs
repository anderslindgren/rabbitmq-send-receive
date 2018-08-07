namespace RabbitREPL
{
    internal class TestCommand : ICommand
    {
        public string[] Args { get; set; }
        public Options Options { get; set; }
        public string Description =>
            "run some built in tests (alive or health)";

        public void Execute(ref Context context)
        {
            throw new System.NotImplementedException();
        }
    }
}