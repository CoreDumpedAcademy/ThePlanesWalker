using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    targetManager tManager;

    private void Awake()
    {
        tManager = GameObject.Find("TargetG").GetComponent<targetManager>();
    }

    public void DestroyPortals(GameObject AditionalPortal)
    {
        Destroy(AditionalPortal);
        Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tManager.EnableSpawnPoints();
            GameObject PortalExit = GameObject.Find("PortalExit(Clone)");
            if (PortalExit != null)
            {
                collision.transform.position = PortalExit.transform.position;
                DestroyPortals(PortalExit);
            }
        }
    }
}
