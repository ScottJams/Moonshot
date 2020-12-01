using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public MainMenu mainMenu;

    public void DisplayMainMenu()
    {
        gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

}
