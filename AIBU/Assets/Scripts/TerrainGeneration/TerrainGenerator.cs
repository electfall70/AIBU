using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GeneratorSetting
{
    public float scaleX, scaleZ;
    public float minHeight, maxHeight;
    public bool ridged;
}

[System.Serializable]
public struct BiomeSetting
{
    public float maxHeight;
    public FastNoiseLite.NoiseType noiseType;
    public float noiseFrequency;
    public FastNoiseLite.FractalType fractalType;
    public int octaves;
    public float lacunarity;
    public float gain;
    public float weightedStrength;
}

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider), typeof(MeshRenderer))]
public class TerrainGenerator : MonoBehaviour
{
    [Header("Generation Settings")]
    [SerializeField] [Range(1, 256)] private int chunkSize;
    [SerializeField] private int scale = 1;
    [SerializeField] private string seed;
    [SerializeField] private bool randomSeed;

    [SerializeField] private List<GeneratorSetting> terrainSettings;
    [SerializeField] private BiomeSetting biomeSetting;

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
        //float[] noise = NoiseGenerator.GenerateNoiseMap(chunkSize, seed.GetHashCode(), terrainSettings);

        FastNoiseLite noiseGenerator = new FastNoiseLite();
        noiseGenerator.SetSeed(randomSeed ? Random.Range(-1000000, 1000000): seed.GetHashCode());
        //noiseGenerator.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        //noiseGenerator.SetFrequency(0.015f);
        //noiseGenerator.SetFractalWeightedStrength(.25f);
        //noiseGenerator.SetFractalType(FastNoiseLite.FractalType.Ridged);

        noiseGenerator.SetNoiseType(biomeSetting.noiseType);
        noiseGenerator.SetFrequency(biomeSetting.noiseFrequency);
        noiseGenerator.SetFractalType(biomeSetting.fractalType);
        noiseGenerator.SetFractalOctaves(biomeSetting.octaves);
        noiseGenerator.SetFractalLacunarity(biomeSetting.lacunarity);
        noiseGenerator.SetFractalGain(biomeSetting.gain);
        noiseGenerator.SetFractalWeightedStrength(biomeSetting.weightedStrength);

        float[] noise = new float[chunkSize * chunkSize];
        int i = 0;
        for (float y = 0; y < chunkSize; y++)
        {
            for (float x = 0; x < chunkSize; x++)
            {
                noise[i++] = noiseGenerator.GetNoise(x, y)* biomeSetting.maxHeight;
            }
        }

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