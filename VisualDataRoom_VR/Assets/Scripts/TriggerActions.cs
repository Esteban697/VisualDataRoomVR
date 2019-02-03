using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActions : MonoBehaviour {

    public GameObject myXObject, myMaskObject, myDObject, cameraObject, particleObject;
    public AudioSource pianoSound, particleSound, endSound;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            myXObject.GetComponent<MoveLetter>().Move(6.0f);
            myMaskObject.GetComponent<MoveLetter>().Move(4.0f);
            myDObject.GetComponent<MoveLetter>().Move(5.0f);
            cameraObject.GetComponent<MoveLetter>().Move(5.0f);
            particleObject.SetActive(true);
            pianoSound.Play();
            particleSound.Play();
        }
        if (Input.GetKeyDown("z"))
        {
            print("space key was pressed");
            cameraObject.GetComponent<MoveCameraEnd>().MoveCamera(3.5f);
            endSound.Play();
        }
    }
}
