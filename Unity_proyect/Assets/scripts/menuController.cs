using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour {
    public GameObject Menu;

    public void LoadScene (string scene)
    {
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
