using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Tutorial : MonoBehaviour,
    IEventReceiver<ChangeTutorialState>,
    IEventReceiver<StartGame>
{
    [SerializeField] private Light _pointLight;

    private bool _isTurorial = true;

    private void Start()
    {
        this.Subscribe<ChangeTutorialState>();
        this.Subscribe<StartGame>();
    }

    public void OnEvent(ChangeTutorialState var)
    {
        if (_isTurorial == false)
        {
            _pointLight.gameObject.SetActive(false);
            Debug.Log("Is tutorial == false");
            return;
        }

        Debug.Log($"@@@ is tutorial == {_isTurorial}");

        switch (var.TutorialState)
        {
            case 0:
                _pointLight.gameObject.SetActive(true);
                _pointLight.transform.position = new Vector3(3, 1, -.3f);
                break;

            case 3:
                _pointLight.gameObject.SetActive(true);
                _pointLight.transform.position = new Vector3(7, 1, -.3f);
                break;

            case 5:
                _pointLight.gameObject.SetActive(true);
                _pointLight.transform.position = new Vector3(7, 2, -.3f);
                break;

            default:
                break;
        }
    }

    public void OnEvent(StartGame var)
    {
        if (var.LevelIndex == 0)
            _isTurorial = true;
        else
        {
            _isTurorial = false;
        }

        Debug.Log($"@@@ is Tutorial == {_isTurorial}  index {var.LevelIndex}");
    }
}
