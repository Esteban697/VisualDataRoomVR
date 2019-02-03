using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateX : MonoBehaviour {

    //Set in Inspector
    public float amplitude;
    public float speed;
    //Set in scripts 
    private float tempValY, tempValX, tempValZ;
    private Vector3 tempScale;
    private Vector3 tempPos;

    void Start()
    {
        tempScale = transform.localScale;
        tempValY = transform.localScale.y;
        tempValX = transform.localScale.x;
        tempValZ = transform.localScale.z;
    }
    void Update()
    {
        tempScale.y = tempValY - (amplitude/2) * Mathf.Sin(speed * Time.time);
        tempScale.x = tempValX + amplitude * Mathf.Sin(speed * Time.time);
        tempScale.z = tempValZ - (amplitude/2) * Mathf.Sin(speed * Time.time);
        transform.localScale = tempScale;
    }
}
