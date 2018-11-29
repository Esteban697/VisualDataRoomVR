using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OvrAvatarTouchController : MonoBehaviour
{
    public GameObject example;
    public OVRInput.Controller controller;
    public ThumbCollider thumbCollider;
    public bool zoomPlaneActive = false;
    public GameObject zoomPlane;
    public GameObject otherController;

    void Update()
    {

        if (!OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, controller))
        {
            if ((controller & OVRInput.Controller.LTouch) != 0)
                example.transform.position += Vector3.up * 0.01f;
            if ((controller & OVRInput.Controller.RTouch) != 0)
                example.transform.position -= Vector3.up * 0.01f;
        }
        if (!OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, controller))
        {
            if ((controller & OVRInput.Controller.LTouch) != 0)
                example.transform.position += Vector3.forward * 0.01f;
            if ((controller & OVRInput.Controller.RTouch) != 0)
                example.transform.position -= Vector3.forward * 0.01f;
        }

        if (!OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, controller) && !OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, controller) && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.7f)
        {
            if ((controller & OVRInput.Controller.LTouch) != 0)
                example.transform.position += Vector3.left * 0.01f;
            if ((controller & OVRInput.Controller.RTouch) != 0)
                example.transform.position -= Vector3.left * 0.01f;
        }


        if (!OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, controller) &&
            !OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, controller) &&
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.7f && thumbCollider.thumbsAlligned)
        {
            zoomPlaneActive = true;
            zoomPlane.SetActive(true);
        }
        else if (!OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, controller) &&
                 !OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, controller) &&
                 OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.7f)
        {
            zoomPlane.transform.localScale =
                new Vector3(((this.transform.position.x - otherController.transform.position.x) * 0.3f),
                    0.05f,
                    ((this.transform.position.x - otherController.transform.position.x) * 0.3f));
        }
        else 
        {
            zoomPlaneActive = false;
            zoomPlane.SetActive(false);
        }
    }
}
