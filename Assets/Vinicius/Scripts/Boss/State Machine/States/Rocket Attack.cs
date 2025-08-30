using System.Collections;
using UnityEngine;

public class RocketAttack : BaseState
{
    public override void StateEnter()
    {
        //Escolhe uma direção aleatória 

        StartCoroutine(a());
    }

    private IEnumerator a()
    {
        yield return new WaitForSeconds(3f);

        controller.SetDashState();
    }

    public override void StateExit()
    {
        controller.canTakeDamage = true;
    }
}