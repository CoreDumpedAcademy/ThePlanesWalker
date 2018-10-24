using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour {
    public menuController Menu;
    public Text Pointer;
    bool onPause;
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

    public void PauseGame()
    {
        Menu.EnableMenu(!onPause);
        Time.timeScale = (onPause ? 1 : 0);
        onPause = !onPause;
    }

    private void Awake()
    {
        onPause = false;
        points = 0;
        Pointer.text = "Points: " + points;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            PauseGame();
    }
}
