using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public int gridRows = 11;
    public int gridColumns = 11;
    public float gridSize = 15f; // Distance between buildings

    private bool[,] gridOccupied;

    void Start()
    {
        GenerateCity();
    }

    void GenerateCity()
    {
        gridOccupied = new bool[gridRows, gridColumns];

        int buildingsToGenerate = Random.Range(5, 7); // Randomize number of buildings
        for (int i = 0; i < buildingsToGenerate; i++)
        {
            Vector3 position = GetRandomBuildingPosition();
            GenerateBuilding(position);
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
        return new Vector3(row * gridSize, 0, column * gridSize);
    }

    void GenerateBuilding(Vector3 position)
    {
        GameObject building = new GameObject("Building");
        building.transform.position = position;

        MeshFilter meshFilter = building.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = building.AddComponent<MeshRenderer>();
        meshFilter.mesh = GenerateBuildingMesh();

        Material buildingMaterial = new Material(Shader.Find("Standard"));
        buildingMaterial.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        meshRenderer.material = buildingMaterial;

        building.transform.localScale = new Vector3(
            Mathf.Max(Random.Range(1, 5), 6.0f),
            Random.Range(10.0f, 20.0f),
            Mathf.Max(Random.Range(1, 5), 6.0f)
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
