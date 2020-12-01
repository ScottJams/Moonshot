using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    // Runs before a scene gets loaded
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadUtilities()
    {
        GameObject utilitiesPrefab = Resources.Load<GameObject>("Utilities");
        GameObject utilities = Instantiate(utilitiesPrefab);
        GameObject.DontDestroyOnLoad(utilities);
    }

}
