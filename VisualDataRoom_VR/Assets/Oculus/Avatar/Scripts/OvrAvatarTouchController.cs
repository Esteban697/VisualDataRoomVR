using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OvrAvatarTouchController : MonoBehaviour
{
    public GameObject example;
    public OVRInput.Controller controller;
    public ThumbCollider thumbCollider;
    public GameObject zoomPlane;
    public GameObject zoomPlanePosition;
    public GameObject otherController;
    public GameObject objectToChange;
    public Transform objectTransform;
    public bool zoomInOn = false;

    void Start()
    {
        
    }
        void Update()
    {
        //Detection of the index finger position
        if (!OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, controller))
        {
            if ((controller & OVRInput.Controller.LTouch) != 0)
                example.transform.position += Vector3.up * 0.01f;
            if ((controller & OVRInput.Controller.RTouch) != 0)
                example.transform.position -= Vector3.up * 0.01f;
        }
        //Detection of the thumb finger position
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
        
        
        //Detection of "corner gesture": thumb and index up with grip pressed
        if (!OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, controller) &&
            !OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, controller) &&
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.7f && thumbCollider.thumbsAlligned)
        {
            zoomPlanePosition.transform.SetParent(objectToChange.transform);
            zoomPlanePosition.transform.localPosition = (Vector3.down * objectToChange.transform.localScale.x * 1.5f);
            zoomPlanePosition.SetActive(true);
            objectTransform.localScale = objectToChange.transform.localScale;
            zoomInOn = true;
        }
        else if (!OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, controller) &&
                 !OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, controller) &&
                 OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.7f)
        {
            zoomPlane.transform.localScale =
                new Vector3((Mathf.Abs(this.transform.position.x - otherController.transform.position.x) * 0.9f),
                    0.05f,
                    (Mathf.Abs(this.transform.position.x - otherController.transform.position.x) * 0.9f));

            objectTransform.localScale =
                new Vector3(objectTransform.localScale.x + ((Mathf.Abs(this.transform.position.x - otherController.transform.position.x) * 0.01f)),
                        objectTransform.localScale.y + ((Mathf.Abs(this.transform.position.x - otherController.transform.position.x) * 0.01f)),
                        objectTransform.localScale.z + ((Mathf.Abs(this.transform.position.x - otherController.transform.position.x) * 0.01f)));
            if (zoomInOn == true)
            {
                objectToChange.transform.localScale = objectTransform.localScale;
            }
        }
        else 
        {
            zoomPlanePosition.SetActive(false);
            zoomInOn = false;
        }
    }
}
