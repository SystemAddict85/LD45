using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDespawner : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        var obs = other.GetComponent<Obstacle>();
        if(obs.spawner != null)
            obs.DisablePoolableObject();
    }
}
