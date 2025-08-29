using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerRumble : MonoBehaviour
{
    public static ControllerRumble Instance;

    private Gamepad gamepad;
    private PlayerController playerController;

    private Coroutine coroutine;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }

    public void ApplyEffect(float lowFrequency, float highFrequency, float duration)
    {
        if (!playerController.isOnController)
            return;

        gamepad = Gamepad.current;

        if (gamepad != null)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(Routine(lowFrequency, highFrequency, duration));
        }
    }

    private IEnumerator Routine(float lowFrequency, float highFrequency, float duration)
    {
        gamepad.SetMotorSpeeds(lowFrequency, highFrequency);

        yield return new WaitForSeconds(duration);

        gamepad.SetMotorSpeeds(0f, 0f);
    }
}