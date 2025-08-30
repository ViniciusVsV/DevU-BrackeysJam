using UnityEngine;
using TMPro; // Não esqueça de importar o TMPro

public class AmmoCounterManager : MonoBehaviour
{
    [SerializeField] private Animator ammoDisplayAnimator;
    [SerializeField] private TextMeshProUGUI ammoText;
    private PlayerWeapon playerWeapon;

    private void Awake()
    {
        playerWeapon = FindFirstObjectByType<PlayerWeapon>();
    }

    private void Start()
    {
        ammoDisplayAnimator.Play("Activate");
    }

    private void Update()
    {
        if (playerWeapon.remainingRounds <= 0)
            ammoText.text = "";
        else
            ammoText.text = playerWeapon.remainingRounds.ToString();
    }

    public void Deactivate()
    {
        ammoDisplayAnimator.Play("Deactivate");
    }
}