using UnityEngine;

public class BossKilledEffect : MonoBehaviour
{
    public static BossKilledEffect Instance;
    private TransitionScreenManager transitionScreenManager;

    private void Awake()
    {
        Instance = this;

        transitionScreenManager = FindFirstObjectByType<TransitionScreenManager>();
    }

    public void ApplyEffect()
    {
        CameraShake.Instance.ApplyEffect(false, Vector2.zero);
    }
}