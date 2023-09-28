using System;
using System.Reflection;
using System.Collections.Generic;

public static partial class CMD
{
    /// <summary>
    /// Добавить слушателя в обработчик команд
    /// </summary>
    /// <param name="reciver">слушатель команд</param>
    public static void Bind(this ICommandReciver reciver)
    {
        BindReciverMethod(
                reciver,
                attribute => attribute != null
                );
    }

    /// <summary>
    /// Добавить слушателя в обработчик команд, но только для определенного типа команды
    /// </summary>
    /// <typeparam name="T">тип команды</typeparam>
    /// <param name="reciver">слушатель команд</param>
    public static void Bind<T>(this ICommandReciver reciver)
        where T : struct, ICommand
    {
        BindReciverMethod(
                reciver,
                attribute => attribute != null && attribute.CommandType.Equals(typeof(T))
                );
    }

    //Общий метод добавления слушателя и подключение к в обработчику команд 
    private static void BindReciverMethod(ICommandReciver reciver, Predicate<CommandAttribute> predicate)
    {
        var methodInfos = reciver.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        for (var i = 0; i < methodInfos.Length; ++i)
        {
            var commandAttribute = Attribute.GetCustomAttribute(methodInfos[i], typeof(CommandAttribute)) as CommandAttribute;

            if (predicate.Invoke(commandAttribute))
            {
                if (!ReciverBindedMethodToken.TryGetValue(reciver.GetType(), out var tokenHashSet))
                {
                    tokenHashSet = new();
                    ReciverBindedMethodToken.Add(reciver.GetType(), tokenHashSet);
                }

                if (tokenHashSet.Contains(methodInfos[i].MetadataToken)) return;

                tokenHashSet.Add(methodInfos[i].MetadataToken);

                if (!ReciverInfos.TryGetValue(commandAttribute.CommandType, out var list))
                {
                    list = new();
                    ReciverInfos.Add(commandAttribute.CommandType, list);
                }

                var paramentrInfo = methodInfos[i].GetParameters();

                //VALIDATE
                if (paramentrInfo.Length > 0)
                {
                    if (paramentrInfo.Length > 1)
                    {
                        throw new Exception($"[CMD] Не могу связать рабочий метод {methodInfos[i].Name} помеченный атрибутом Command Attribute, т.к. метод содержит более 1 аргумента.");
                    }

                    if (!paramentrInfo[default].ParameterType.Equals(commandAttribute.CommandType))
                    {
                        throw new Exception($"[CMD] Не могу связать рабочий метод {methodInfos[i].Name} помеченный атрибутом Command Attribute, т.к. аргумент типа {paramentrInfo[default].ParameterType}, не наследует интерфейс ICommand.");
                    }
                }

                AddSort(
                    new ReciverInfo(reciver, methodInfos[i], commandAttribute.CommandExecuteOrder, paramentrInfo.Length > 0),
                    list);
            }
        }
    }
    //Метод сортировки списка слушателей, по значению порядка выполнения указанного в атрибуте Command Attribute
    private static void AddSort(ReciverInfo info, LinkedList<ReciverInfo> list)
    {
        if (list.Count > 0)
        {
            if (info.Order >= list.Last.Value.Order)
            {
                list.AddLast(info);
                return;
            }
            if (info.Order <= list.First.Value.Order)
            {
                list.AddFirst(info);
                return;
            }

            var node = list.Last;

            while (node != null)
            {
                if (info.Order >= node.Value.Order)
                {
                    list.AddBefore(node.Next, info);
                    break;
                }

                node = node.Previous;
            }

        }
        else
        {
            list.AddLast(info);
        }
    }
}