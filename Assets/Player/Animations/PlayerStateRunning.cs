using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRunning : IPlayerState
{
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    public IPlayerState Tick(PlayerControls PlayerControls)
    {
        MoveCharacter(PlayerControls);
        if (PlayerControls.HorizontalMovement == 0)
        {
            return new PlayerStateIdle();
        }


        return this;
    }

    public void Enter(PlayerControls PlayerControls)
    {
        PlayerControls.Animator.SetBool(IsRunning, true);

    }

    public void Exit(PlayerControls PlayerControls)
    {
    }

    private void MoveCharacter(PlayerControls PlayerControls)
    {
        PlayerControls.PlayerRigidbody.velocity = new Vector2(
            PlayerControls.HorizontalMovement * PlayerControls.MoveSpeed, PlayerControls.PlayerRigidbody.velocity.y);

        // Adjust sprite to face direction player is moving
        if (PlayerControls.HorizontalMovement > 0)
        {
            PlayerControls.PlayerSpriteRenderer.flipX = false;
        }
        else if (PlayerControls.HorizontalMovement < 0)
        {
            PlayerControls.PlayerSpriteRenderer.flipX = true;
        }
    }
}