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
        //ChangeSliderValueBasedOnSpace();

        //Lerping a float value between start and end floats based on 3rd parameter (sliderValue)
        currentLerpedValue = Mathf.Lerp(startValue, endValue, sliderValue);

        //another way of testing the output of the Mathf.Lerp function
        Debug.Log(Mathf.Lerp(startValue, endValue, sliderValue));

        //Lerping between start and end Vectors based on slider value
        //transform.position = Vector3.Lerp(startPosition, endPosition, sliderValue);

        //Lerping scale between zero and 1 based on slider value
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, sliderValue);

        //float madeUpVariable = AddOneToNumber(4);
	}

    void ChangeSliderValueOverTime()
    {
        //This goes from 0 to 1 every second:
        //sliderValue = Time.time % 1;

        //Absolute value of Sin of Time
        sliderValue = Mathf.Abs(Mathf.Sin(Time.time*10));
    }

    void ChangeSliderValueBasedOnSpace()
    {
        //this takes our t/slider/0-1 value from our position on the x axis in worldspace
        sliderValue = transform.position.x;
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
