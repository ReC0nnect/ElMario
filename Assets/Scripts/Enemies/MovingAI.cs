using System.Collections;
using UnityEngine;

public class MovingAI : MonoBehaviour, IBotLogic
{
    EnemyView Enemy;

    public void Init(EnemyView enemy)
    {
        Enemy = enemy;
    }

    void Start()
    {
        StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        for (; ; )
        {
            Enemy.Move();
            yield return null;
        }
    }
}
