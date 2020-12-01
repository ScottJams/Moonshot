using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress
{
    // The identifier for this level
    public int levelID;

    // List of collectable moon data for this level
    public List<CollectableMoonData> collectableMoonData;

    // Whether this level has been unlocked or not
    public bool isUnlocked;

    // Default progress if none exists
    public LevelProgress(int levelID)
    {
        this.levelID = levelID;

        this.collectableMoonData = new List<CollectableMoonData>();

        for (int i = 1; i < 11; i++)
        {
            collectableMoonData.Add(new CollectableMoonData(levelID, i, false));
        }

        this.isUnlocked = (levelID == 1) ? true : false;

    }

    // Progress using existing data
    public LevelProgress(int levelID, List<CollectableMoonData> collectableMoonData, bool isUnlocked)
    {
        this.levelID = levelID;
        this.collectableMoonData = collectableMoonData;
        this.isUnlocked = isUnlocked;
    }

}
