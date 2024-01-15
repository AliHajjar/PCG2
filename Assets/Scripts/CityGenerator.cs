using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public int gridRows = 11;
    public int gridColumns = 11;
    public float gridSize = 15f; // Distance between buildings

    private bool[,] gridOccupied;

    void Awake()
    {
        gridOccupied = new bool[gridRows, gridColumns];
        GenerateRoad();
        GenerateCity();
        GeneratePlane();
    }

    private void Start()
    {
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
    }

    void GenerateCity()
    {
        int buildingsToGenerate = Random.Range(7, 9); // Randomize number of buildings
        for (int i = 0; i < buildingsToGenerate; i++)
        {
            Vector3 position = GetRandomBuildingPosition();
            if (position != Vector3.zero) // Check if a valid position was found
            {
                GenerateBuilding(position);
            }
        }
    }



    void GenerateRoad()
    {
        // Determine the row for the road (assuming the road is horizontal)
        int roadRow = gridRows / 2;

        // The buffer size on each side of the road
        int bufferSize = 1;

        // Mark the road's cells and buffer zone as occupied
        for (int col = 0; col < gridColumns; col++)
        {
            for (int buffer = -bufferSize; buffer <= bufferSize; buffer++)
            {
                int rowToMark = roadRow + buffer;
                if (rowToMark >= 0 && rowToMark < gridRows) // Check to avoid indexing outside the grid
                {
                    gridOccupied[rowToMark, col] = true;
                }
            }
        }

        // Instantiate and set up the road GameObject
        GameObject roadObject = new GameObject("Road");
        roadObject.AddComponent<Road>();
        float roadLength = gridSize / 1.53f; // Adjust the length here
        roadObject.transform.position = new Vector3(gridRows * gridSize / 2, 0, roadRow * gridSize);
        roadObject.transform.localScale = new Vector3(1f, 1f, roadLength);

    }


    Vector3 GetRandomBuildingPosition()
    {
        int row, column;
        do
        {
            row = Random.Range(0, gridRows);
            column = Random.Range(0, gridColumns);
            if (!gridOccupied[row, column])
            {
                gridOccupied[row, column] = true; // Mark the cell as occupied
                return new Vector3(row * gridSize, 0, column * gridSize);
            }
        } while (true); // Keep looking until a free cell is found

        return Vector3.zero; // Return a zero vector if no position is found
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

    void GeneratePlane()
    {
        float planeWidth = gridRows * gridSize;
        float planeHeight = gridColumns * gridSize;

        // Adjust the size of the plane
        Vector3 planeScale = new Vector3(planeWidth / 8f, 1f, planeHeight / 8f); // Unity's default plane size is 10x10 units

        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = planeScale;
        plane.transform.position = new Vector3(planeWidth / 2f, -0.1f, planeHeight / 2f);
    }

}
