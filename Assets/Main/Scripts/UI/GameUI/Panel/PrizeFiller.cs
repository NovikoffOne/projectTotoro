using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrizeFiller : MonoBehaviour
{
    [SerializeField] private TMP_Text _point;
    [SerializeField] private List<GameObject> _stars;

    private void OnDisable()
    {
        _stars.ForEach(star => star.SetActive(false));
    }

    public void Fill(int star, int point)
    {
        DrawStar(star);
        DrawPoint(point);
    }

    private void DrawStar(int countStars)
    {
        for (int i = 0; i < countStars; i++)
            _stars[i].SetActive(true);
    }

    private void DrawPoint(int point)
    {
        _point.text = point.ToString();
    }
}
