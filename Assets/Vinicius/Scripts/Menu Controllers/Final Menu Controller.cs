using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalMenuController : MonoBehaviour
{
    private TransitionScreenManager transitionScreenManager;

    private void Awake()
    {
        transitionScreenManager = FindFirstObjectByType<TransitionScreenManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            transitionScreenManager.PlayEnd("Main Menu");
    }
}