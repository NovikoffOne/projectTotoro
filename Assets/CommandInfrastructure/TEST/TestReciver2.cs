using UnityEngine;

public sealed class TestReciver2 : MonoBehaviour, ICommandReciver
{
    private void Start()
    {
        this.Bind();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.Forget();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            this.Forget<TwoTestCMD>();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            this.Bind();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            this.Bind<OneTestCMD>();
        }
    }

    [Command(typeof(OneTestCMD), 1)]
    private void Test1()
    {
        Debug.Log("Is test 1 R2");
    }

    [Command(typeof(OneTestCMD), 3)]
    private void Test1(OneTestCMD command)
    {
        Debug.Log(command.testValue);
        Debug.Log("Is test 3 R2");
    }

    [Command(typeof(TwoTestCMD), 5)]
    private void Test2(TwoTestCMD twoTestCMD)
    {
        Debug.Log($"Invoker - {twoTestCMD.Invoker?.name}");
        Debug.Log("Is test 2 R2");
    }
}
