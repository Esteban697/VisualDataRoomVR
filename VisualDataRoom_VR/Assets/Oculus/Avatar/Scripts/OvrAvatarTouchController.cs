using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OvrAvatarTouchController : MonoBehaviour
{
    public GameObject example;
    public OVRInput.Controller controller;

    void Update()
    {
        //OVRInput.Controller controller = OVRInput.GetActiveController();


        if (!OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, controller))
        {
            if ((controller & OVRInput.Controller.LTouch) != 0)
                example.transform.position += Vector3.up * 0.01f;
            if ((controller & OVRInput.Controller.RTouch) != 0)
                example.transform.position -= Vector3.up * 0.01f;
        }
    }
}
