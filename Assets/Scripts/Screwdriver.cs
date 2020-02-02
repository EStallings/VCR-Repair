using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screwdriver : MonoBehaviour
{
    public string screwModel;

    public Socketable mySocketable;
    public Socketable currentScrew;
    
    // Start is called before the first frame update
    void Start()
    {
        mySocketable = GetComponent<Socketable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mySocketable.grabber) {
            transform.up = transform.position - mySocketable.grabber.aimer.raycastStartPos.position;
            if(!currentScrew) {
                transform.localPosition = Vector3.zero;
            }
            AlignToScrew();
        }
    }
    
    void AlignToScrew() {
        if(!currentScrew) return;
        var oldParent = transform.parent;
        transform.parent = currentScrew.transform;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.parent = oldParent;
    }

    public void Turn(Vector2 mouseDelta)
    {
        float delta = mouseDelta.x;
        if(currentScrew && currentScrew.model == screwModel) {
            currentScrew.removalAmount = Mathf.Clamp(currentScrew.removalAmount + mouseDelta.x * Time.deltaTime * currentScrew.removalRate, 0, 1);
        }
    }
}
