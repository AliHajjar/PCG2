using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{

    [SerializeField]
    private Vector3 size = Vector3.one;

    [SerializeField]
    private int submeshCount = 6;

    void Start() {
        BuildCube();
    }

    void BuildCube()
    {

        int topSubmeshIndex     = 1;
        int bottomSubmeshIndex  = 1;
        int frontSubmeshIndex   = 1;
        int backSubmeshIndex    = 1;
        int leftSubmeshIndex    = 1;
        int rightSubmeshIndex   = 1;

        MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        MeshBuilder meshBuilder = new MeshBuilder(submeshCount);


        //define points
        //top points
        Vector3 t0 = new Vector3(size.x, size.y, -size.z);
        Vector3 t1 = new Vector3(-size.x, size.y, -size.z);
        Vector3 t2 = new Vector3(-size.x, size.y, size.z);
        Vector3 t3 = new Vector3(size.x, size.y, size.z);

        //bottom points
        Vector3 b0 = new Vector3(size.x, -size.y, -size.z);
        Vector3 b1 = new Vector3(-size.x, -size.y, -size.z);
        Vector3 b2 = new Vector3(-size.x, -size.y, size.z);
        Vector3 b3 = new Vector3(size.x, -size.y, size.z);


        //create the triangles
        
        //top square
        meshBuilder.BuildTriangle(t0, t1, t2, topSubmeshIndex);
        meshBuilder.BuildTriangle(t0, t2, t3, topSubmeshIndex);

        //bottom square
        meshBuilder.BuildTriangle(b2, b1, b0, bottomSubmeshIndex);
        meshBuilder.BuildTriangle(b3, b2, b0, bottomSubmeshIndex);

        //front square
        meshBuilder.BuildTriangle(b2, t3, t2, frontSubmeshIndex);
        meshBuilder.BuildTriangle(b2, b3, t3, frontSubmeshIndex);

        //back square
        meshBuilder.BuildTriangle(b0, t1, t0, backSubmeshIndex);
        meshBuilder.BuildTriangle(b0, b1, t1, backSubmeshIndex);

        //left square
        meshBuilder.BuildTriangle(b1, t2, t1, leftSubmeshIndex);
        meshBuilder.BuildTriangle(b1, b2, t2, leftSubmeshIndex);

        //right square
        meshBuilder.BuildTriangle(b3, t0, t3, rightSubmeshIndex);
        meshBuilder.BuildTriangle(b3, b0, t0, rightSubmeshIndex);


        meshFilter.mesh = meshBuilder.CreateMesh();

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        Materials materials = new Materials();

        meshRenderer.materials = materials.GetMaterialsList().ToArray();
    }
}
