using UnityEngine;


public class CoinBrickSpawnPoint : MonoBehaviour, ISpawnPoint<CoinBrick>
{
    [SerializeField] CoinBrick Prefab;
    [SerializeField] int Amount;

    public CoinBrick SpawnedInstance { get; private set; }

    void OnEnable()
    {
        gameObject.SetActive(false);
    }

    public CoinBrick Spawn(Transform parent)
    {
        SpawnedInstance = Instantiate(Prefab, parent);
        SpawnedInstance.SetAmount(Amount);

        SpawnedInstance.transform.position = transform.position;
        return SpawnedInstance;
    }
}
