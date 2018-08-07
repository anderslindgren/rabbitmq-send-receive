namespace RabbitREPL
{
    internal interface ICommand
    {
        string[] Args { get; set; }

        Options Options { get; set; }

        string Description { get; }

        void Execute(ref Context context);
    }
}