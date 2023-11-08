using UnityEngine;
using UnityEngine.UI;

public class LockImage : MonoBehaviour
{
    [SerializeField] Button _button;

    private void OnEnable()
    {
        _button.enabled = false;
    }

    private void OnDisable()
    {
        _button.enabled = true;
    }

    public void HidePanel()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
    }
}
