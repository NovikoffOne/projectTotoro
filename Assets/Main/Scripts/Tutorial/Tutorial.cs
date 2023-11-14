using UnityEngine;

public class Tutorial : MonoBehaviour,
    IEventReceiver<TutorialStateChanged>
{
    [SerializeField] private Light _pointLightPrefab;

    private Light _pointLight;
    
    private StateMachine _stateMachine;
    
    public Light PointLight => _pointLight;
    public Vector3 StartLightPosition { get; } = new Vector3(3, 1, -.3f);
    public Vector3 State3LightPosition { get; } = new Vector3(7, 1, -.3f);
    public Vector3 State5LightPosition { get; } = new Vector3(7, 2, -.3f);

    private void Awake()
    {
        if(_stateMachine == null)
        {
            _stateMachine = new StateMachine();
        }
    }

    private void Start()
    {
        this.Subscribe<TutorialStateChanged>();
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
        this.Unsubscribe<TutorialStateChanged>();
    }

    public void OnEvent(TutorialStateChanged var)
    {
        if (var.IsTutorial == false)
        {
            _stateMachine.ChangeState<OffTutorialState>(state => state.Target = this);
            return;
        }
        else
        {
            switch (var.TutorialState)
            {
                case 0:
                    _stateMachine.ChangeState<StartTutorialState>(state => state.Target = this);
                    break;

                case 3:
                    _stateMachine.ChangeState<LoadingTutorialState>(state => state.Target = this);
                    break;

                case 5:
                    _stateMachine.ChangeState<LevelTransitionTutorialState>(state => state.Target = this);
                    break;

                default:
                    break;
            }
        }
    }

    public void CreatePointLigth()
    {
        _pointLight = Instantiate(_pointLightPrefab, StartLightPosition, Quaternion.identity);
    }
}