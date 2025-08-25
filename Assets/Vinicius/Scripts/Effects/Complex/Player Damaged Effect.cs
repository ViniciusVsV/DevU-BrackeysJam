using UnityEngine;

public class PlayerDamagedEffect : MonoBehaviour
{
    public static PlayerDamagedEffect Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyEffect()
    {
        //hitstop
        //balanço de camera
        //efeito sonoro
        //sprite flash
    }
}