using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Tutorial : MonoBehaviour,
    IEventReceiver<ChangeTutorialState>
{
    [SerializeField] private Light _pointLight;

    private void Start()
    {
        this.Subscribe<ChangeTutorialState>();
    }

    public void OnEvent(ChangeTutorialState var)
    {
        switch (var.TutorialState)
        {
            case 0:
                _pointLight.gameObject.SetActive(true);
                _pointLight.transform.position = new Vector3(3, 1, -.3f);
                break;

            case 3:
                _pointLight.transform.position = new Vector3(7, 1, -.3f);
                break;

            case 5:
                _pointLight.transform.position = new Vector3(7, 2, -.3f);
                break;

            default:
                _pointLight.gameObject.SetActive(false);
                break;
        }
    }
}
