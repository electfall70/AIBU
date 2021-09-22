using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private BiomeSetting biomeSetting;

    [SerializeField] private List<BiomeSetting> biomeSettings;

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

    Color32[] colors32;

    [ContextMenu("Generate")]
    public void GenerateMap()
    {
        colors32 = GenerateBiomeMap();

        float[] noiseRed = GenerateNoise(biomeSettings[0], Channel.R);
        float[] noiseGreen = GenerateNoise(biomeSettings[1], Channel.G);
        float[] noiseBlue = GenerateNoise(biomeSettings[2] ,Channel.B);

        float[] noise = new float[chunkSize * chunkSize];
        for(int x = 0; x < chunkSize * chunkSize; x++)
        {
            noise[x] = noiseRed[x] + noiseGreen[x] + noiseBlue[x];
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
       
        mesh.colors32 = colors32;
        MeshFilter.sharedMesh = mesh;
        MeshCollider.sharedMesh = mesh;
    }

    public enum Channel
    {
        R,G,B
    }

    private float[] GenerateNoise(BiomeSetting biomeSetting, Channel c)
    {
        FastNoiseLite noiseGenerator = new FastNoiseLite();

        noiseGenerator.SetSeed(randomSeed ? Random.Range(-1000000, 1000000) : seed.GetHashCode());
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
                float biomeStrength = 0;
                Color currentColor = colors32[i];
                switch (c)
                {
                    case Channel.R: 
                        biomeStrength = currentColor.r;
                        break;
                    case Channel.G:
                        biomeStrength = currentColor.g;
                        break;
                    case Channel.B:
                        biomeStrength = currentColor.b;
                        break;
                }
                noise[i++] += noiseGenerator.GetNoise(x, y) * biomeSetting.maxHeight * biomeStrength;
            }
        }
        return noise;
    }

    public Color32[] GenerateBiomeMap()
    {
        Color32[] colors32 = new Color32[chunkSize * chunkSize];

        FastNoiseLite biomeNoiseGenerator = new FastNoiseLite();
        biomeNoiseGenerator.SetSeed(randomSeed ? Random.Range(-1000000, 1000000) : seed.GetHashCode());
        biomeNoiseGenerator.SetNoiseType(biomeSetting.noiseType);
        biomeNoiseGenerator.SetFrequency(biomeSetting.noiseFrequency);
        biomeNoiseGenerator.SetFractalType(biomeSetting.fractalType);
        biomeNoiseGenerator.SetFractalOctaves(biomeSetting.octaves);
        biomeNoiseGenerator.SetFractalLacunarity(biomeSetting.lacunarity);
        biomeNoiseGenerator.SetFractalGain(biomeSetting.gain);
        biomeNoiseGenerator.SetFractalWeightedStrength(biomeSetting.weightedStrength);

        for (int e = 0, z = 0; z < chunkSize; z++)
        {
            for (int x = 0; x < chunkSize; x++)
            {
                biomeNoiseGenerator.SetFractalType(biomeSetting.fractalType);

                float test = biomeNoiseGenerator.GetNoise(x, z);
                float test1 = Mathf.SmoothStep(1, 0, test);
                float test2 = Mathf.SmoothStep(0, 1f, test);
               biomeNoiseGenerator.SetFractalType(FastNoiseLite.FractalType.PingPong);

                float teste = biomeNoiseGenerator.GetNoise(-x, -z);
                float test3 = Mathf.SmoothStep(0, 1f, teste);

                colors32[e++] = new Color32((byte)(255 * test1), (byte)(255 * test2), (byte)(255 * test3), 255);
            }
        }
        return colors32;
    }
}