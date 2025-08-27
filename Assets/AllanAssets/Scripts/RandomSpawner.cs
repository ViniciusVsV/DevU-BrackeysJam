using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public enum PosicaoDoSpawner { Cima, Baixo, Esquerda, Direita }
    public GameObject prefab;

    private BoxCollider2D areaDeSpawn;

    // Limite do spawn é o box collider
    private Bounds bounds;
    private Vector2 posicaoSpawn;
    private Vector2 direcaoMovimento;

    private float posY;
    private float posX;

    [SerializeField] private PosicaoDoSpawner posSpawner;

    [SerializeField] private float intervaloInicial = 3.0f;
    [SerializeField] private float intervaloMinimo = 0.5f;
    [SerializeField] private float fatorDeDificuldade = 0.98f;
    private float cronometroParaSpawn;

    void Awake()
    {
        areaDeSpawn = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        cronometroParaSpawn = intervaloInicial;
    }

    void Update()
    {
        cronometroParaSpawn -= Time.deltaTime;

        if (cronometroParaSpawn <= Mathf.Epsilon)
        {
            Spawn();

            float tempoDecorrido = Time.timeSinceLevelLoad;

            float intervaloVariavel = (intervaloInicial - intervaloMinimo) * Mathf.Pow(fatorDeDificuldade, tempoDecorrido);
            float intervaloAtual = intervaloMinimo + intervaloVariavel;

            cronometroParaSpawn = intervaloAtual;
        }
    }

    void Spawn()
    {
        bounds = areaDeSpawn.bounds;

        direcaoMovimento = Vector2.zero;

        if(posSpawner == PosicaoDoSpawner.Cima || posSpawner == PosicaoDoSpawner.Baixo)
        {
            posX = Random.Range(bounds.min.x, bounds.max.x);
            posicaoSpawn = new Vector2(posX, transform.position.y);
        }
        else
        {
            posY = Random.Range(bounds.min.y, bounds.max.y);
            posicaoSpawn = new Vector2(transform.position.x, posY);
        }

         switch (posSpawner)
            {
            case PosicaoDoSpawner.Cima:
                direcaoMovimento = Vector2.down;
                break;
            case PosicaoDoSpawner.Baixo:
                direcaoMovimento = Vector2.up;
                break;
            case PosicaoDoSpawner.Direita:
                direcaoMovimento = Vector2.left;
                break;
            case PosicaoDoSpawner.Esquerda:
                direcaoMovimento = Vector2.right;
                break;

            }

        GameObject novoInimigo = Instantiate(prefab, posicaoSpawn, Quaternion.identity);

        MovimentoInimigo scriptDoInimigo = novoInimigo.GetComponent<MovimentoInimigo>();

        if (scriptDoInimigo != null)
        {
            scriptDoInimigo.direcaoMovimento = direcaoMovimento;
        }
    }
}
