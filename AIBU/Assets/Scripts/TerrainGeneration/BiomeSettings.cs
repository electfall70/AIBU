using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Biome Setting")]
public class BiomeSettings : ScriptableObject
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
