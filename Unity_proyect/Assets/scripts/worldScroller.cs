using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldScroller : MonoBehaviour
{
    Rigidbody2D rigOfMine;
    void Awake()
    {
        rigOfMine = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigOfMine.velocity = Vector2.left * gameController.Instance.XWorldScrollSpeed;
    }
}
