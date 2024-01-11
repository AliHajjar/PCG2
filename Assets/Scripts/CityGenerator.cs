using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public int gridRows = 10;
    public int gridColumns = 10;
    public float gridSize = 11f; // Distance between buildings
    public int numberOfCities = 2;

    private bool[,] gridOccupied;

    void Start()
    {
        gridOccupied = new bool[gridRows, gridColumns];
        for (int i = 0; i < numberOfCities; i++)
        {
            GenerateCity(i);
        }
    }

    void GenerateCity(int cityIndex)
    {
        gridOccupied = new bool[gridRows, gridColumns];
        Random.InitState(cityIndex); // Initialize random seed based on the city index

        int buildingsToGenerate = 6; // Randomize number of buildings
        for (int i = 0; i < buildingsToGenerate; i++)
        {
            Vector3 position = GetRandomBuildingPosition();
            GenerateBuilding(position, cityIndex);
        }
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

        float xPosition = (row + 0.5f) * gridSize; // Center of the grid cell
        float zPosition = (column + 0.5f) * gridSize; // Center of the grid cell
        return new Vector3(xPosition, 0, zPosition);
    }

    void GenerateBuilding(Vector3 position, int cityIndex)
    {
        float offsetMultiplier = 3f; // Adjust this value to increase or decrease the gap
        position += new Vector3(cityIndex * gridRows * gridSize * offsetMultiplier, 0, 0);

        GameObject building = new GameObject("Building");
        building.transform.position = position;

        MeshFilter meshFilter = building.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = building.AddComponent<MeshRenderer>();

        meshFilter.mesh = GenerateBuildingMesh();

        Material buildingMaterial = new Material(Shader.Find("Standard"));
        buildingMaterial.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        meshRenderer.material = buildingMaterial;

        float minWidthDepth = 6.0f;
        float minHeight = 10.0f;
        float maxHeight = 20.0f;
        building.transform.localScale = new Vector3(
            Mathf.Max(Random.Range(1, 5), minWidthDepth),
            Random.Range(minHeight, maxHeight),
            Mathf.Max(Random.Range(1, 5), minWidthDepth)
        );
    }

    Mesh GenerateBuildingMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = {
            new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 1), new Vector3(0, 0, 1),
            new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 1), new Vector3(0, 1, 1)
        };

        int[] triangles = {
            0, 2, 1, 0, 3, 2,
            4, 5, 6, 4, 6, 7,
            0, 1, 5, 0, 5, 4,
            1, 2, 6, 1, 6, 5,
            2, 3, 7, 2, 7, 6,
            3, 0, 4, 3, 4, 7
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

}
