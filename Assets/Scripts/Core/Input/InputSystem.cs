using System;
using UnityEngine.InputSystem;
using UnityEngine;

public interface IMovingActions
{
    event Action<float> OnMove;
    event Action<float> OnJump;
}

public class InputSystem : IUpdatable, IMovingActions
{
    PlayerInput PlayerInput;

    public event Action<float> OnMove = delegate { };
    public event Action<float> OnJump = delegate { };

    public InputSystem()
    {
        PlayerInput = new PlayerInput();
        PlayerInput.Enable();

        PlayerInput.Move.Jump.started += DoJump;
        //PlayerInput.Move.Jump.canceled += DoJump;
    }

    ~InputSystem()
    {
        PlayerInput.Move.Jump.started -= DoJump;
        //PlayerInput.Move.Jump.canceled -= DoJump;
    }

    void DoJump(InputAction.CallbackContext context)
    {
        OnJump.Invoke(1f);
        //OnJump.Invoke(context.ReadValue<float>());
    }

    void IUpdatable.Update()
    {
        var left = PlayerInput.Move.MoveLeft.ReadValue<float>();
        var right = PlayerInput.Move.MoveRight.ReadValue<float>();

        OnMove.Invoke(right - left);
    }
}