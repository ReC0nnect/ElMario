using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem
{
    World World;

    public SpawnSystem()
    {
        World = Object.FindObjectOfType<World>();
        var level = World.CreateNextLevel();
        SpawnAllLevelObjects(level);
    }

    void SpawnAllLevelObjects(GameObject level)
    {
        var spawnPoints = level.GetComponentsInChildren<ISpawnPoint>(true);
        foreach (var point in spawnPoints)
        {
            point.Spawn(level.transform);
        }
    }
}
