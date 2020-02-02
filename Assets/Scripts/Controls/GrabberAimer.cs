using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberAimer : MonoBehaviour
{
    public Transform raycastStartPos;
    public LayerMask emptyGrabberLayerMask;
    public LayerMask heldObjectLayerMask;
    public Grabber grabber;
    public bool surfaceMode = true;

    public float distanceMax = 10f;
    public float distanceMin = 1f;
    public float currentDistance = 3;

    public float rotationV;
    public float rotationH;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        LayerMask mask = emptyGrabberLayerMask;
        if(grabber.grabbed) {
            mask = heldObjectLayerMask;
        }
        if(surfaceMode && Physics.Raycast(raycastStartPos.position, raycastStartPos.forward, out hit, 200f, mask)) {
            grabber.transform.position = hit.point;
            currentDistance = Vector3.Distance(raycastStartPos.position, hit.point);
        }
        else {
            grabber.transform.position = raycastStartPos.position + raycastStartPos.forward * currentDistance;
        }
    }
}
