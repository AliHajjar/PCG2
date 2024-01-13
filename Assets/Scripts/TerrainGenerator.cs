using System.Collections;
using System.Collections.Generic;
using UnityEditor; // This is required for AssetDatabase
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public Terrain terrain;
    public int terrainWidth = 2049;  // Default width
    public int terrainLength = 2049; // Default length
    public float terrainHeight = 200f;  // Default height
    public float detailDensity = 0.3f; // Default density of vegetation

    public Material grassMaterial;

    public float scale = 3f;           // Controls the scale of the noise
    public float heightMultiplier = 1f; // Multiplies the height of the noise

    void Start()
    {
        terrain = Terrain.activeTerrain;

        Material skyboxMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Polytope Studio/Lowpoly_Environments/Sources/Materials/PT_Skybox_mat.mat");

        // Check if the skybox material is loaded
        if (skyboxMaterial != null)
        {
            // Apply the skybox material to the scene
            RenderSettings.skybox = skyboxMaterial;
        }
        else
        {
            Debug.LogError("Skybox material not found");
        }

        if (terrain != null)
        {
            terrain.terrainData = GenerateTerrain(terrain.terrainData);
            AddVegetation();
            terrain.transform.position = new Vector3(0, -0.1f, 0);
        }
        else
        {
            Debug.LogError("Terrain not found");
        }

        if (grassMaterial != null)
        {
            terrain.materialTemplate = grassMaterial;
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
        float offsetX = Random.Range(0f, 100f); // Random offset for X
        float offsetY = Random.Range(0f, 100f); // Random offset for Y

        for (int x = 0; x < terrainWidth; x++)
        {
            for (int y = 0; y < terrainLength; y++)
            {
                float xCoord = ((float)x / terrainWidth * scale) + offsetX;
                float yCoord = ((float)y / terrainLength * scale) + offsetY;
                heights[x, y] = Mathf.PerlinNoise(xCoord, yCoord) * heightMultiplier;
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
                bool placeTree = Random.Range(0.0f, 1.0f) < (detailDensity * 0.02f);
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
            float[,] resetHeights = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];
            terrain.terrainData.SetHeights(0, 0, resetHeights);
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
