using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScreenManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip transitionAnimation;

    private void Awake()
    {
        animator.Play("Start");
    }

    public void PlayEnd(string newScene)
    {
        animator.Play("End");

        StartCoroutine(Routine(newScene));
    }

    private IEnumerator Routine(string newScene)
    {
        yield return new WaitForSeconds(transitionAnimation.length);

        SceneManager.LoadScene(newScene);
    }
}