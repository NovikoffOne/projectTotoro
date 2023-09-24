using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

internal class GameOverPanel : MonoBehaviour 
{
    [SerializeField] private Button _exitMenuButton;
    [SerializeField] private Button _reloadButton;

    public Button ExitMenuButton => _exitMenuButton;
    public Button ReloadButton => _reloadButton;
}
