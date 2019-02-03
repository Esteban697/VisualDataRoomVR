using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLetter : MonoBehaviour {

    public GameObject letter, positionPoint;

    // Move the GameObject to positionPoint
    public void Move (float time) {
        iTween.MoveTo(letter,
                   iTween.Hash(
                       "position", new Vector3(positionPoint.transform.position.x,
                                                positionPoint.transform.position.y,
                                                positionPoint.transform.position.z),
                       "time", time,
                       "easetype", "linear"
                   )
           );
    }
}
