using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Highlighter))]
[RequireComponent(typeof(Collider))]
public class Socketable : MonoBehaviour
{
    [Header("Debug Stuff")]
    public bool lastFunctioning;
    public bool lastRetained;

    [Header("Real Stuff")]
    public int priority;
    public string model;
    public Socket.SocketType socketType;
    public float removalAmount;
    public float threadPitch = 1;
    public float removalRate = 1;
    public float removalHeight = 0.05f;
    public bool functioning = false;

    public bool CanBeRemoved()
    {
        return !IsRetainedByScrews() && (socketType == Socket.SocketType.Screw ? removalAmount >= 0.99 : true);
    }

    public bool canBeMoved = true;
    public bool defaultKinematic = false;
    public bool resetPositionOnPickup = false;
    public bool resetPositionOnDrop = false;

    public Socket defaultSocket;

    public Socket currentSocket;
    public Rigidbody myRb;
    public Highlighter highlighter;
    public Collider myColl;
    public Grabber grabber;
    public Quaternion startRotation;
    public Screwdriver myScrewdriverScript;

    public int originalLayer;
    public Vector3 startPosition;

    public bool IsFunctioning() {
        return functioning && removalAmount == 0;
    }

    public bool IsRetainedByScrews() {
        if(!currentSocket) return false;
        foreach(var screw in currentSocket.screwSockets) {
            if(screw.currentOccupant) {
                return true;
            }
        }
        return false;
    }
    
    public void Grab(Grabber grab) {
        myRb.isKinematic = true;
        originalLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("HeldObject");
        grabber = grab;
        startRotation = transform.rotation;
        startPosition = transform.position;
        if(canBeMoved) {
            transform.parent = grabber.transform;
            if(resetPositionOnPickup) {
                transform.localPosition = Vector3.zero;
            }
        }
        if(currentSocket) {
            currentSocket.currentOccupant = null;
        }
    }

    public void Drop() {
        gameObject.layer = originalLayer;
        grabber = null;
        if(canBeMoved) {
            transform.parent = null;
        }
        if(currentSocket) {
            // Drop into socket logic
            transform.parent = currentSocket.transform;
            transform.localEulerAngles = Vector3.zero;
            transform.localPosition = Vector3.zero;
            currentSocket.currentOccupant = this;
            myRb.isKinematic = true;
        } else {
            // Drop
            if(!defaultKinematic) {
                myRb.isKinematic = false;
            }
            if(canBeMoved) {
                transform.parent = null;
                transform.position = transform.position + Vector3.up * 0.2f;
            }
            if(resetPositionOnPickup) {
                transform.position = startPosition;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!defaultSocket) defaultSocket = GetComponentInParent<Socket>();
        myColl = GetComponent<Collider>();
        myRb = GetComponent<Rigidbody>();
        highlighter = GetComponent<Highlighter>();
        myScrewdriverScript = GetComponent<Screwdriver>();
        if(model == "") {
            throw new System.Exception("You must define a model name for socketable: " + this);
        }
        if(defaultSocket) {
            originalLayer = gameObject.layer;
            currentSocket = defaultSocket;
            Drop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        lastFunctioning = IsFunctioning();
        lastRetained = IsRetainedByScrews();
        if(!canBeMoved && grabber) {
            transform.rotation = startRotation * grabber.transform.rotation;
        }
        if(canBeMoved && currentSocket && !grabber) {
            transform.localPosition = -Vector3.up * removalAmount * removalHeight;
            transform.localEulerAngles = Vector3.up * removalAmount * threadPitch;
        }
    }

    void OnTriggerEnter(Collider coll) {
        var socket = coll.GetComponent<Socket>();
        if(socket && socket.socketType == socketType) {
            currentSocket = socket;
        }
    }

    void OnTriggerExit(Collider coll) {
        var socket = coll.GetComponent<Socket>();
        if(socket && socket == currentSocket) {
            currentSocket = null;
        }
    }
}
