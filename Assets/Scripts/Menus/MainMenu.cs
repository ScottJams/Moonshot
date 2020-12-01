using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelSelectMenu levelSelectMenu;
    public OptionsMenu optionsMenu;

    public void DisplayLevelSelect()
    {
        gameObject.SetActive(false);
        levelSelectMenu.gameObject.SetActive(true);
    }

    public void DisplayOptionsMenu()
    {
        gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Game has been quit");
        Application.Quit();
    }
}
