using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject[] Levels;

    GameObject Current;
    int NextLevelIndex;


    public GameObject CreateNextLevel()
    {
        Current = Instantiate(Levels[NextLevelIndex++], transform);
        return Current;
    }
}
