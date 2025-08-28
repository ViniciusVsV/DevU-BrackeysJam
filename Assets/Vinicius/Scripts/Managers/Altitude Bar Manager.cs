using UnityEngine;

public class AltitudeBarManager : MonoBehaviour
{
    private Animator animator;
    private int currentSide;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        currentSide = -1;
    }

    private void Start()
    {
        animator.Play("Activate");
    }

    private void ChangeSide()
    {
        if (currentSide == 1)
            animator.Play("Change to Left");
        else
            animator.Play("Change to Right");

        currentSide *= -1;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Boss"))
            ChangeSide();    
    }
}