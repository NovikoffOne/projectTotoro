using System;

/// <summary>
/// Атрибут для метода, который будет вызываться при команде
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class CommandAttribute : Attribute
{
    /// <summary>
    /// Атрибут для метод
    /// </summary>
    /// <param name="commandType">Тип команды/param>
    /// <param name="commandExecuteOrder">Приоритет выполнения </param>
    public CommandAttribute(Type commandType, int commandExecuteOrder = 0)
    {
        if (commandType.GetInterface(nameof(ICommand)) == null)
        {
            throw new Exception($"[Command Attribute] Target type command not implamantation of ICommand");
        }

        CommandType = commandType;
        CommandExecuteOrder = commandExecuteOrder;
    }

    public Type CommandType { get; }
    public int CommandExecuteOrder { get; }
}
