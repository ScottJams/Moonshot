using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int levelID;
    [SerializeField] public static bool gameIsPaused = false;

    private LevelProgress levelProgress;
    [SerializeField] private List<CollectableMoon> moonsInScene;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private List<MoonPreview> moonPreviews;
    [SerializeField] private Animation moonPreviewsAnimation;

    void Start()
    {
        levelProgress = SaveManager.SharedInstance.ProgressForLevel(this.levelID);
        EnableMoons();
    }

    private void Update()
    {
        bool pauseInput = Input.GetButtonDown("Pause");
        if (pauseInput && gameIsPaused)
        {
            ResumeGame();
        }
        else if (pauseInput && !gameIsPaused)
        {
            PauseGame();
        }
    }

    void EnableMoons()
    {
        foreach (CollectableMoon collectableMoon in moonsInScene)
        {
            CollectableMoonData moonData = levelProgress.collectableMoonData.Find(x => x.moonID == collectableMoon.moonID &&
            x.levelID == collectableMoon.levelID);
            collectableMoon.gameObject.SetActive(!moonData.isCollected);
            collectableMoon.collected = moonData.isCollected;
        }
    }

    void SaveLevelProgress()
    {
        SaveManager.SharedInstance.SaveLevelProgress(levelProgress);
    }

    public void UpdateMoonDataFromScene()
    {

        foreach (CollectableMoon moon in moonsInScene)
        {
            CollectableMoonData moonData = new CollectableMoonData(moon.levelID, 
                moon.moonID, 
                moon.CollectedStatus());
            CollectableMoonData existingData = 
                levelProgress.collectableMoonData.Find(x => x.levelID == moonData.levelID && x.moonID == moonData.moonID);
            levelProgress.collectableMoonData.Remove(existingData);
            levelProgress.collectableMoonData.Add(moonData);
        }

    }

    // Updates the collectable moon UI
    public void UpdateMoonPreviews()
    {
        foreach (MoonPreview moonPreview in moonPreviews)
        {
            moonPreview.UpdateMoonPreview();
        }

        moonPreviewsAnimation.Play();
    }
    
    // Updates the moon status from scene, saves the progress and loads the menu scene
    public void ExitToLevelSelect()
    {
        ResumeGame();
        UpdateMoonDataFromScene();
        SaveLevelProgress();
        gameIsPaused = false;
        SceneManager.LoadScene("Main Menu");
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        StartCoroutine(Unpause());
    }

    IEnumerator Unpause()
    {
        yield return new WaitForSeconds(0.1f);
        gameIsPaused = false;
    }

}
