using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Tutorial : MonoBehaviour,
    IEventReceiver<ChangeTutorialState>
{
    [SerializeField] private Light _pointLightPrefab;
    
    private Light _pointLight;
    private bool _isFirst;

    private void Awake()
    {
        _isFirst = true;
    }

    private void Start()
    {
        this.Subscribe<ChangeTutorialState>();
    }

    private void OnDisable()
    {
        if (_pointLight == null)
            return;

        Debug.Log("OnDisable");
        _pointLight.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        this.gameObject.SetActive(false);
        this.Unsubscribe<ChangeTutorialState>();
    }

    public void OnEvent(ChangeTutorialState var)
    {
        Debug.Log($"ChangaeTutorialState");
        
        if (var.IsTutorial == false || _isFirst == false)
            return;

        if(_pointLight == null)
            _pointLight = Instantiate(_pointLightPrefab, new Vector3(3, 1, -.3f), Quaternion.identity);

        switch (var.TutorialState)
        {
            case 0:
                _pointLight.transform.position = new Vector3(3, 1, -.3f);
                break;

            case 3:
                _pointLight.transform.position = new Vector3(7, 1, -.3f);
                break;

            case 5:
                _pointLight.transform.position = new Vector3(7, 2, -.3f);
                _isFirst = false;
                break;

            default:
                break;
        }
    }

    //public void OnEvent(StartGame var)
    //{
    //    if (var.LevelIndex == 0)
    //        _isTurorial = true;
    //    else
    //    {
    //        _isTurorial = false;
    //    }

    //    Debug.Log($"@@@ is Tutorial == {_isTurorial}  index {var.LevelIndex}");
    //}
}
