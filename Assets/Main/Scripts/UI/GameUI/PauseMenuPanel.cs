using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

internal class PauseMenuPanel : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _reloadButton;

    public Button PlayButton => _playButton;
    public Button CloseButton => _closeButton;
    public Button ReloadButton => _reloadButton;
}
