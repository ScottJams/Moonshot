using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectOnCollision : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject bubble1;
    public GameObject bubble2;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelManager.TotalMoonsCollected() < 10)
        {
            bubble1.SetActive(true);
        } 
        else
        {
            bubble2.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bubble1.SetActive(false);
        bubble2.SetActive(false);
    }
}
