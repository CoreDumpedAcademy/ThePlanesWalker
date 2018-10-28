using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour {
    public GameObject Menu;

    public void LoadScene (string scene)
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Debug.Log("Exit :D");
        Application.Quit();
    }

    public void EnableMenu(bool value)
    {
        Menu.SetActive(value);
    }
}
