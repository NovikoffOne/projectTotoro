using System;
using System.Reflection;

public static partial class CMD
{
    /// <summary>
    /// Удалить слушателя из обработчика команд
    /// </summary>
    /// <param name="reciver"></param>
    public static void Forget(this ICommandReciver reciver)
    {
        var methodInfos = reciver.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        for (var i = 0; i < methodInfos.Length; ++i)
        {
            var commandAttribute = Attribute.GetCustomAttribute(methodInfos[i], typeof(CommandAttribute)) as CommandAttribute;

            if (commandAttribute != null)
            {
                RemoveReciver(reciver.GetType(), commandAttribute.CommandType);
            }
        }
    }

    /// <summary>
    /// Удалить слушателя из обработчика команд, только для определенного типа команды
    /// </summary>
    /// <typeparam name="T">Тип команды</typeparam>
    /// <param name="reciver"></param>
    public static void Forget<T>(this ICommandReciver reciver)
        where T : struct, ICommand
    {
        RemoveReciver(reciver.GetType(), typeof(T));
    }

    //Метод удаления слушателя
    private static void RemoveReciver(Type reciverType, Type commandType)
    {
        if (ReciverInfos.TryGetValue(commandType, out var list))
        {
            var node = list.First;

            while (node != null)
            {
                if (node.Value.Reciver.GetType().Equals(reciverType))
                {
                    ReciverBindedMethodToken[reciverType].Remove(node.Value.Token);

                    node.Value.Dispose();

                    var next = node.Next;
                    list.Remove(node);
                    node = next;
                }
                else
                {
                    node = node.Next;
                }
            }
        }
    }
}
