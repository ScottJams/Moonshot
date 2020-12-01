using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSpriteOnCollision : MonoBehaviour
{

    public GameObject sprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sprite.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sprite.SetActive(false);
    }


}
