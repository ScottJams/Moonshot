using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMoon : MonoBehaviour
{
    public int levelID;
    public int moonID;
    public bool collected = false;
    public LevelManager levelManager;

    private void Awake()
    {
        gameObject.SetActive(!collected);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollectMoon();
    }

    public void CollectMoon()
    {
        collected = true;
        gameObject.SetActive(false);
        levelManager.UpdateMoonDataFromScene();
        levelManager.UpdateMoonPreviews();
        AudioManager.SharedInstance.PlaySoundEffect(SoundEffect.collectMoon);
    }

    public bool CollectedStatus()
    {
        return collected;
    }

}
