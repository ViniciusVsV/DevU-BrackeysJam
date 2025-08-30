using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private TransitionScreenManager transitionScreenManager;

    void Start()
    {
        transitionScreenManager = FindFirstObjectByType<TransitionScreenManager>();
    }

    public void Play()
    {
        transitionScreenManager.PlayEnd("Vinicius");
    }

    public void Exit()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}