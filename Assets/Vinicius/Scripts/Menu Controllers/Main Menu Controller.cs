using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private PlaneBehaviour planeBehaviour;

    [SerializeField] private Canvas canvas;

    void Start()
    {
        planeBehaviour = FindFirstObjectByType<PlaneBehaviour>();

        AudioController.Instance.PlayMenuMusic();
    }

    public void Play()
    {
        canvas.renderMode = RenderMode.WorldSpace;

        planeBehaviour.TakeOff();
    }

    public void Exit()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}