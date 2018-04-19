using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockController : MonoBehaviour {

    public int numberOfFish = 30;
    public GameObject fishPrefab;
    public float spawnRadius = 5;
    [Space,Header("Flock Tuning")]
    public float attractStrength = 1;
    public float avoidRadius = 6;
    public float avoidStrength = 8;
    public float alignStrength = 0.8f;
    [Space]
    public FishController[] fishArray;

	void Start () {
        SpawnFish();
	}
	
	void Update () {
		
	}

    void SpawnFish()
    {
        //initialize array with slots for each of our fish (an array of size = numberOfFish
        fishArray = new FishController[numberOfFish];

        for (int i = 0; i < numberOfFish; ++i)
        {
            //Create a new fish GameObject
            GameObject newFish = Instantiate<GameObject>(fishPrefab,Random.insideUnitSphere*spawnRadius,Random.rotation);
            //Store a reference to that fish's fishController component in an array
            fishArray[i] = newFish.GetComponent<FishController>();
            //tell that fishController component that this is our flock controller -- store it in variable in that component
            fishArray[i].controller = this;
        }
    }
}
