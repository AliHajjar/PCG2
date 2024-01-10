using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    void Start()
    {
        GameObject cube = new GameObject();
        cube.name = "Cube";
        cube.AddComponent<Cube>();
        cube.transform.localScale = this.transform.localScale;
        cube.transform.position = this.transform.position;
        cube.transform.rotation = this.transform.rotation;
        cube.transform.parent = this.transform;
    }
}
