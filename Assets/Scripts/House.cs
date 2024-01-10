using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{

    [SerializeField]
   private Vector3 wallSize = Vector3.one;

    void Start()
    {
        int length = 40;
        int height = 10;
        int depth = 1;

        wallSize = new Vector3(length, height , depth); 

        GameObject leftWall = new GameObject();
        leftWall.name = "Left Wall";
        leftWall.AddComponent<Wall>();
        leftWall.transform.localScale = wallSize;
        leftWall.transform.position = this.transform.position;
        leftWall.transform.parent = this.transform;

        GameObject rightWall = new GameObject();
        rightWall.name = "Right Wall";
        rightWall.AddComponent<Wall>();
        rightWall.transform.localScale = wallSize;
        rightWall.transform.position = new Vector3(this.transform.position.x,
                                                   this.transform.position.y,
                                                   this.transform.position.z + length);
        rightWall.transform.parent = this.transform;


        wallSize = new Vector3(length / 2, height , depth);

        GameObject frontWall = new GameObject();
        frontWall.name = "Front Wall";
        frontWall.AddComponent<Wall>();
        frontWall.transform.localScale = wallSize;
        frontWall.transform.position = new Vector3(this.transform.position.x + (length - depth),
                                                   this.transform.position.y,
                                                   this.transform.position.z + (length / 2));
        frontWall.transform.Rotate(0f, 90f, 0f, Space.Self);
        frontWall.transform.parent = this.transform;     

        GameObject backWall = new GameObject();
        backWall.name = "Back Wall";
        backWall.AddComponent<Wall>();
        backWall.transform.localScale = wallSize;
        backWall.transform.position = new Vector3(this.transform.position.x - (length - depth),
                                                   this.transform.position.y,
                                                   this.transform.position.z + (length / 2));
        backWall.transform.Rotate(0f, 90f, 0f, Space.Self);
        backWall.transform.parent = this.transform;  

        wallSize = new Vector3(length, (height * 2), depth); 
        
        GameObject floor = new GameObject();
        floor.name = "Floor";
        floor.AddComponent<Wall>();
        floor.transform.localScale = wallSize;
        floor.transform.position = new Vector3(this.transform.position.x,
                                                   this.transform.position.y - (height - depth),
                                                   this.transform.position.z + (height * 2));
        floor.transform.Rotate(90f, 0f, 0f, Space.Self);
        floor.transform.parent = this.transform;

        /**
        GameObject ceiling = new GameObject();
        ceiling.name = "Ceiling";
        ceiling.AddComponent<Wall>();
        ceiling.transform.localScale = wallSize;
        ceiling.transform.position = new Vector3(this.transform.position.x,
                                                   this.transform.position.y + (height - depth),
                                                   this.transform.position.z + (height * 2));
        ceiling.transform.Rotate(90f, 0f, 0f, Space.Self);
        ceiling.transform.parent = this.transform;
        **/

    }

}
