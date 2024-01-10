using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNetwork : MonoBehaviour
{
    [SerializeField]
    Vector3 verticalRoadSize = new Vector3(1f, 1f, 10f);

    [SerializeField]
    Vector3 horizontalRoadSize = new Vector3(1f, 1f, 6.8f);


    GameObject topRoad;

    bool roadRotated = false;

    [SerializeField]
    private GameObject carPrefab;

    private int roadOffset = 5;

    // Start is called before the first frame update
    void Start()
    {
        GameObject leftRoad = new GameObject();
        leftRoad.name = "Left Road";
        leftRoad.AddComponent<Road>();
        leftRoad.transform.position = this.transform.position;
        leftRoad.transform.parent = this.transform;
        leftRoad.transform.localScale = verticalRoadSize;


        GameObject rightRoad = new GameObject();
        rightRoad.name = "Right Road";
        rightRoad.AddComponent<Road>();
        rightRoad.transform.position = new Vector3(
            this.transform.position.x + 100,
            this.transform.position.y,
            this.transform.position.z);
        rightRoad.transform.parent = this.transform;
        rightRoad.transform.localScale = verticalRoadSize;

        topRoad = new GameObject();
        topRoad.name = "Top Road";
        topRoad.AddComponent<Road>();
        topRoad.transform.position = new Vector3(
            this.transform.position.x + 54.4f,
            this.transform.position.y,
            this.transform.position.z + 109.6f);
        topRoad.transform.parent = this.transform;
        topRoad.transform.localScale = horizontalRoadSize;

        Road leftRoadObj = leftRoad.GetComponent<Road>();

        roadOffset = Random.Range(-90,90);

        Vector3 carSpawnPos = new Vector3(
                leftRoad.transform.position.x + leftRoadObj.GetPavementWidth() + leftRoadObj.GetLaneWidth(),
                leftRoad.transform.position.y,
                leftRoad.transform.position.z + roadOffset

        );

        if(carPrefab != null){
            GameObject car = Instantiate(carPrefab, carSpawnPos, Quaternion.identity);
            car.name = "Car";
            car.transform.parent = this.transform; 
        }      
    }

    void Update(){
        if(!roadRotated){
            topRoad.transform.Rotate(0f, 90f, 0f, Space.World);
            roadRotated = true;
        }
    }

}
