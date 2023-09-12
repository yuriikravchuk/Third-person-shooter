using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _resumeButton;

    public event UnityAction ResumeButtonClicked
    {
        add => _resumeButton.onClick.AddListener(value);
        remove => _resumeButton.onClick.RemoveListener(value);
    }

    private void Awake()
    {
        _exitButton.onClick.AddListener(Application.Quit);
        _mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene(0));
    }
}
