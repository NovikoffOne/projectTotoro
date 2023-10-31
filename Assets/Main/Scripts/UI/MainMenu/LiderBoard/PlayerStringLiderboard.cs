using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class PlayerStringLiderboard : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _rank;

    public TMP_Text PlayerName => _name;
    public TMP_Text PlayerScore => _score;
    public TMP_Text PlayerRank => _rank;
}
