using UnityEngine;

public class AltitudeBarManager : MonoBehaviour
{
    [SerializeField] private Animator altitudeBarAnimator;
    private int currentSide;

    private bool isDeactivated;

    private void Awake()
    {
        currentSide = -1;
    }

    private void Start()
    {
        altitudeBarAnimator.Play("Activate");
    }

    private void ChangeSide()
    {
        if (currentSide == 1)
            altitudeBarAnimator.Play("Change to Left");
        else
            altitudeBarAnimator.Play("Change to Right");

        currentSide *= -1;
    }

    public void Deactivate()
    {
        isDeactivated = true;

        if (currentSide == 1)
            altitudeBarAnimator.Play("Deactivate Right");
        else
            altitudeBarAnimator.Play("Deactivate Left");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDeactivated)
            return;

        if (other.CompareTag("Boss"))
            ChangeSide();
    }
}