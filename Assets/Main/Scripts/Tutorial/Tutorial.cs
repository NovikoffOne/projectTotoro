using UnityEngine;

public class Tutorial : MonoBehaviour,
    IEventReceiver<ChangeTutorialState>
{
    [SerializeField] private Light _pointLightPrefab;

    private Vector3 _startLightPosition = new Vector3(3, 1, -.3f);
    private Vector3 _state3LightPosition = new Vector3(7, 1, -.3f);
    private Vector3 _state5LightPosition = new Vector3(7, 2, -.3f);

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

        _pointLight.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        this.gameObject.SetActive(false);
        this.Unsubscribe<ChangeTutorialState>();
    }

    public void OnEvent(ChangeTutorialState var)
    {
        if (var.IsTutorial == false || _isFirst == false)
            return;

        if (_pointLight == null)
            _pointLight = Instantiate(_pointLightPrefab, _startLightPosition, Quaternion.identity);

        switch (var.TutorialState)
        {
            case 0:
                _pointLight.transform.position = _startLightPosition;
                break;

            case 3:
                _pointLight.transform.position = _state3LightPosition;
                break;

            case 5:
                _pointLight.transform.position = _state5LightPosition;
                _isFirst = false;
                break;

            default:
                break;
        }
    }
}
