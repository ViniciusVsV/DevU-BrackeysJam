using UnityEngine.Splines;
using Unity.Mathematics;

using UnityEngine;
using System; // Para o evento Action

public class SplineFall : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private SplineContainer splineToFollow; 
    [SerializeField] private Transform objectToMove; 

    [Header("Configurações da Queda")]
    [SerializeField] private float fallDuration; 

    private float elapsedTime = 0f;
    private bool isFallCompleted = false;

    void Awake()
    {
        if (splineToFollow == null)
        {
            this.enabled = false;
        }
    }

    void Update()
    {
        if (isFallCompleted || objectToMove == null) return;

        elapsedTime += Time.deltaTime;
        float progress = math.clamp(elapsedTime / fallDuration, 0, 1);

        // position = X Y Z
        splineToFollow.Evaluate(progress, out float3 position, out float3 tangent, out float3 upVector);

        objectToMove.position = position;
        

        if (progress >= 1.0f)
        {
            isFallCompleted = true;
        }
    }
}