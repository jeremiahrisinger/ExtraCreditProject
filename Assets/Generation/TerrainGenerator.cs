using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    [SerializeField] int width = 56;
    [SerializeField] int length = 56;
    [SerializeField] int depth = 20;
    [SerializeField] float scale = 0.5f;
    
    
    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);


    }

    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, length);
       // terrainData.SetHeights(0, 0, NoiseMap.GenerateNoiseMap(width, length, scale));
        return terrainData;
    }
}
