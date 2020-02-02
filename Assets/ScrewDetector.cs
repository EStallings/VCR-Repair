using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewDetector : MonoBehaviour
{
    public Screwdriver driver;
    // Start is called before the first frame update
    void Start()
    {
        driver = GetComponentInParent<Screwdriver>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Socketable s = other.GetComponent<Socketable>();
        if(s && s.socketType == Socket.SocketType.Screw && s.currentSocket) {
            driver.currentScrew = s;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Socketable s = other.GetComponent<Socketable>();
        if(s && s == driver.currentScrew) {
            driver.currentScrew = null;
        }
    }
}
