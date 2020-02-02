using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    [Header("Debug Stuff")]
    public bool lastFunctioning;

    [Header("Actual stuff")]
    public List<string> modelsAllowed;
    public SocketType socketType;
    public Socketable currentOccupant;
    public Socket[] dependencies;

    public enum SocketType {
        PowerPlug,
        PowerBoard,
        Deck,
        AVBoard,
        DeckController,
        SpoolController,
        Motor,
        Screw,
        Case,
    }
    
    public bool IsFunctioning() {
        if (!currentOccupant || !currentOccupant.functioning) {
            return false;
        }
        if(currentOccupant.socketType != socketType) {
            throw new System.Exception("Socket Mismatch! " + this + " vs " + currentOccupant);
        }
        if(!modelsAllowed.Contains(currentOccupant.model)) {
            return false;
        }
        foreach(var socket in dependencies) {
            if(!socket.IsFunctioning()) return false;
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(modelsAllowed.Count == 0) {
            throw new System.Exception("You must define at least one accepted model for this socket: "  + this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        lastFunctioning = IsFunctioning();
    }
}
