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

        transitionScreenManager.PlayEnd("Victory Screen");
    }
}