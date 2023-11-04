using UnityEngine;


public class PlayerEntity : ITransform
{
    PlayerView Player;
    IMovingActions MovingActions;

    public Vector2 Position => Player.transform.position;

    public PlayerEntity(IMovingActions input)
    {
        MovingActions = input;

        MovingActions.OnMove += Move;
        MovingActions.OnJump += Jump;

        var playerSpawnPoint = Object.FindObjectOfType<PlayerSpawnPoint>(true);
        Player = playerSpawnPoint.Spawn(null);
        Player.OnHitHead += DestroyBlock;
    }

    void DestroyBlock(Collider2D collider)
    {
        if (collider.TryGetComponent(out LevelObjectCollider levelObjectCollider)
            && levelObjectCollider.LevelObject is IHeadInteractable interactable)
        {
            interactable.Interact();
        }
    }

    ~PlayerEntity()
    {
        MovingActions.OnMove -= Move;
        MovingActions.OnJump -= Jump;
    }

    void Move(float direction)
    {
        Player.Move(direction);
    }

    void Jump(float power)
    {
        Player.Jump(power);
    }
}
