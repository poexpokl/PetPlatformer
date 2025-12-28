using System;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private IPlayerState currentState;
    public event Action StateChanged;

    private void Update()
    {
        currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        currentState.StateFixedUpdate();
    }
    public void ChangeState(IPlayerState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
        StateChanged?.Invoke(); //? и мб нужны переменные
    }
}
