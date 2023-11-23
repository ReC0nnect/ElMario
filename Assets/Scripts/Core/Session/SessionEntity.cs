using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionEntity : IUpdatable, IFixedUpdatable
{
    static SessionEntity Current;

    public InputSystem Input;
    public CameraSystem Camera;
    public SpawnSystem Spawn;
    public PlayerEntity Player;

    readonly IUpdatable[] Updatables;


    public SessionEntity()
    {
        Input = new InputSystem();
        Spawn = new SpawnSystem();
        Player = new PlayerEntity(Input);
        Camera = new CameraSystem(Player);

        Updatables = new IUpdatable[]
        {
            Input,
            Camera
        };

        var initableObjects = Object.FindObjectsOfType<MonoBehaviour>(true).OfType<ISessionInit>();
        foreach (var obj in initableObjects)
        {
            obj.OnSessionInit(this);
        }
    }

    public static SessionEntity Create()
    {
        return Current ??= new SessionEntity();
    }

    public void Update()
    {
        foreach (var item in Updatables)
        {
            item.Update();
        }
    }

    public void FixedUpdate()
    {

    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
    }

    public void CleanUp()
    {
        Current = null;
    }
}
