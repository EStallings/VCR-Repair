using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public List<Transform> respawnPoints;

    void OnCollisionEnter(Collision coll)
    {
        Transform randomPoint = respawnPoints[Random.Range(0, respawnPoints.Count)];
        coll.rigidbody.MovePosition(randomPoint.position);
        coll.rigidbody.velocity = Vector3.zero;
    }
}
