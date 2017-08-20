using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    PlayerController pc;

    void Start ()
    {
        pc = PlayerController.instance;
    }

    void OnTriggerEnter (Collider col)
    {
        col.transform.position = pc.spawnPoint;
        pc.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
