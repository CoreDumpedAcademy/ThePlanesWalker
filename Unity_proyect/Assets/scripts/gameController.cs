using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour {
    public static gameController Instance;
    public menuController Menu;
    public Text Pointer;
    public float XWorldScrollSpeed;
    bool onPause;
    int points;
    float timeOnPause;

    public int GetPoints()
    {
        return points;
    }

    public void SetPoints(int value)
    {
        points = value;
        Pointer.text = "Points: " + points;
    }

    public void PauseGame(bool value)
    {
        if (value != onPause)
        {
            Menu.EnableMenu(value);
            if (onPause)
                Time.timeScale = 1;
            else
            {
                Time.timeScale = 0;
            }
            onPause = value;
        }
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
        onPause = false;
        points = 0;
        Pointer.text = "Points: " + points;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseGame(!onPause);
        }
    }

    private void OnDestroy()
    {
        if (gameController.Instance == this)
            gameController.Instance = null;
    }
}
