using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlaneBehaviour : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;

    [SerializeField] private float xSpeed;
    private float ySpeed;

    [SerializeField] private float xAcceleration;
    [SerializeField] private float yAcceleration;
    [SerializeField] private float takeOffDuration;

    [SerializeField] private float transitionDelay;

    private Vector2 moveDirection;

    private TransitionScreenManager transitionScreenManager;

    private void Start()
    {
        transitionScreenManager = FindFirstObjectByType<TransitionScreenManager>();

        moveDirection = Vector2.right;
    }

    private void Update()
    {
        Vector2 movement = new Vector2(xSpeed * moveDirection.x, ySpeed * moveDirection.y);

        transform.Translate(Time.deltaTime * movement);
    }

    public void TakeOff()
    {
        moveDirection = new Vector2(1, 1);

        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < takeOffDuration)
        {
            xSpeed += xAcceleration * Time.deltaTime;
            ySpeed += yAcceleration * Time.deltaTime;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        cinemachineCamera.Follow = null;

        yield return new WaitForSeconds(transitionDelay);

        transitionScreenManager.PlayEnd("Cutscene");
    }
}
