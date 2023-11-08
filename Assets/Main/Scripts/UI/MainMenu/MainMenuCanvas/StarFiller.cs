using TMPro;
using UnityEngine;

public class StarFiller : MonoBehaviour
{
    [SerializeField] private GameObject _star1;
    [SerializeField] private GameObject _star2;
    [SerializeField] private GameObject _star3;

    [SerializeField] private LockImage _lock;
    [SerializeField] private GameObject _levelPanel;

    [SerializeField] private TMP_Text _text;

    private const int TrueInt = 1;

    private int _countStar1 = 1;
    private int _countStar2 = 2;

    private string _levelStarText = "LevelStar ";
    private string _levelPassedText = "LevelPassed ";

    public void FiilStars(int index)
    {
        if (index == 0 && PlayerPrefs.HasKey(_levelStarText + index) == true)
        {
            DrawStars(PlayerPrefs.GetInt(_levelStarText + index));
            return;
        }
        else if (index == 0 && PlayerPrefs.HasKey(_levelStarText + index) == false)
        {
            DrawStars(0);
            return;
        }

        var key = PlayerPrefs.GetInt(_levelPassedText + (index - 1));

        switch (key)
        {
            case TrueInt:
                DrawStarHandler(index);
                break;

            case 0:
                _levelPanel.SetActive(false);
                _text.gameObject.SetActive(false);
                break;

            default:
                _levelPanel.SetActive(false);
                _text.gameObject.SetActive(false);
                break;
        }
    }

    private void DrawStarHandler(int index)
    {
        _levelPanel.SetActive(true);
        _lock.HidePanel();

        if (PlayerPrefs.HasKey(_levelStarText + index))
            DrawStars(PlayerPrefs.GetInt(_levelStarText + index));
        else
            DrawStars(0);
    }

    private void DrawStars(int countStars)
    {
        _lock.HidePanel();

        if (countStars > 0)
            _star1.SetActive(true);

        if (countStars > _countStar1)
            _star2.SetActive(true);

        if (countStars > _countStar2)
            _star3.SetActive(true);
    }
}
