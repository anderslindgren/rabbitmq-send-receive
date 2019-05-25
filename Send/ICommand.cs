namespace RabbitREPL
{
    internal interface ICommand
    {
        string Description { get; }

        void Execute();
    }
}