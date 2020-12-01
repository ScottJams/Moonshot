using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // Singleton
    private static SaveManager _instance;
    public static SaveManager SharedInstance { get { return _instance; } }

    // List of current level progress
    private List<LevelProgress> levelProgresses;

    // Enforce singleton on awake
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } 
        else
        {
            _instance = this;
            if (levelProgresses == null)
            {
                NewSaveData();
            }
            
        }

    }

    // Creates default level progresses (new save)
    private void NewSaveData()
    {
        levelProgresses = new List<LevelProgress>
        {
            new LevelProgress(1),
            new LevelProgress(2),
            new LevelProgress(3)
        };
    }

    // Returns the level progress for a given level ID
    public LevelProgress ProgressForLevel(int levelID)
    {
        return levelProgresses.Find(x => x.levelID == levelID);
    }

    // Loads level progresses from disk


    // Adds the level progress given to the list of level progress. Replaces old progress if appliicable.
    public void SaveLevelProgress(LevelProgress newLevelProgress)
    {
        var matchingLevelSave = levelProgresses.Find(x => x.levelID == newLevelProgress.levelID);

        if (matchingLevelSave != null)
        {
            levelProgresses.Remove(matchingLevelSave);
        }

        levelProgresses.Add(newLevelProgress);
    }

    // Returns if the given moon has been collected on the given level
    public bool HasMoonBeenCollected(int levelID, int moonID)
    {
        LevelProgress levelProgress = levelProgresses.Find(x => x.levelID == levelID);
        if (levelProgress != null)
        {
            CollectableMoonData moon = levelProgress.collectableMoonData.Find(x => x.moonID == moonID);
            return moon.isCollected;
        }
        return false;
    }

    // Serialize the progress
    public void SerializeSaveData()
    {

    }

}
