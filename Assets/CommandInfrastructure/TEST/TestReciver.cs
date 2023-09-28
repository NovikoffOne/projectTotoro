using UnityEngine;

public sealed class TestReciver : MonoBehaviour, ICommandReciver
{
    private void Start()
    {
        this.Bind();
    }

    [Command(typeof(TwoTestCMD))]
    public void Test2()
    {
        Debug.Log("Is test 2 R1");
    }

    [Command(typeof(OneTestCMD), 1)]
    public void Test1(OneTestCMD command)
    {
        Debug.Log(command.testValue);
        Debug.Log("Is test 1 R1");
    }

    [Command(typeof(OneTestCMD), 2)]
    public void Test1()
    {
        Debug.Log("Is test 2 R1");
    }
}
