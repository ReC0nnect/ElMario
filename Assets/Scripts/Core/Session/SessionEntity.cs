using UnityEngine;

public class SessionEntity : IUpdatable, IFixedUpdatable
{
    static SessionEntity Current;

    public InputSystem Input;
    public CameraSystem Camera;
    public PlayerEntity Player;

    readonly IUpdatable[] Updatables;


    public SessionEntity()
    {
        Input = new InputSystem();
        Player = new PlayerEntity(Input);
        Camera = new CameraSystem(Player);

        Updatables = new IUpdatable[]
        {
            Input,
            Camera
        };
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
}
