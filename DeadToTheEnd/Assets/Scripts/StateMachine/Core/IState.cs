using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter();
    public void Exit();
    public void Update();
    public void FixedUpdate();
    public void TriggerOnStateAnimationEnterEvent();
    public void TriggerOnStateAnimationExitEvent();
    public void TriggerOnStateAnimationHandleEvent();

    public void HandleInput();
}
