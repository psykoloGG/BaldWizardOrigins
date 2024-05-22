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
    [SerializeField] private SpriteRenderer PlayerSpriteRenderer;
    [SerializeField] private SpriteRenderer HandSpriteRenderer;
    [SerializeField] private GameObject HandRotator;
    [SerializeField] private MainWeaponSlot MainWeaponSlot;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Rigidbody2D PlayerRigidbody;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;
    private bool OnGround;
    private Vector2 PointerPosition;
    private bool Firing;

    [SerializeField] private float MoveSpeed = 3.0f;
    [SerializeField] private float JumpingPower = 8.0f;
    private float HorizontalMovement;
    
    private float CoyoteTime = 0.2f;
    private float CoyoteTimeCounter = 0.0f;
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
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        PlayerRigidbody.velocity = new Vector2(HorizontalMovement * MoveSpeed, PlayerRigidbody.velocity.y);

        // Adjust sprite to face direction player is moving
        if (HorizontalMovement > 0)
        {
            PlayerSpriteRenderer.flipX = false;
        }
        else if (HorizontalMovement < 0)
        {
            PlayerSpriteRenderer.flipX = true;
        }
        
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
            PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, JumpingPower);
        }
        
        if (Context.canceled && PlayerRigidbody.velocity.y > 0)
        {
            PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, PlayerRigidbody.velocity.y * 0.5f);
            CoyoteTimeCounter = 0;
        }
    }

    private bool IsOnGround()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, GroundLayer);
    }
}