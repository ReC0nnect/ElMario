using System;
using UnityEngine;


public class UIController : MonoBehaviour
{
    [SerializeField] DeathScreen DeathScreen;

    public static UIController Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    public static void ShowDeathScreen()
    {
        Instance.DeathScreen.Show();
    }
}
