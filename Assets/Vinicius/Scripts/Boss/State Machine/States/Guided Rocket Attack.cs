using System.Collections;
using UnityEngine;

public class GuidedRocketAttack : BaseState
{
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float numberOfRockets;
    [SerializeField] private float exitDelay;
    [SerializeField] private float spawnDelay;

    public override void StateEnter()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        bool[] usedPoints = new bool[spawnPoints.Length];

        for (int i = 0; i < numberOfRockets; i++)
        {
            int randomPoint;

            do
            {
                randomPoint = Random.Range(0, spawnPoints.Length);
            } while (usedPoints[randomPoint]);

            usedPoints[randomPoint] = true;

            Instantiate(rocketPrefab, spawnPoints[randomPoint].position, Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);
        }

        yield return new WaitForSeconds(exitDelay);

        controller.SetIdleState();
    }
}