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
        pc.Respawn();
    }
}
