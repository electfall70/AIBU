using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GeneratorSetting
{
    public float scaleX, scaleZ;
    public float minHeight, maxHeight;
    public bool ridge;
}

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider), typeof(MeshRenderer))]
public class TerrainGenerator : MonoBehaviour
{
    [Header("Generation Settings")]
    [SerializeField] [Range(1, 256)] private int chunkSize;
    [SerializeField] private int scale = 1;
    [SerializeField] private string seed;
    [SerializeField] private List<GeneratorSetting> terrainSettings;
    [SerializeField] private bool useFalloff;
    [SerializeField] private float falloffStrength;


    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    public MeshFilter MeshFilter
    {
        get
        {
            if (meshFilter == null)
            {
                meshFilter = GetComponent<MeshFilter>();
            }
            return meshFilter;
        }
    }

    public MeshCollider MeshCollider 
    {
        get
        {
            if (meshCollider == null)
            {
                meshCollider = GetComponent<MeshCollider>();
            }
            return meshCollider;
        }
    }

    [ContextMenu("Generate")]
    public void GenerateMap()
    {
        float[] noise = NoiseGenerator.GenerateNoiseMap(chunkSize, seed.GetHashCode(), terrainSettings);
        float[] falloff = FalloffGenerator.GenerateFalloffMap(chunkSize);

        for(int z = 0; z < chunkSize; z++)
        {
            for (int x = 0; x < chunkSize; x++)
            {
                if (useFalloff)
                {
                    noise[z * chunkSize + x] = noise[z * chunkSize + x] - falloff[z * chunkSize + x] * falloffStrength;
                }
            }
        }

        Mesh mesh = MeshGenerator.GenerateTerrainMesh(chunkSize, scale, noise).GenerateMesh();
        MeshFilter.sharedMesh = mesh;
        MeshCollider.sharedMesh = mesh;
    }
}