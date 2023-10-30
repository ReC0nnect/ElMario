using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnPoint<T>
{
    T SpawnedInstance { get; }

    T Spawn();
}
