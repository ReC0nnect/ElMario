using UnityEngine;


public class BrickSpawnPoint : MonoBehaviour, ISpawnPoint<Brick>
{
    [SerializeField] Brick BrickPrefab;

    public Brick SpawnedInstance { get; private set; }

    void OnEnable()
    {
        gameObject.SetActive(false);
    }

    public Brick Spawn(Transform parent)
    {
        SpawnedInstance = Instantiate(BrickPrefab, parent);

        SpawnedInstance.transform.position = transform.position;
        return SpawnedInstance;
    }
}
