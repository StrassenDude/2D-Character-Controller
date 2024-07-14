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


    public event Action<Vector2> MoveEvent;

    public event Action FireEvent;
    public event Action FireCancelEvent;


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

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
}
