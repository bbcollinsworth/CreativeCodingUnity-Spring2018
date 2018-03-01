using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpController : MonoBehaviour {

    [Range(0,1)]
    public float sliderValue;

    [Space]
    public float currentLerpedValue;

    private float startValue = -23;
    private float endValue = 16.7845f;

    public Vector3 startPosition;
    public Vector3 endPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ChangeSliderValueOverTime();

        //Lerping a float value between start and end floats based on 3rd parameter (sliderValue)
        currentLerpedValue = Mathf.Lerp(startValue, endValue, sliderValue);

        //another way of testing the output of the Mathf.Lerp function
        Debug.Log(Mathf.Lerp(startValue, endValue, sliderValue));

        //Lerping between start and end Vectors based on slider value
        transform.position = Vector3.Lerp(startPosition, endPosition, sliderValue);

        //float madeUpVariable = AddOneToNumber(4);
	}

    void ChangeSliderValueOverTime()
    {
        //sliderValue = Time.time % 1;

        //Absolute value of Sin of Time
        sliderValue = Mathf.Abs(Mathf.Sin(Time.time*10));
    }

    //EXAMPLE OF A FUNCTION THAT RETURNS A VALUE
    //float Number(float numberIn)
    //{
    //    return numberIn;
    //}

    //float AddOneToNumber(float numberIn)
    //{
    //    return numberIn + 1;
    //}
}
