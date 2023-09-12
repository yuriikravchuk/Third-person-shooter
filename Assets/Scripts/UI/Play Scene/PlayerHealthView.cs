using TMPro;
using UnityEngine;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Init(int maxValue)
    {
        _text.text = $"Health: {maxValue}";
    }

    public void OnValueChanged(float value)
    {
        _text.text = $"Health: {value}";
    }
}
