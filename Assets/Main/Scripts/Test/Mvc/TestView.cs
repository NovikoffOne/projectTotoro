using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestView : MonoBehaviour, IView
{
    public TextMeshProUGUI Text;
    public Button button;

    private void Start()
    {
        this.AddController<TestMVC>();
    }
}
