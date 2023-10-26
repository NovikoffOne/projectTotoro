using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour, IPanel
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private List<TMP_Text> _texts = new List<TMP_Text>();
    
    private List<Button> _buttons = new List<Button>();

    public Button NextButton => _nextButton;
    public List<Button> Buttons => _buttons;
    public List<TMP_Text> Texts => _texts;

    private void Awake()
    {
        _buttons.Add(_nextButton);
    }

    public void SetActiveText(TMP_Text text)
    {
        text.gameObject.SetActive(true);
    }
}
