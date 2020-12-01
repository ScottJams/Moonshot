using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoonPreview : MonoBehaviour
{
    public int levelID;
    public int moonID;
    private Image moonImage;

    private void Awake()
    {
        moonImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        UpdateMoonPreview();
    }

    public void UpdateMoonPreview()
    {
        bool moonCollected = SaveManager.SharedInstance.HasMoonBeenCollected(levelID, moonID);
        if (!moonCollected)
        {
            var moonColor = moonImage.color;
            moonColor.a = 0.3f;
            moonImage.color = moonColor;
        } 
        else
        {
            var moonColor = moonImage.color;
            moonColor.a = 1;
            moonImage.color = moonColor;
        }
    }


}
