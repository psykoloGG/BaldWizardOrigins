using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR;

/**
 * PlayerControls
 *
 * Class responsible for all player controls:
 * - Movement
 * - Hand rotation and shooting
 */
public class PlayerControls : MonoBehaviour
{
    private IPlayerState CurrentState = new PlayerStateIdle();
    
    public SpriteRenderer PlayerSpriteRenderer;
    public Rigidbody2D PlayerRigidbody;
    
    [SerializeField] private SpriteRenderer HandSpriteRenderer;
    [SerializeField] private GameObject HandRotator;
    [SerializeField] private MainWeaponSlot MainWeaponSlot;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] public Animator Animator;
    public bool OnGround;
    private Vector2 PointerPosition;
    private bool Firing;

    
    [HideInInspector]
    public float HorizontalMovement;
    public float MoveSpeed = 3.0f;
    
    public float JumpingPower = 8.0f;
    public float CoyoteTime = 0.2f;
    public float CoyoteTimeCounter = 0.0f;
    public bool IsJumping = false;
    
    private void Update()
    {
        UpdateHandRotation();
        
        if (Firing)
        {
            MainWeaponSlot.Fire();
        }
    }
    
    private void FixedUpdate()
    {
        UpdateState();
        UpdateMovement();
    }

    private void UpdateState()
    {
        IPlayerState NewState = CurrentState.Tick(this);
        if (NewState != CurrentState)
        {
            CurrentState.Exit(this);
            CurrentState = NewState;
            CurrentState.Enter(this);
        }
    }

    private void UpdateMovement()
    {
        if (PlayerRigidbody.velocity.y < 0)
        {
            PlayerRigidbody.gravityScale = 2.5f;
        }
        else
        {
            PlayerRigidbody.gravityScale = 2.0f;
        }
        
        if (IsOnGround())
        {
            CoyoteTimeCounter = CoyoteTime;
        }
        else
        {
            CoyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void UpdateHandRotation()
    {
        Vector3 WorldPointerPosition = MainCamera.ScreenToWorldPoint(PointerPosition);
        Vector3 LookDirection = WorldPointerPosition - HandRotator.transform.position;
        float Angle = Mathf.Atan2(LookDirection.y, LookDirection.x) * Mathf.Rad2Deg;
        HandRotator.transform.rotation = Quaternion.Euler(0, 0, Angle);

        if (Angle > 90 || Angle < -90)
        {
            HandSpriteRenderer.flipY = true;
        }
        else
        {
            HandSpriteRenderer.flipY = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D Other)
    {
        if (Other.gameObject.GetComponent<WeaponBehavior>())
        {
            WeaponBehavior WeaponObject = Other.gameObject.GetComponent<WeaponBehavior>();
            Weapon Weapon = WeaponObject.GetWeapon();
            if (!Weapon)
            {
                Debug.LogError("Weapon object does not have a weapon!");
                return;
            }
            MainWeaponSlot.Equip(Weapon);
            Destroy(Other.gameObject);
        }
    }

    public void Fire(InputAction.CallbackContext Context)
    {
        if (Context.performed)
        {
            Firing = true;
        }

        if (Context.canceled)
        {
            Firing = false;
        }
    }
    
    public void Look(InputAction.CallbackContext Context)
    {
        PointerPosition = Context.ReadValue<Vector2>();
    }
    
    public void Move(InputAction.CallbackContext Context)
    {
        HorizontalMovement = Context.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext Context)
    {
        if (Context.performed && CoyoteTimeCounter > 0)
        {
            IsJumping = true;
            PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, JumpingPower);
        }
        
        if (Context.canceled && PlayerRigidbody.velocity.y > 0)
        {
            IsJumping = false;
            PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, PlayerRigidbody.velocity.y * 0.5f);
            CoyoteTimeCounter = 0;
        }
    }

    private bool IsOnGround()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.325f, GroundLayer);
    }
}