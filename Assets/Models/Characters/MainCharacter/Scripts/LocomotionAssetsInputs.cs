using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocomotionAssetsInputs : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;

    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool use;
    public bool dodge;

    [Header("Movement Settings")]
    public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;

    public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void OnUse(InputValue value)
    {
        UseInput(value.isPressed);
    }

    public void OnDodge(InputValue value)
    {
        DodgeInput(value.isPressed);
    }

#else
	// old input sys if we do decide to have it (most likely wont)...
#endif

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

    public void UseInput(bool newUseState)
    {
        use = newUseState;
    }

    public void DodgeInput(bool newUseState)
    {
        dodge = newUseState;
    }

#if !UNITY_IOS || !UNITY_ANDROID

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    public void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

#endif
}