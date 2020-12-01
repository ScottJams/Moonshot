using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float scrollSpeed;
    private float startPosX;
    private float widthToScroll;

    private void Start()
    {
        startPosX = transform.position.x;
        widthToScroll = Screen.width;
    }

    void Update()
    {
        Vector3 movementVector = Vector2.left * scrollSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + movementVector;

        if (newPosition.x < startPosX - widthToScroll)
        {
            transform.position = new Vector3(startPosX, 
                transform.position.y, 
                transform.position.z);
        } 
        else {
            transform.Translate(movementVector);
        }

        
    }
}
