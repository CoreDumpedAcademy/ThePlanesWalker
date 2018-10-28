using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

    public int POINTS_MULTIPLIER = 8; // Controlls the amount fo points the user earns by frame
    public static gameController Instance;
    public menuController Menu;
    public Text pointsText;
    public float XWorldScrollSpeed;
    bool onPause;
    public int points;
    float timeOnPause;

    public void Start()
    {
        GameObject textObject = GameObject.FindGameObjectWithTag("scoreText");
        if (textObject != null) pointsText = textObject.GetComponent<Text>();
        else Debug.Log("ScoreText no encontrado");
    }

    public int GetPoints()
    {
        return points;
    }

    public void SetPoints(int totalPoints)
    {
        points = totalPoints;
        pointsText.text = "Score: " + points;
    }

    public int UpdatePoints()
    {
        points = (int)(Time.timeSinceLevelLoad * POINTS_MULTIPLIER);
        pointsText.text = "Score: " + points;
        return points;
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
        pointsText.text = "Score: " + points;
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
