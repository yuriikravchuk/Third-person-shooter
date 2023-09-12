using UnityEngine;

public class PlaySceneUI : MonoBehaviour
{
    [SerializeField] private GameObject _playUI;
    [SerializeField] private GameObject _settingsUI;

    public void ShowPlayUI() => _playUI.SetActive(true);

    public void HidePlayUI() => _playUI.SetActive(false);
    public void ShowSettings() => _settingsUI.SetActive(true);
    public void HideSettings() => _settingsUI.SetActive(false);
}
