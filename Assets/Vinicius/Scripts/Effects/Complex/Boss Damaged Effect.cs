using UnityEngine;

public class BossDamagedEffect : MonoBehaviour
{
    public static BossDamagedEffect Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyEffect(GameObject newObject)
    {
        SpriteFlash.Instance.ApplyEffect(newObject);
    }
}