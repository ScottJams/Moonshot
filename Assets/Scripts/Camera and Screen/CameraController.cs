using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("The target for the camera to follow")]
    public GameObject followTarget;
    [Tooltip("The distance at which the camera is positioned away from the target on the Z axis")]
    public float cameraZ;
    [Tooltip("The current screen which the camera is locked to")]
    public LevelScreen currentScreen;
    

    // Private
    private Camera gameCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        gameCamera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        float halfCameraWidth =  gameCamera.orthographicSize * Screen.width / Screen.height;
        float halfCameraHeight = gameCamera.orthographicSize;

        // Set camera position vector to the targets position plus Z offset
        Vector3 cameraPosition = new Vector3(followTarget.transform.position.x,
            followTarget.transform.position.y,
            cameraZ);

        // Lock camera to horizontal bounds
        cameraPosition.x = Mathf.Clamp(cameraPosition.x,
            currentScreen.screenCollider.bounds.min.x + halfCameraWidth, 
            currentScreen.screenCollider.bounds.max.x - halfCameraWidth);

        // Lock camera to vertical bounds
        cameraPosition.y = Mathf.Clamp(cameraPosition.y,
            currentScreen.screenCollider.bounds.min.y + halfCameraHeight,
            currentScreen.screenCollider.bounds.max.y - halfCameraHeight);

        // Set camera position
        gameCamera.transform.position = cameraPosition;
    }

    // Checks if the collision is a new screen and moves appropriately
    public void UpdateCameraScreen(Collider2D collision)
    {
        LevelScreen newScreen = collision.gameObject.GetComponentInParent<LevelScreen>();
        if (newScreen != currentScreen)
        {
            currentScreen = newScreen;
        }
    }

}

