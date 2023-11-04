using UnityEngine;


public class PlayerSpawnPoint : MonoBehaviour, ISpawnPoint<PlayerView>
{
    [SerializeField] PlayerView PlayerPrefab;

    public PlayerView SpawnedInstance { get; private set; }

    void OnEnable()
    {
        gameObject.SetActive(false);
    }

    public PlayerView Spawn(Transform parent)
    {
        if (SpawnedInstance != null)
        {
            return SpawnedInstance;
        }

        SpawnedInstance = Instantiate(PlayerPrefab, parent);

        SpawnedInstance.transform.position = transform.position;
        return SpawnedInstance;
    }
}
