/// <summary>
/// Интерфейс команды
/// </summary>
public interface ICommand
{
}

/// <summary>
/// Интерфейс команды с вызывающим
/// </summary>
/// <typeparam name="I"></typeparam>
public interface ICommand<I> : ICommand
    where I : ICommandInvoker
{
    I Invoker { get; set; }
}
