using UnityEngine;

public class BossDamagedEffect : MonoBehaviour
{
    public static BossDamagedEffect Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyEffect(GameObject gameObject)
    {
        SpriteFlash.Instance.ApplyEffect(gameObject);
    }
}