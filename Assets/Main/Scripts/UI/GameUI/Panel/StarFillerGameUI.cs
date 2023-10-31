using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class StarFillerGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _star1;
    [SerializeField] private GameObject _star2;
    [SerializeField] private GameObject _star3;

    [SerializeField] private TMP_Text _point;

    public void FillStars(int index, int point)
    {
        DrawStars(index);
        DrawPoint(point);
    }

    private void DrawStars(int countStars)
    {
        if (countStars > 0)
            _star1.SetActive(true);

        if (countStars > 1)
            _star2.SetActive(true);

        if (countStars > 2)
            _star3.SetActive(true);
    }

    private void DrawPoint(int point)
    {
        _point.text = point.ToString();
    }
}
