using System;
using System.Reflection;
using System.Collections.Generic;

/// <summary>
/// API для "динамических команд"
/// </summary>
public static partial class CMD
{
    //ВРЕМЕННО!!
    private static Dictionary<Type, LinkedList<ReciverInfo>> ReciverInfos = new();
    private static Dictionary<Type, HashSet<int>> ReciverBindedMethodToken = new();

    /// <summary>
    /// Объект для хранения информации по получателю и его рабочему методу для “динамической команды”
    /// </summary>
    private sealed class ReciverInfo : IDisposable
    {
        private MethodInfo _commandMethod;
        private bool _isUseCommandAsParametr;

        public ReciverInfo(
            ICommandReciver reciver,
            MethodInfo commandMethod,
            int order,
            bool isUseCommandAsParametr)
        {
            Reciver = reciver;
            Order = order;

            _commandMethod = commandMethod;
            _isUseCommandAsParametr = isUseCommandAsParametr;
        }

        public ICommandReciver Reciver { get; private set; }
        public int Token => _commandMethod.MetadataToken;
        public int Order { get; }

        public void Execute(ICommand command)
        {
            if (_isUseCommandAsParametr)
            {
                object[] parametrs =
                {
                command
                };

                _commandMethod?.Invoke(Reciver, parametrs);
            }
            else
            {
                _commandMethod?.Invoke(Reciver, null);
            }
        }

        public void Dispose()
        {
            Reciver = null;
            _commandMethod = null;

            GC.SuppressFinalize(this);
        }
    }
}