using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectCollider : MonoBehaviour
{
    [SerializeField] LevelObject LinkedLevelObject;

    public LevelObject LevelObject => LinkedLevelObject;
}
