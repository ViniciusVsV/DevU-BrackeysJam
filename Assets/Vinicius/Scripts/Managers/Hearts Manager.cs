using System.Collections;
using UnityEngine;

public class HeartsManager : MonoBehaviour
{
    [SerializeField] private Animator[] heartAnimators;
    [SerializeField] private float activationDelay;

    private void Start()
    {
        StartCoroutine(ActivateRoutine());
    }

    private IEnumerator ActivateRoutine()
    {
        foreach (var animator in heartAnimators)
        {
            animator.Play("Activate");
            yield return new WaitForSeconds(activationDelay);
        }
    }

    public void DeactivateHeart(int index)
    {
        heartAnimators[index].Play("Deactivate");
    }

    public void ActivateHeart(int index)
    {
        heartAnimators[index].Play("Activate");
    }

    public void DeactivateAll()
    {
        foreach (var animator in heartAnimators)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (!stateInfo.IsName("Deactivate"))
                animator.Play("Deactivate");
        }
    }

}