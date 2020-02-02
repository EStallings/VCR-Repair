using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyChecker : MonoBehaviour
{
    public Socket[] allSockets;
    public bool isAllAssembled;
    public bool isAllWorking;

    // Update is called once per frame
    void Update()
    {
        isAllAssembled = true;
        isAllWorking = true;
        foreach(Socket socket in allSockets) {
            if(!socket.currentOccupant || socket.currentOccupant.removalAmount > 0) {
                isAllAssembled = false;
                isAllWorking = false;
            }
            if(!socket.currentOccupant.lastFunctioning) {
                isAllWorking = false;
            }
        }
    }
}
