using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class RoadCube : MonoBehaviour
{
    [SerializeField]
    private Vector3 size = Vector3.one;
    private const int submeshCount = 1;

    private const int submeshIndex = 0;

    private List<Material> materialList = new List<Material>();

    public void SetMaterialList(List<Material> materialList){
        this.materialList = materialList;
    }

    public void SetSize(Vector3 size){
        this.size = size;
    }

    void Start() {
        BuildCube();
    }

    void BuildCube()
    {

        MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        MeshCollider meshCollider = this.GetComponent<MeshCollider>();

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
        meshBuilder.BuildTriangle(t0, t1, t2, submeshIndex);
        meshBuilder.BuildTriangle(t0, t2, t3, submeshIndex);

        //bottom square
        meshBuilder.BuildTriangle(b2, b1, b0, submeshIndex);
        meshBuilder.BuildTriangle(b3, b2, b0, submeshIndex);

        //front square
        meshBuilder.BuildTriangle(b2, t3, t2, submeshIndex);
        meshBuilder.BuildTriangle(b2, b3, t3, submeshIndex);

        //back square
        meshBuilder.BuildTriangle(b0, t1, t0, submeshIndex);
        meshBuilder.BuildTriangle(b0, b1, t1, submeshIndex);

        //left square
        meshBuilder.BuildTriangle(b1, t2, t1, submeshIndex);
        meshBuilder.BuildTriangle(b1, b2, t2, submeshIndex);

        //right square
        meshBuilder.BuildTriangle(b3, t0, t3, submeshIndex);
        meshBuilder.BuildTriangle(b3, b0, t0, submeshIndex);


        meshFilter.mesh = meshBuilder.CreateMesh();

        

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.materials = materialList.ToArray();

        meshCollider.sharedMesh = meshFilter.mesh;
    }
}
