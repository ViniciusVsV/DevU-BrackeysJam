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


    void Awake()
    {
        areaDeSpawn = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }
    }

    void Spawn()
    {
        bounds = areaDeSpawn.bounds;

        direcaoMovimento = Vector2.zero;

        switch (posSpawner)
        {
            case PosicaoDoSpawner.Cima:
                posX = Random.Range(bounds.min.x, bounds.max.x);
                posicaoSpawn = new Vector2(posX, transform.position.y);
                direcaoMovimento = Vector2.down;
                break;
            case PosicaoDoSpawner.Baixo:
                posX = Random.Range(bounds.min.x, bounds.max.x);
                posicaoSpawn = new Vector2(posX, transform.position.y);
                direcaoMovimento = Vector2.up;
                break;
            case PosicaoDoSpawner.Direita:
                posY = Random.Range(bounds.min.y, bounds.max.y);
                posicaoSpawn = new Vector2(transform.position.x, posY);
                direcaoMovimento = Vector2.left;
                break;
            case PosicaoDoSpawner.Esquerda:
                posY = Random.Range(bounds.min.y, bounds.max.y);
                posicaoSpawn = new Vector2(transform.position.x, posY);
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
