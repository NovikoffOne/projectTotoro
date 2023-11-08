using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private Image _imageFiller;
    [SerializeField] private TMP_Text _textValue;

    public void SetValue(float valueNormalized)
    {
        this._imageFiller.fillAmount = valueNormalized;

        var valueInPercent = Mathf.RoundToInt(valueNormalized * 100);
        this._textValue.text = $"{valueInPercent}%";
    }
}
