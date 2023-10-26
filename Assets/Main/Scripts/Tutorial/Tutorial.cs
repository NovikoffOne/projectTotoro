using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Tutorial : MonoBehaviour,
    IEventReceiver<ChangeTutorialState>,
    IEventReceiver<NewGame>
{
    [SerializeField] private Light _pointLight;

    private bool _isTurorial;

    private void Start()
    {
        this.Subscribe<ChangeTutorialState>();
        this.Subscribe<NewGame>();
        _pointLight.gameObject.SetActive(false);
    }

    public void OnEvent(ChangeTutorialState var)
    {
        if (!_isTurorial)
        {
            this.gameObject.SetActive(false);
            return;
        }

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

    public void OnEvent(NewGame var)
    {
        _isTurorial = var.IndexLevel == 0;
    }
}
