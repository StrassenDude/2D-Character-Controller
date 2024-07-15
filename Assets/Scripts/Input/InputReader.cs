using NoiceOneGames;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions
{

    GameInput _gameInput;

    private void OnEnable()
    {
        if(_gameInput == null)
        {
            _gameInput = new GameInput();

            _gameInput.Gameplay.SetCallbacks(this);

            SetGameplay();
        }
    }

    public void SetGameplay()
    {
        _gameInput.Gameplay.Enable();
    }


    public event Action<Vector2> DirectionEvent;

    public event Action FireEvent;
    public event Action FireCancelEvent;

    public event Action AccelerateEvent;
    public event Action AccelerateCancelEvent;


    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            FireEvent?.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            FireCancelEvent?.Invoke();
        }

    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            AccelerateEvent?.Invoke();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            AccelerateCancelEvent?.Invoke();
        }
    }

    public void OnDirection(InputAction.CallbackContext context)
    {
        DirectionEvent?.Invoke(context.ReadValue<Vector2>());
    }
}
