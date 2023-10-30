using UnityEngine;


public class PlayerSpawnPoint : MonoBehaviour, ISpawnPoint<PlayerView>
{
    [SerializeField] PlayerView PlayerPrefab;

    public PlayerView SpawnedInstance { get; private set; }

    void OnEnable()
    {
        gameObject.SetActive(false);
    }

    public PlayerView Spawn()
    {
        SpawnedInstance = Instantiate(PlayerPrefab);

        SpawnedInstance.transform.position = transform.position;
        return SpawnedInstance;
    }
}
