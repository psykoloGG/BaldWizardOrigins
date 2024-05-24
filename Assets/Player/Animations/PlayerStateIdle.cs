using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : IPlayerState
{
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    public IPlayerState Tick(PlayerControls PlayerControls)
    {
        if (PlayerControls.HorizontalMovement != 0)
        {
            return new PlayerStateRunning();
        }

        return this;
    }

    public void Enter(PlayerControls PlayerControls)
    {
        PlayerControls.Animator.SetBool(IsRunning, false);
    }

    public void Exit(PlayerControls PlayerControls)
    {

    }
}
