using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    playerController Spawner;

    private void Awake()
    {
        Spawner = GameObject.Find("Player").GetComponent<playerController>();
    }

    public void DestroyPortals(GameObject AditionalPortal)
    {
        if (AditionalPortal != null)
            Destroy(AditionalPortal);
        Destroy(this.gameObject);
        Spawner.EnableSpawnPoints();
        Spawner.EnableEnterPortal();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject PortalExit = GameObject.Find("PortalExit(Clone)");
            if (PortalExit != null)
            {
                collision.transform.position = PortalExit.transform.position;
                DestroyPortals(PortalExit);
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
            DestroyPortals(GameObject.Find("PortalExit(Clone)"));
    }
}

