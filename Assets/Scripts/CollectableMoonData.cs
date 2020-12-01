using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CollectableMoonData
{
    public readonly int levelID;
    public readonly int moonID;
    public readonly bool isCollected;

    public CollectableMoonData(int levelID, int moonID, bool isCollected)
    {
        this.levelID = levelID;
        this.moonID = moonID;
        this.isCollected = isCollected;
    }

}
