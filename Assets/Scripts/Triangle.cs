using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Triangle : MonoBehaviour
{
    [SerializeField]
    private Vector3 size = Vector3.one;

    private int subMeshCount = 1;

    void Start()
    {
       MeshFilter meshFilter = GetComponent<MeshFilter>();

       MeshBuilder meshBuilder = new MeshBuilder(subMeshCount);

       Vector3 p0 = new Vector3(size.x, size.y, -size.z);
       Vector3 p1 = new Vector3(-size.x, size.y, -size.z);
       Vector3 p2 = new Vector3(-size.x, size.y, size.z);

       meshBuilder.BuildTriangle(p0, p1, p2, 0);

       meshFilter.mesh = meshBuilder.CreateMesh();

    }

 
}
