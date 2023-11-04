using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnPoint
{
    void Spawn(Transform parent);
}

public interface ISpawnPoint<T> : ISpawnPoint
{
    T SpawnedInstance { get; }


    void ISpawnPoint.Spawn(Transform parent)
    {
        Spawn(parent);
    }

    new T Spawn(Transform parent);
}
