using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldRecycle : MonoBehaviour {

    public float objectWidth;

	void Start () {
		
	}
	
	void Update () {
        if (transform.position.x <= -objectWidth)
            transform.Translate(Vector2.right * 2 * objectWidth);

    }
}
