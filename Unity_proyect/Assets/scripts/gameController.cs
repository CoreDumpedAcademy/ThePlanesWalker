using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

    public int POINTS_MULTIPLIER = 111; // readonly == java's final property, which means this is a constant
    public static gameController Instance;
<<<<<<< HEAD
    public Text Pointer;
    public float XWorldScrollSpeed;
    int points;
=======
    public menuController Menu;
    public Text pointsText;
    public float XWorldScrollSpeed;
    bool onPause;
    public int points;
    float timeOnPause;
>>>>>>> PipiOnDrugs

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


    private void Awake()
    {
        if (gameController.Instance == null)
            gameController.Instance = this;
        else if (gameController.Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("WARNING: gameController instanciado por 2a vez");
        }
<<<<<<< HEAD
        points = 0;
        Pointer.text = "Points: " + points;
=======
        onPause = false;
        pointsText.text = "Score: " + points;
>>>>>>> PipiOnDrugs
    }

    private void OnDestroy()
    {
        if (gameController.Instance == this)
            gameController.Instance = null;

    }
}
