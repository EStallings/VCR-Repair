using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusIndicator : MonoBehaviour
{
    public Light myLight;
    public Socket socket;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myLight.enabled = socket.IsFunctioning();
    }
}
