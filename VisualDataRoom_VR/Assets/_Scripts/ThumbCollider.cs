using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumbCollider : MonoBehaviour {

    public bool thumbsAlligned = false;

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Thumbs"))
        {
            thumbsAlligned = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Thumbs"))
        {
            thumbsAlligned = false;
        }
    }
}
