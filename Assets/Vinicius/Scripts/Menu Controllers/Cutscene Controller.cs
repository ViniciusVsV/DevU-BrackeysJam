using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    private TransitionScreenManager transitionScreenManager;

    [SerializeField] private CinemachineCamera[] cinemachineCameras;
    [SerializeField] private float imageDuration;
    [SerializeField] private float endDelay;

    private bool isEnding;

    void Start()
    {
        transitionScreenManager = FindFirstObjectByType<TransitionScreenManager>();

        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        for (int i = 0; i < cinemachineCameras.Length; i++)
        {
            cinemachineCameras[i].Priority = i + 2;

            yield return new WaitForSeconds(imageDuration);
        }

        if (!isEnding)
            End();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Menu))
        {
            if (isEnding)
                return;

            End();
        }
    }

    private void End()
    {
        isEnding = true;

        AudioController.Instance.PlayGameMusic();

        StartCoroutine(EndRoutine());
    }

    private IEnumerator EndRoutine()
    {
        yield return new WaitForSeconds(endDelay);

        transitionScreenManager.PlayEnd("Vinicius");
    }
}
