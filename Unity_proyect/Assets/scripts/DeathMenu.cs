using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public static bool Muerto = false;

    public void Start()
    {
        Muerto = false;
    }
   

    public GameObject DeathMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) //aqui deberia ir la condicion de muerte
        {
            if (!Muerto)
            {
                Morir();
            }
        }
    }

    public void Morir()
    {
        DeathMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Muerto = true;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        Time.timeScale = 1f;
        Muerto = false;
        Debug.Log("Reiniciarndo juego...");
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Muerto = false;
        Time.timeScale = 1f;
    }
}