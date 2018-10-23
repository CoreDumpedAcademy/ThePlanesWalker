using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalSpawner : MonoBehaviour {
    public GameObject PortalEnterPrefab;
    public GameObject PortalExitPrefab;
    bool aviableToShoot;
    string actualState;

    private void Start()
    {
        actualState = "portalEnter";
        aviableToShoot = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bouncer") || collision.gameObject.CompareTag("Ground"))
            aviableToShoot = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        aviableToShoot = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && aviableToShoot)
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
            }
        }
    }
}
