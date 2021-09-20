using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGenerator
{
    public static float[] GenerateNoiseMap(int chunkSize, int seed, List<GeneratorSetting> terrainSettings)
    {
        float[] noiseMap = new float[chunkSize * chunkSize];
        System.Random prng = new System.Random(seed);
        for (int z = 0; z < chunkSize; z++)
        {
            for (int x = 0; x < chunkSize; x++)
            {
                noiseMap[z * chunkSize + x] = CalcHeight(x, z, terrainSettings,  prng);             
            }
        }
        return noiseMap;
    }

    private static float CalcHeight(int x, int z, List<GeneratorSetting> terrainSettings, System.Random prng)
    {
        float y = 0;
        foreach (GeneratorSetting setting in terrainSettings)
        {
            y += CalcHeight(x, z, setting, prng);
        }
        return y;
    }

    private static float CalcHeight(int x, int z, GeneratorSetting setting, System.Random prng)
    {
        float xCoord = x * setting.scaleX;
        float zCoord = z * setting.scaleZ;
        float noise = Mathf.PerlinNoise(xCoord, zCoord);
        float y = Mathf.Lerp(setting.minHeight, setting.maxHeight, noise);
        if (setting.ridge) y = 1 - Mathf.Abs(y);

        return y;
    }
}
