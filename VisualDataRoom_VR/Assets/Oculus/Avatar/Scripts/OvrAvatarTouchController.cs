using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OvrAvatarTouchController : MonoBehaviour
{
    public GameObject example;
    public OVRInput.Controller controller;
    public ThumbCollider thumbCollider;
    public PalmCollider palmCollider;
    public GameObject zoomPlane;
    public GameObject zoomPlanePosition;
    public GameObject otherController;
    public GameObject objectToChange;
    public Transform objectTransform;
    public float distanceReference = 0.3f;
    public bool zoomInOn = false;
    public bool zoomOutOn = false;

    void Start()
    {
        
    }
        void Update()
    {
        //Detection of the index finger position
        //if (!OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, controller))
        //{
        //    if ((controller & OVRInput.Controller.LTouch) != 0)
        //        example.transform.position += Vector3.up * 0.01f;
        //    if ((controller & OVRInput.Controller.RTouch) != 0)
        //        example.transform.position -= Vector3.up * 0.01f;
        //}
        //Detection of the thumb finger position
        //if (!OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, controller))
        //{
        //    if ((controller & OVRInput.Controller.LTouch) != 0)
        //        example.transform.position += Vector3.forward * 0.01f;
        //    if ((controller & OVRInput.Controller.RTouch) != 0)
        //        example.transform.position -= Vector3.forward * 0.01f;
        //}
        //if (!OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, controller) && !OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, controller) && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.7f)
        //{
        //    if ((controller & OVRInput.Controller.LTouch) != 0)
        //        example.transform.position += Vector3.left * 0.01f;
        //    if ((controller & OVRInput.Controller.RTouch) != 0)
        //        example.transform.position -= Vector3.left * 0.01f;
        //}
        
        
        //Scale Up Gesture: Detection of "corner gesture": thumb and index up with grip pressed with thumbs alligned
        if (!OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, controller) &&
            !OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, controller) &&
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.7f &&
            thumbCollider.thumbsAlligned)
        {
            zoomPlanePosition.transform.SetParent(objectToChange.transform);
            zoomPlanePosition.transform.localPosition = (Vector3.down * objectToChange.transform.localScale.x * 1.5f);
            zoomPlanePosition.SetActive(true);
            objectTransform.localScale = objectToChange.transform.localScale;
            zoomInOn = true;
            zoomOutOn = false;
        }
        //Out of he colliders
        else if (!OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, controller) &&
                 !OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, controller) &&
                 OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.7f &&
                 !zoomOutOn)
        {
            zoomPlane.transform.localScale =
                new Vector3((Mathf.Abs(this.transform.position.x - otherController.transform.position.x) * 0.9f),
                    0.05f,
                    (Mathf.Abs(this.transform.position.x - otherController.transform.position.x) * 0.9f));

            var distanceBetween = (Mathf.Abs(this.transform.position.x - otherController.transform.position.x) * 0.01f);
            
            objectTransform.localScale =
                new Vector3(objectTransform.localScale.x + distanceBetween,
                        objectTransform.localScale.y + distanceBetween,
                        objectTransform.localScale.z + distanceBetween);
            if (zoomInOn == true)
            {
                objectToChange.transform.localScale = objectTransform.localScale;
            }
        }
        //Change gesture
        else 
        {
            zoomPlanePosition.SetActive(false);
            zoomInOn = false;
        }

        //Scale Down Gesture: Detection of "corner gesture": thumb and index up with grip pressed with palms alligned
        if (!OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, controller) &&
            !OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, controller) &&
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.7f &&
            palmCollider.palmsAlligned)
        {
            zoomPlanePosition.transform.SetParent(objectToChange.transform);
            zoomPlanePosition.transform.localPosition = (Vector3.down * objectToChange.transform.localScale.x * 1.5f);
            zoomPlanePosition.SetActive(true);
            objectTransform.localScale = objectToChange.transform.localScale;
            zoomOutOn = true;
            zoomInOn = false;
        }
        //Out of colliders
        else if (!OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, controller) &&
                 !OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, controller) &&
                 OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.7f &&
                 !zoomInOn)
        {
            zoomPlane.transform.localScale =
                new Vector3((Mathf.Abs(this.transform.position.x - otherController.transform.position.x) * 0.9f),
                    0.05f,
                    (Mathf.Abs(this.transform.position.x - otherController.transform.position.x) * 0.9f));

            var distanceBetween2 = (0.38f/(Mathf.Abs(this.transform.position.x - otherController.transform.position.x)))*0.001f;

            objectTransform.localScale =
                new Vector3(objectTransform.localScale.x - distanceBetween2,
                    objectTransform.localScale.y - distanceBetween2,
                    objectTransform.localScale.z - distanceBetween2);
            if (zoomOutOn == true)
            {
                objectToChange.transform.localScale = objectTransform.localScale;
            }
        }
        // Change gesture
        else
        {
            zoomPlanePosition.SetActive(false);
            zoomOutOn = false;
        }
    }
}
