using UnityEngine;


public class PlayerView : MonoBehaviour
{
    [SerializeField] Rigidbody2D Rigidbody;
    [SerializeField] float Speed = 5f;
    [SerializeField] float JumpSpeed = 5f;
    [SerializeField] float MaxSpeed = 5f;
    [SerializeField] float MaxJumpSpeed = 5f;


    public void Move(float direction)
    {
        var velocity = Rigidbody.velocity;
        velocity.x += Time.deltaTime * direction * Speed;
        velocity.x = Mathf.Clamp(velocity.x, -MaxSpeed, MaxSpeed);
        Rigidbody.velocity = velocity;
    }

    public void Jump(float power)
    {
        var velocity = Rigidbody.velocity;
        velocity.y += power * JumpSpeed;
        velocity.y = Mathf.Clamp(velocity.y, 0f, MaxJumpSpeed);
        Rigidbody.velocity = velocity;
    }
}
