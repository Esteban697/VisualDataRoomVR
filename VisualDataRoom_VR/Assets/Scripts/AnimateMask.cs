using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMask : MonoBehaviour {

    //Set in Inspector
    public float amplitude;           
    public float speed;
    //Set in scripts 
    private float tempValY, tempValX;
    private Vector3 tempScale;
    private Vector3 tempPos;

    void Start()
    {
        tempScale = transform.localScale;
        tempValY = transform.localScale.y;
        tempValX = transform.localScale.x;
    }
    void Update()
    {
        tempScale.y = tempValY + amplitude * Mathf.Sin(speed * Time.time);
        tempScale.x = tempValX - amplitude * Mathf.Sin(speed * Time.time);
        transform.localScale = tempScale;
    }
}
