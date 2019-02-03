using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmCollider : MonoBehaviour {

    public bool palmsAlligned = false;

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Palms"))
        {
            palmsAlligned = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Palms"))
        {
            palmsAlligned = false;
        }
    }
}
