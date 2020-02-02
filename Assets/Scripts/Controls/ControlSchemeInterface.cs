using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSchemeInterface : MonoBehaviour
{
    public static ControlSchemeInterface instance;
    public CameraControl cameraControl;

    void Start() {
        if(instance) {
            throw new System.Exception("There can only be one ControlSchemeInterface at a time");
        }
        instance = this;

    }
}
