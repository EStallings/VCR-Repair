using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCRController : MonoBehaviour
{

    public TV tv;

    public Socket AVBoard;
    public Socket PowerBoard;
    public Socket PowerPlug;
    public Socket Audio1;
    public Socket Audio2;
    public Socket VideoOut;

    public Socket PlaybackBoard;
    public Socket PlaybackMotor;

    public Socket Deck;
    public Socket EjectMotor;

    public Socket VHSSocket;
    public Transform VHSRetract;
    public Transform VHSExtend;
    public bool VHSRetractMode;
    public float retractSpeed = 1.5f;

    void Update() {
        if(Deck.IsFunctioning()) {
            if(!VHSSocket.currentOccupant) {
                VHSRetractMode = true;
            }
            if(VHSRetractMode && VHSSocket.currentOccupant) {
                VHSSocket.transform.position = Vector3.Lerp(VHSSocket.transform.position, VHSRetract.position, retractSpeed * Time.deltaTime);
            }
            if(!VHSRetractMode) {
                VHSSocket.transform.position = Vector3.Lerp(VHSSocket.transform.position, VHSExtend.position, retractSpeed * Time.deltaTime);
            }
        }
    }

    public void Eject() {
        if(Deck.IsFunctioning()) {
            if(VHSSocket.currentOccupant) {
                VHSRetractMode = false;
            }
        }
    }

    public void Play() {

    }

    public void Pause() {

    }

    public void Rewind() {

    }

    public void FastForward() {

    }


}
