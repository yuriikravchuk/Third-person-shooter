using TMPro;
using UnityEngine;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Init(int maxValue) => SetValue(maxValue);

    public void SetValue(int value) => _text.text = $"Health: {value}";
}
