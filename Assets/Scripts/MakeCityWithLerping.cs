using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCityWithLerping : MonoBehaviour {

    public GameObject buildingPrefab;
    public int numberOfBuildings;
    public float maxHeight;
    public float cityRadius = 10;

    private int lastBuildingSpawned = 0;

	// Use this for initialization
	void Start () {
        //CreateCity();
        CreateCityAroundCircle();
	}
	

    void CreateCityAroundCircle()
    {
        for (int i = 0; i < numberOfBuildings; ++i)
        {
            Vector2 randomPointInCircle = Random.insideUnitCircle*cityRadius;
            Vector3 nextBuildingPosition = new Vector3(randomPointInCircle.x,0,randomPointInCircle.y);
            float nextBuildingHeight = Mathf.Lerp(maxHeight,maxHeight*0.1f,nextBuildingPosition.magnitude/cityRadius);
            Vector3 nextBuildingScale = new Vector3(0.75f,nextBuildingHeight,0.75f);

            MakeBuilding(nextBuildingPosition,nextBuildingScale);
        }
    }

    void MakeBuilding(Vector3 position, Vector3 scale)
    {
        GameObject newBuilding = Instantiate<GameObject>(buildingPrefab);
        newBuilding.transform.position = position;
        newBuilding.transform.localScale = scale;
    }

    void CreateCity()
    {
        for (int i = 0; i < numberOfBuildings; ++i)
        {
            StartCoroutine(AddBuildingAfterTime(i));
        }
    }

    IEnumerator AddBuildingAfterTime(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait*0.1f);
        GameObject newBuilding = Instantiate<GameObject>(buildingPrefab);
        newBuilding.transform.position = Vector3.right * secondsToWait*2;
    }

    //void CreateCity()
    //{
    //    for (int i = 0; i < numberOfBuildings; ++i)
    //    {
    //        if (Time.time > i && lastBuildingSpawned < i)
    //        {
    //            GameObject newBuilding = Instantiate<GameObject>(buildingPrefab);
    //            newBuilding.transform.position = Vector3.right * i;
    //            lastBuildingSpawned = i;
    //        }
    //    }
    //}
}
