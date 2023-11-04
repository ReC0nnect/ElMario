using Physics;
using System;
using UnityEngine;


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

    GroundedCounter IsGrounded = new();

    public event Action<Collider2D> OnHitHead = delegate { };

    void Awake()
    {
        HeadTrigger.SetEnterHandler(this);
        FootTrigger.SetEnterHandler(this);
        FootTrigger.SetExitHandler(this);
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
}
