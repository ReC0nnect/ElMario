using UnityEngine;

public enum MovingDirection
{
    Left = 0,
    Right = 1
}

public class EnemySpawnPoint : MonoBehaviour, ISpawnPoint<EnemyView>, ISessionInit
{
    [SerializeField] EnemyView EnemyPrefab;
    [SerializeField] MovingDirection Direction;

    public EnemyView SpawnedInstance { get; private set; }

    SessionEntity Session;

    void OnEnable()
    {
        gameObject.SetActive(false);
    }

    public EnemyView Spawn(Transform parent)
    {
        if (SpawnedInstance != null)
        {
            return SpawnedInstance;
        }

        SpawnedInstance = Instantiate(EnemyPrefab, parent);
        SpawnedInstance.SetDirection(Direction == MovingDirection.Right ? Vector2.right : Vector2.left);
        if (SpawnedInstance is ISessionInit sessionInit)
        {
            sessionInit.OnSessionInit(Session);
        }

        SpawnedInstance.transform.position = transform.position;
        return SpawnedInstance;
    }

    void ISessionInit.OnSessionInit(SessionEntity session)
    {
        Session = session;
    }
}
