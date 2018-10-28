using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour {
    public static gameController Instance;
    public Text Pointer;
    public float XWorldScrollSpeed;
    int points;

    public int GetPoints()
    {
        return points;
    }

    public void SetPoints(int value)
    {
        points = value;
        Pointer.text = "Points: " + points;
    }


    private void Awake()
    {
        if (gameController.Instance == null)
            gameController.Instance = this;
        else if (gameController.Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("WARNING: gameController instanciado por 2a vez");
        }
        points = 0;
        Pointer.text = "Points: " + points;
    }

    private void OnDestroy()
    {
        if (gameController.Instance == this)
            gameController.Instance = null;
    }
}
