using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RocketAttack : BaseState
{
    [Header("-----Prefabs-----")]
    [SerializeField] private GameObject prefabFoguete;

    [Header("-----Patterns-----")]
    [SerializeField] private List<PadraoDeAtaqueFoguetes> padroesDeAtaque;

    [SerializeField] float endDelay;

    public override void StateEnter()
    {
        if (padroesDeAtaque == null || padroesDeAtaque.Count == 0)
            return;

        int indiceAleatorio = Random.Range(0, padroesDeAtaque.Count);
        PadraoDeAtaqueFoguetes padraoEscolhido = padroesDeAtaque[indiceAleatorio];

        foreach (Transform pontoDeSpawn in padraoEscolhido.pontosDeSpawn)
        {
            if (pontoDeSpawn != null)
            {
                GameObject fogueteInstanciado = Instantiate(prefabFoguete, pontoDeSpawn.position, pontoDeSpawn.rotation);

                MovimentoInimigo scriptMovimento = fogueteInstanciado.GetComponent<MovimentoInimigo>();

                if (scriptMovimento != null)
                {
                    scriptMovimento.direcaoMovimento = pontoDeSpawn.up;
                }
            }
        }

        StartCoroutine(Routine());
    }

    public override void StateExit()
    {
        controller.canTakeDamage = true;
    }

    private IEnumerator Routine()
    {
        yield return new WaitForSeconds(endDelay);

        controller.SetDashState();
    }
}