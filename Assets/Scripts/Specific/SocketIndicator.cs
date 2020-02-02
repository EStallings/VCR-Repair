using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketIndicator : MonoBehaviour
{
    public Grabber grabber;
    public Transform indicator;
    public bool test;
    public float spinSpeed = 1;
    public float bobSpeed = 1;
    public float bobAmount = 0.2f;
    public float bobV;
    public bool draw;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        draw = false;
        if(grabber.grabbed)
        {
            Socketable s = grabber.grabbed.GetComponent<Socketable>();
            if(s && s.currentSocket) {
                draw = true;
            }
        }
        if(draw || test) {
            if(!indicator.gameObject.activeInHierarchy) {
                indicator.gameObject.SetActive(true);
            }
            indicator.Rotate(new Vector3(0, Time.deltaTime * spinSpeed, 0));
            bobV += bobSpeed * Time.deltaTime;
            indicator.localPosition += Vector3.up * Mathf.Sin(bobV) * bobAmount;
        }
        if(!test && !draw && indicator.gameObject.activeInHierarchy) {
            indicator.gameObject.SetActive(false);
        }
    }
}
