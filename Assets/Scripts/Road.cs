using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    float depth = 1f;

    float pavementWidth = 0.5f;
    float pavementHeight = 0.2f;

    float laneWidth = 2f;
    float laneHeight = 0.1f;

    float roadMarkWidth = 0.05f;
    float roadMarkHeigth = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        //pavement
        List<Material> pavementMaterialList = new List<Material>();

        Material pavementMaterial = new Material(Shader.Find("Specular"));
        pavementMaterial.color = Color.grey;

        pavementMaterialList.Add(pavementMaterial);

        //lane
        List<Material> laneMaterialList = new List<Material>();

        Material laneMaterial = new Material(Shader.Find("Specular"));
        laneMaterial.color = Color.black;

        laneMaterialList.Add(laneMaterial);

        //road mark
        List<Material> roadMarkMaterialList = new List<Material>();

        Material roadMarkMaterial = new Material(Shader.Find("Specular"));
        roadMarkMaterial.color = Color.white;

        roadMarkMaterialList.Add(roadMarkMaterial);

        //Left Pavement
        GameObject leftPavement = new GameObject();
        leftPavement.name = "Left Pavement";
        RoadCube lPRoadCube = leftPavement.AddComponent<RoadCube>();
        lPRoadCube.SetSize(new Vector3(pavementWidth, pavementHeight, depth));
        lPRoadCube.SetMaterialList(pavementMaterialList);
        leftPavement.transform.position = this.transform.position;
        leftPavement.transform.parent = this.transform;
        leftPavement.transform.localScale = this.transform.localScale;

        //Left Lane
        GameObject leftLane = new GameObject();
        leftLane.name = "Left Lane";
        RoadCube lLRoadCube = leftLane.AddComponent<RoadCube>();
        lLRoadCube.SetSize(new Vector3(laneWidth, laneHeight, depth));
        lLRoadCube.SetMaterialList(laneMaterialList);
        leftLane.transform.position = new Vector3(
            leftPavement.transform.position.x + pavementWidth + laneWidth,
            this.transform.position.y,
            this.transform.position.z);
        leftLane.transform.parent = this.transform;
        leftLane.transform.localScale = this.transform.localScale;

        //Road Mark
        GameObject roadMark = new GameObject();
        roadMark.name = "Road Mark";
        RoadCube rMRoadCube = roadMark.AddComponent<RoadCube>();
        rMRoadCube.SetSize(new Vector3(roadMarkWidth, roadMarkHeigth, depth));
        rMRoadCube.SetMaterialList(roadMarkMaterialList);
        roadMark.transform.position = new Vector3(
            leftLane.transform.position.x + laneWidth + roadMarkWidth,
            this.transform.position.y,
            this.transform.position.z);
        roadMark.transform.parent = this.transform;
        roadMark.transform.localScale = this.transform.localScale;

        //Right Lane
        GameObject rightLane = new GameObject();
        rightLane.name = "Right Lane";
        RoadCube rLRoadCube = rightLane.AddComponent<RoadCube>();
        rLRoadCube.SetSize(new Vector3(laneWidth, laneHeight, depth));
        rLRoadCube.SetMaterialList(laneMaterialList);
        rightLane.transform.position = new Vector3(
            roadMark.transform.position.x + roadMarkWidth + laneWidth,
            this.transform.position.y,
            this.transform.position.z);
        rightLane.transform.parent = this.transform;
        rightLane.transform.localScale = this.transform.localScale;

        //Right Pavement
        GameObject rightPavement = new GameObject();
        rightPavement.name = "Right Pavement";
        RoadCube rPRoadCube = rightPavement.AddComponent<RoadCube>();
        rPRoadCube.SetSize(new Vector3(pavementWidth, pavementHeight, depth));
        rPRoadCube.SetMaterialList(pavementMaterialList);
        rightPavement.transform.position = new Vector3(
            rightLane.transform.position.x + laneWidth + pavementWidth,
            this.transform.position.y,
            this.transform.position.z);
        rightPavement.transform.parent = this.transform;  
        rightPavement.transform.localScale = this.transform.localScale;   
    }

    public float GetPavementWidth(){
        return pavementWidth;
    }

    public float GetLaneWidth(){
        return laneWidth;
    }

    
}
