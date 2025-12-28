using UnityEngine;

public interface IPlayerState
{
    public void StateUpdate();
    public void StateFixedUpdate();
    public void EnterState();
    public void ExitState();

}
