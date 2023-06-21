using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform Landing;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = Landing.position;
    }
}
