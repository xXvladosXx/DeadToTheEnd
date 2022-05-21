using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter();
    public void Exit();
    public void Update();
    public void FixedUpdate();
    public void OnAnimationEnterEvent();
    public void OnAnimationExitEvent();
    public void OnAnimationHandleEvent();

}
