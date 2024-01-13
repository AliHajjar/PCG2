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

        roadOffset = Random.Range(-90,90);

    }

    void Update(){
        
    }

}
