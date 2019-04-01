using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIMangaer : MonoBehaviour
{
    public GameObject menu;
    public GameObject discriptionText;
    public GameObject exit;

    public void Start()
    {
        menu.SetActive(true);
        discriptionText.SetActive(false);
        exit.SetActive(false);

    }
    public void StartButtonClick()
    {
        SceneManager.LoadScene("VRShootingGame");
    }

    public void DiscriptionButtonClick()
    {
        menu.SetActive(false);
        discriptionText.SetActive(true);
    }
    public void DiscriptionExitButtonClick()
    {
        discriptionText.SetActive(false);
        menu.SetActive(true);
    }
    public void ExitButtonClick()
    {
        menu.SetActive(false);
        exit.SetActive(true);
    }
    public void exitYESButtonClick()
    {
        Application.Quit();
    }
    public void exitNOButtonClcik() 
    {
        exit.SetActive(false);
        menu.SetActive(true);
    }

}
