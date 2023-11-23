using Physics;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;


public class PlayerView : MonoBehaviour, ITrigger2DEnterHandler, ITrigger2DExitHandler
{
    [SerializeField] Trigger2D HeadTrigger;
    [SerializeField] Trigger2D FootTrigger;
    [SerializeField] Rigidbody2D Rigidbody;
    [SerializeField] float MoveAirSpeed = 5f;
    [SerializeField] float MoveSpeed = 5f;
    [SerializeField] float JumpSpeed = 5f;
    [SerializeField] float MaxSpeed = 5f;
    [SerializeField] float MaxJumpSpeed = 5f;
    [SerializeField] Animator Animator;
    [Header("Attack from above")]
    [SerializeField] LayerMask AttackFromAboveLayer;
    [SerializeField] float AttackFromAboveTresshold = 0.2f;
    [SerializeField] float AttackFromAboveDistance = 1f;

    GroundedCounter IsGrounded = new();

    RaycastHit2D[] DownRaycast;

    public event Action<Collider2D> OnHitHead = delegate { };
    public event Action<EnemyView> OnHitFoot = delegate { };

    void Awake()
    {
        HeadTrigger.SetEnterHandler(this);
        FootTrigger.SetEnterHandler(this);
        FootTrigger.SetExitHandler(this);
        DownRaycast = new RaycastHit2D[16];
    }

    void Update()
    {
        if (!IsGrounded)
        {
            var hits = Physics2D.RaycastNonAlloc(FootTrigger.transform.position, Vector2.down, DownRaycast, AttackFromAboveDistance, AttackFromAboveLayer);
            if (hits > 0)
            {
                Debug.Log($"{hits} Hit from above");

                for (int i = 0; i < hits; i++)
                {
                    if (DownRaycast[i].collider.TryGetComponent(out EnemyViewCollider enemyCollider) 
                        && transform.position.y - AttackFromAboveTresshold > enemyCollider.EnemyView.Position.y)
                    {
                        OnHitFoot.Invoke(enemyCollider.EnemyView);
                        return;
                    }
                }
            }
        }
    }

    void OnDestroy()
    {
        HeadTrigger.RemoveEnterHandler(this);
        FootTrigger.RemoveEnterHandler(this);
        FootTrigger.RemoveExitHandler(this);
    }

    public void Move(float direction)
    {
        var velocity = Rigidbody.velocity;
        var speed = IsGrounded ? MoveSpeed : MoveAirSpeed;
        velocity.x += Time.deltaTime * direction * speed;
        velocity.x = Mathf.Clamp(velocity.x, -MaxSpeed, MaxSpeed);
        Rigidbody.velocity = velocity;
    }

    public void StopMove()
    {
        Rigidbody.velocity = Vector2.zero;
    }

    public void Jump(float power)
    {
        if (!IsGrounded)
        {
            return;
        }

        var velocity = Rigidbody.velocity;
        velocity.y += power * JumpSpeed;
        velocity.y = Mathf.Clamp(velocity.y, 0f, MaxJumpSpeed);
        Rigidbody.velocity = velocity;
    }

    void ITrigger2DEnterHandler.OnTriggerEnter(Collider2D collision, Trigger2D trigger)
    {
        if (trigger == HeadTrigger)
        {
            OnHitHead.Invoke(collision);
        }
        else if (trigger == FootTrigger)
        {
            IsGrounded += collision.gameObject;
        }
    }

    void ITrigger2DExitHandler.OnTriggerExit(Collider2D collision, Trigger2D trigger)
    {
        if (trigger == FootTrigger)
        {
            IsGrounded -= collision.gameObject;
        }
    }

    public void Kill()
    {
        Animator.SetTrigger("OnDeath");
    }
}
