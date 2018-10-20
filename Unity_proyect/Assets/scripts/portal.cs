using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    GameObject OtherPortal;

    private void Awake()
    {
        OtherPortal = GameObject.Find("PortalExit");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position = OtherPortal.transform.position;
        }
    }
}
