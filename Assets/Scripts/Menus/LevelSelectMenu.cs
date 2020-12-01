using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    public MainMenu mainMenu;

    public void DisplayMainMenu()
    {
        gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void LoadColdMoon()
    {
        SceneManager.LoadScene("Level1");
    }

}
