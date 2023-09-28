public struct OneTestCMD : ICommand
{
    public int testValue;
}

public struct TwoTestCMD : ICommand<TestInvoker>
{
    public int testValue;

    public TestInvoker Invoker { get; set; }
}
