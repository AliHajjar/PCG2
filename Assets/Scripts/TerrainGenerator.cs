using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public Terrain terrain;
    public int terrainWidth = 800;  // Default width
    public int terrainLength = 800; // Default length
    public float terrainHeight = 0.01f;  // Default height
    public float detailDensity = 0.3f; // Default density of vegetation

    void Start()
    {
        terrain = Terrain.activeTerrain;
        if (terrain != null)
        {
            terrain.terrainData = GenerateTerrain(terrain.terrainData);
            AddVegetation();
            terrain.transform.position = new Vector3(-150f, -0.1f, -300f);
        }
        else
        {
            Debug.LogError("Terrain not found");
        }
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = terrainWidth + 1;
        terrainData.size = new Vector3(terrainWidth, terrainHeight, terrainLength);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[terrainWidth, terrainLength];
        for (int x = 0; x < terrainWidth; x++)
        {
            for (int y = 0; y < terrainLength; y++)
            {
                heights[x, y] = Mathf.PerlinNoise(x * .1f, y * .1f) * .5f;
            }
        }
        return heights;
    }

    void AddVegetation()
    {
        for (int x = 0; x < terrainWidth; x++)
        {
            for (int y = 0; y < terrainLength; y++)
            {
                // Lower the chance for placing vegetation in each iteration
                bool placeTree = Random.Range(0.0f, 1.0f) < (detailDensity * 0.07f);
                bool placeMushroom = Random.Range(0.0f, 1.0f) < (detailDensity * 0.05f);

                float unitX = (float)x / terrainWidth;
                float unitY = (float)y / terrainLength;
                float normHeight = terrain.terrainData.GetHeight(x, y) / terrainHeight;
                Vector3 position = new Vector3(unitX, normHeight, unitY);

                if (placeTree)
                {
                    TreeInstance tree = CreateTreeInstance(position, 0); // Tree prototype index
                    terrain.AddTreeInstance(tree);
                }

                if (placeMushroom)
                {
                    TreeInstance mushroom = CreateTreeInstance(position, 1); // Mushroom prototype index
                    terrain.AddTreeInstance(mushroom);
                }
            }
        }
    }

    void OnEnable()
    {
        terrain = Terrain.activeTerrain;
        if (terrain != null)
        {
            terrain.terrainData = GenerateTerrain(terrain.terrainData);
            AddVegetation();
        }
    }

    void OnDisable()
    {
        if (terrain != null)
        {
            // Clear all tree and mushroom instances
            terrain.terrainData.treeInstances = new TreeInstance[0];
        }
    }

    TreeInstance CreateTreeInstance(Vector3 position, int prototypeIndex)
    {
        return new TreeInstance
        {
            position = position,
            prototypeIndex = prototypeIndex,
            widthScale = Random.Range(0.8f, 1.2f),
            heightScale = Random.Range(0.8f, 1.2f),
            color = Color.white,
            lightmapColor = Color.white
        };
    }

}
