using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMap
{
	public static float[,] GenerateNoiseMap(int mapWidth, int mapLength, float scale, int seed, int octaves, 
		float persistence, float lacunarity, Vector2 offset)
	{

		System.Random prng = new System.Random(seed);
		Vector2[] octaveOffset = new Vector2[octaves];
		for (int i = 0; i < octaves; i++)
		{
			float offsetX = prng.Next(-100000, 100000)+offset.x;
			float offsetZ = prng.Next(-100000, 100000) + offset.y;
			octaveOffset[i] = new Vector2(offsetX, offsetZ);
		}

		if (scale <= 0)
			scale = 0.0001f;

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;
		float halfWidth = mapWidth / 2f;
		float halfLength = mapLength / 2f;


		float[,] noiseMap = new float[mapWidth, mapLength];

		for (int x = 0; x < mapWidth; x++)
		{
			for (int z = 0; z < mapLength; z++)
			{
				float amplitude = 1;
				float frequency = 1;

				float noiseHeight = 0;

				for (int i = 0; i < octaves; i++)
				{
					float perlinValue = Mathf.PerlinNoise((x - halfWidth) / scale * frequency + octaveOffset[i].x,
															(z - halfWidth) / scale * frequency + octaveOffset[i].y);
					noiseHeight += perlinValue * amplitude * 2 - 1;

					amplitude *= persistence;

					frequency *= lacunarity;
				}
				if (noiseHeight > maxNoiseHeight)
					maxNoiseHeight = noiseHeight;
				if (noiseHeight < minNoiseHeight)
					minNoiseHeight = noiseHeight;
					
				noiseMap[x, z] = noiseHeight;
			}
			
		}

		for (int x = 0; x < mapWidth; x++)
		{
			for (int z = 0; z < mapLength; z++)
			{
				noiseMap[x, z] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, z]);
			}
		}

				return noiseMap;
	}
}
