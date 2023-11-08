using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class StarFillerGameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _point;
    [SerializeField] private List<GameObject> _stars;

    private void OnDisable()
    {
        _stars.ForEach(star => star.SetActive(false));
    }

    public void FillStars(int star, int point)
    {
        DrawStars(star);
        DrawPoint(point);
    }

    private void DrawStars(int countStars)
    {
        for (int i = 0; i < countStars; i++)
        {
            _stars[i].SetActive(true);
        }
    }

    private void DrawPoint(int point)
    {
        _point.text = point.ToString();
    }
}
