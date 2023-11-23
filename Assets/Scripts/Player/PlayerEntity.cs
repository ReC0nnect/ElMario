using UnityEngine;


public class PlayerEntity : ITransform
{
    PlayerView Player;
    IMovingActions MovingActions;
    int HealthPoints = 1;


    bool IsDead;
    public Vector2 Position => Player.transform.position;

    public PlayerEntity(IMovingActions input)
    {
        MovingActions = input;

        MovingActions.OnMove += Move;
        MovingActions.OnJump += Jump;

        var playerSpawnPoint = Object.FindObjectOfType<PlayerSpawnPoint>(true);
        Player = playerSpawnPoint.Spawn(null);
        Player.OnHitHead += DestroyBlock;
        Player.OnHitFoot += FootAttack;
    }

    ~PlayerEntity()
    {
        Unsubscrube();
    }

    void Unsubscrube()
    {
        MovingActions.OnMove -= Move;
        MovingActions.OnJump -= Jump;
        Player.OnHitHead -= DestroyBlock;
        Player.OnHitFoot -= FootAttack;
    }

    void DestroyBlock(Collider2D collider)
    {
        if (IsDead) { return; }

        if (collider.TryGetComponent(out LevelObjectCollider levelObjectCollider)
            && levelObjectCollider.LevelObject is IHeadInteractable interactable)
        {
            interactable.Interact();
        }
    }

    void FootAttack(EnemyView enemy)
    {
        if (IsDead) { return; }

        enemy.AttackFromAbove();
    }

    void Move(float direction)
    {
        if (IsDead) { return; }

        Player.Move(direction);
    }

    void Jump(float power)
    {
        if (IsDead) { return; }

        Player.Jump(power);
    }

    public void Hit()
    {
        if (IsDead) { return; }

        Debug.Log("Hit");
        HealthPoints--;
        if (HealthPoints <= 0)
        {
            Debug.Log("Dead");
            IsDead = true;
            Player.StopMove();
            Player.Kill();
            Unsubscrube();
            UIController.ShowDeathScreen();
        }
    }
}
