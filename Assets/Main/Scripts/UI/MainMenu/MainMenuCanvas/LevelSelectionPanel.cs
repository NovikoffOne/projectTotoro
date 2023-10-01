using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionPanel : MonoBehaviour, IPanel
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private List<Button> _buttons;

    public Button CloseButton => _closeButton;
    public List<Button> Buttons => _buttons;
}
