using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RocketAttack : BaseState
{
    [Header("-----Prefabs-----")]
    [SerializeField] private GameObject prefabFoguete;

    [Header("-----Patterns-----")]
    [SerializeField] private List<PadraoDeAtaqueFoguetes> padroesDeAtaque;

    [SerializeField] float startDelay;
    [SerializeField] float endDelay;

    [Header("-----Timing-----")]
    [SerializeField] private float warningDuration;
    [SerializeField] private float blinkingFrequency;

    public override void StateEnter()
    {
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        if (padroesDeAtaque == null || padroesDeAtaque.Count == 0)
            yield break;

        int indiceAleatorio = Random.Range(0, padroesDeAtaque.Count);
        PadraoDeAtaqueFoguetes padraoEscolhido = padroesDeAtaque[indiceAleatorio];

        List<GameObject> warningsPadrao = new List<GameObject>();

        foreach (Transform pontoDeSpawn in padraoEscolhido.pontosDeSpawn)
        {
            if (pontoDeSpawn.childCount > 0)
            {
                GameObject aviso = pontoDeSpawn.GetChild(0).gameObject;
                warningsPadrao.Add(aviso);
            }
        }

        float temp = warningDuration;
        float cronometro = 0f;
        while (cronometro < temp)
        {
            foreach (GameObject aviso in warningsPadrao)
            {
                aviso.SetActive(!aviso.activeSelf);
            }

            yield return new WaitForSeconds(blinkingFrequency);
            temp -= blinkingFrequency;
        }

        

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

        StartCoroutine(EndRoutine());
    }



    public override void StateExit()
    {
        controller.canTakeDamage = true;
    }


    private IEnumerator EndRoutine()
    {
        yield return new WaitForSeconds(endDelay);

        controller.SetDashState();
    }
}