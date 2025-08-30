using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class PadraoDeAtaqueFoguetes
{
    public string nomeDoPadrao;
    public List<Transform> pontosDeSpawn;
}

public class AtaqueFoguetes : BaseState
{
    [Header("Configuração do Foguete")]
    [SerializeField] private GameObject prefabFoguete;

    [Header("Padrões de Ataque")]
    [SerializeField] private List<PadraoDeAtaqueFoguetes> padroesDeAtaque;

    [SerializeField] float endDelay;

    public override void StateEnter()
    {
        if (padroesDeAtaque == null || padroesDeAtaque.Count == 0)
        {
            return;
        }

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

    private IEnumerator Routine()
    {
        yield return new WaitForSeconds(endDelay);
        controller.SetIdleState();
    }

}