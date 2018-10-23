using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetManager : MonoBehaviour {
    public GameObject PortalEnterPrefab;
    public GameObject PortalExitPrefab;
    bool aviableToSpawnPortals;
    bool onWall;
    string actualState;

    private void Start()
    {
        actualState = "portalEnter";
        onWall = false;
        aviableToSpawnPortals = true;
    }

    public void EnableSpawnPoints()
    {
        aviableToSpawnPortals = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bouncer") || collision.gameObject.CompareTag("Ground"))
            onWall = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onWall = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && aviableToSpawnPortals && onWall)
        {
            if(actualState == "portalEnter")
            {
                actualState = "portalExit";
                Instantiate(PortalEnterPrefab, transform.position, Quaternion.Euler(0, 0, 0));
            }
            else if(actualState == "portalExit")
            {
                actualState = "portalEnter";
                Instantiate(PortalExitPrefab, transform.position, Quaternion.Euler(0, 0, 0));
                aviableToSpawnPortals = false;
            }
        }
    }
}
