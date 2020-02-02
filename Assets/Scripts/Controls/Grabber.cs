using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public Socketable grabCandidate;
    public Socketable grabbed;
    public GrabberAimer aimer;
    public Renderer myRenderer;
    public Screwdriver screwdriver;

    public float rotateH;
    public float rotateV;
    public float rotateSpeed;
    public float rotateLerp;

    void Start() {
        myRenderer = GetComponent<Renderer>();
        screwdriver = GetComponent<Screwdriver>(); //OPTIONAL
    }

    public bool TryGrab() {
        if(grabCandidate && grabCandidate.CanBeRemoved()) {
            grabbed = grabCandidate;
            grabCandidate.highlighter.OnHoverEnd();
            myRenderer.enabled = false;
            grabbed.Grab(this);
            return true;
        }
        return false;
    }

    public void TryDrop() {
        if(grabbed) {
            myRenderer.enabled = true;
            grabbed.Drop();
            grabbed = null;
            rotateH = 0;
            rotateV = 0;
        }
    }

    void Update() {
        if(grabCandidate) {
            grabCandidate.highlighter.OnHoverStay();
        }
        transform.localEulerAngles = new Vector3(rotateV, rotateH, 0);
    }

    void OnTriggerStay(Collider coll) {
        if(grabbed) return;
        var g = coll.GetComponentInParent<Socketable>();
        if(g) {
            if(grabCandidate) {
                if(g.priority > grabCandidate.priority || grabCandidate.IsRetainedByScrews() && !g.IsRetainedByScrews()) {
                    grabCandidate.highlighter.OnHoverEnd();
                }
                else {
                    return;
                }
            }
            grabCandidate = g;
            g.highlighter.OnHoverBegin();
        }
    }

    void OnTriggerExit(Collider coll) {
        if(grabbed) return;
        var g = coll.GetComponentInParent<Socketable>();
        if(g && g == grabCandidate) {
            grabCandidate = null;
            g.highlighter.OnHoverEnd();
        }
    }

    internal void TryRotate(Vector2 mouseDelta)
    {
        rotateV = Mathf.Lerp(rotateV, rotateV - mouseDelta.y * rotateSpeed, rotateLerp);
        rotateH = Mathf.Lerp(rotateH, rotateH + mouseDelta.x * rotateSpeed, rotateLerp);
    }
}
