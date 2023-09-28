using UnityEngine;

public sealed class TestInvoker :MonoBehaviour, ICommandInvoker
{
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.ExecuteCommand(new OneTestCMD { testValue = 1 });
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.ExecuteCommand(new TwoTestCMD());
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            var cmd = new TwoTestCMD();
            cmd.Invoker = this;

            this.ExecuteCommand<TwoTestCMD, TestReciver2>(cmd);
        }
    }
}
