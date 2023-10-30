using UnityEngine;

public class CameraSystem : IUpdatable
{
    CameraFollower FollowerInternal;
    CameraFollower Follower
    {
        get
        {
            if (!FollowerInternal)
            {
                FollowerInternal = Object.FindObjectOfType<CameraFollower>();
            }
            return FollowerInternal;
        }
    }

    ITransform Target;
    Vector2 LastTargetPosition;

    public CameraSystem(PlayerEntity player)
    {
        Target = player;
        Follower.InitPosition(Target.Position);
    }

    void IUpdatable.Update()
    {
        if (Target.Position.x - LastTargetPosition.x > 0)
        {
            Follower.SetPosition(Target.Position);
            LastTargetPosition = Target.Position;
        }
    }
}
