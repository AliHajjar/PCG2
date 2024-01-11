using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public int gridRows = 6;
    public int gridColumns = 6;
    public float gridSize = 6f; // Distance between buildings

    private bool[,] gridOccupied;

    void Start()
    {
        gridOccupied = new bool[gridRows, gridColumns];
        GenerateCity();
    }

    void GenerateCity()
    {
        int buildingsToGenerate = 5;
        for (int i = 0; i < buildingsToGenerate; i++)
        {
            Vector3 position = GetRandomBuildingPosition();
            GenerateBuilding(position);
        }

        GenerateRoadNetwork();
    }

    Vector3 GetRandomBuildingPosition()
    {
        int row, column;
        do
        {
            row = Random.Range(0, gridRows);
            column = Random.Range(0, gridColumns);
        } while (gridOccupied[row, column]);

        gridOccupied[row, column] = true;

        float xPosition = row * gridSize;
        float zPosition = column * gridSize;
        return new Vector3(xPosition, 0, zPosition);
    }

    void GenerateBuilding(Vector3 position)
    {
        GameObject building = new GameObject("Building");
        building.transform.position = position;

        MeshFilter meshFilter = building.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = building.AddComponent<MeshRenderer>();

        meshFilter.mesh = GenerateBuildingMesh();

        // Create a more realistic material
        Material buildingMaterial = new Material(Shader.Find("Standard"));
        buildingMaterial.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)); // Random color
        meshRenderer.material = buildingMaterial;

        building.transform.localScale = new Vector3(Random.Range(1, 5), Random.Range(3, 10), Random.Range(1, 5));

        float minWidthDepth = 3.0f; // Minimum width/depth to prevent very flat buildings
        building.transform.localScale = new Vector3(
            Mathf.Max(Random.Range(1, 5), minWidthDepth),
            Random.Range(3, 10),
            Mathf.Max(Random.Range(1, 5), minWidthDepth)
        );
    }

    void GenerateRoadNetwork()
    {
        for (int row = 0; row < gridRows; row++)
        {
            for (int column = 0; column < gridColumns; column++)
            {
                if (!gridOccupied[row, column])
                {
                    Vector3 roadPosition = new Vector3(row * gridSize, 0, column * gridSize);
                    GenerateRoad(roadPosition);
                }
            }
        }
    }

    void GenerateRoad(Vector3 position)
    {
        GameObject road = new GameObject("Road");
        road.transform.position = position;

        RoadCube roadCube = road.AddComponent<RoadCube>();
        roadCube.SetSize(new Vector3(gridSize, 0.1f, gridSize)); // Example size
        // Configure other RoadCube properties as needed

        Road roadComponent = road.AddComponent<Road>();
        // Configure Road properties as needed

        // If RoadNetwork is used for managing road connections
        RoadNetwork roadNetwork = road.AddComponent<RoadNetwork>();
        // Configure RoadNetwork properties as needed
    }

    Mesh GenerateBuildingMesh()
    {
        Mesh mesh = new Mesh();

        // Example of a simple cube mesh
        Vector3[] vertices = {
            new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 1), new Vector3(0, 0, 1), // Bottom
            new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 1), new Vector3(0, 1, 1)  // Top
        };

        int[] triangles = {
            0, 2, 1, 0, 3, 2, // Bottom
            4, 5, 6, 4, 6, 7, // Top
            0, 1, 5, 0, 5, 4, // Front
            1, 2, 6, 1, 6, 5, // Right
            2, 3, 7, 2, 7, 6, // Back
            3, 0, 4, 3, 4, 7  // Left
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
