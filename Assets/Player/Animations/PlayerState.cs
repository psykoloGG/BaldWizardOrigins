using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    public IPlayerState Tick(PlayerControls PlayerControls);
    public void Enter(PlayerControls PlayerControls);
    public void Exit(PlayerControls PlayerControls);
}
