using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraEnd : MonoBehaviour {

    public GameObject cameraObject, positionPoint;

    // Move the camera to positionPoint
    public void MoveCamera(float time)
    {
        iTween.MoveTo(cameraObject,
                   iTween.Hash(
                       "position", new Vector3(positionPoint.transform.position.x,
                                                positionPoint.transform.position.y,
                                                positionPoint.transform.position.z),
                       "time", time,
                       "easetype", "bounce"
                   )
           );
    }
}
