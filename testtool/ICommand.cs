namespace RabbitREPL
{
    internal interface ICommand
    {
        string Description { get; }

        string DetailedDescription { get; }

        void Execute();
    }
}