using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    public List<GameObject> trackRenderers;
    public GameObject offRenderer;

    public void PlayTrack(int trackIndex)
    {
        offRenderer.SetActive(false);
        trackRenderers[trackIndex].SetActive(true);
    }

    public void Stop()
    {
        offRenderer.SetActive(true);
        foreach(var track in trackRenderers) {
            track.SetActive(false);
        }
    }
}
