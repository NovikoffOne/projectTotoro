using System;

public static partial class CMD
{
    /// <summary>
    /// Выполнить команду для всех слушателей 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="invoker"></param>
    /// <param name="command"></param>
    public static void ExecuteCommand<T>(this ICommandInvoker invoker, T command)
        where T : struct, ICommand
    {
        ExecuteCommandHandling(command, _ => true);
    }
    /// <summary>
    /// Выполнить команду для определенного типа слушателей
    /// </summary>
    /// <typeparam name="T">Тип команды</typeparam>
    /// <typeparam name="R">Тип слушателя</typeparam>
    /// <param name="invoker"></param>
    /// <param name="command"></param>
    public static void ExecuteCommand<T, R>(this ICommandInvoker invoker, T command)
        where T : struct, ICommand
        where R : ICommandReciver
    {
        ExecuteCommandHandling(command, reciver => reciver.GetType().Equals(typeof(R)));
    }
    /// <summary>
    /// Выполнить команду для выбранных слушателей 
    /// </summary>
    /// <typeparam name="T">Тип команды</typeparam>
    /// <param name="invoker"></param>
    /// <param name="command"></param>
    /// <param name="predicate">Метод выбора слушателя</param>
    public static void ExecuteCommand<T>(this ICommandInvoker invoker, T command, Predicate<ICommandReciver> predicate)
        where T : struct, ICommand
    {
        ExecuteCommandHandling(command, predicate);
    }

    //Метод отправки команды слушателям
    private static void ExecuteCommandHandling<T>(T command, Predicate<ICommandReciver> predicate)
        where T : ICommand
    {
        if (ReciverInfos.TryGetValue(typeof(T), out var list))
        {
            var node = list.First;

            while (node != null)
            {
                if (predicate.Invoke(node.Value.Reciver))
                {
                    node.Value.Execute(command);
                }

                node = node.Next;
            }
        }
    }
}
