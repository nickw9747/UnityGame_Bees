using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InputManager : Singleton<InputManager> {

    private Vector3 moveDirection;

    public Vector3 MovementDirection { get; private set; }
    public bool Jump { get; private set; }
    public bool Sprint { get; private set; }

    public Action OnActionPress;

    private void Update() {
        MovementInput();

        Jump = CrossPlatformInputManager.GetButton("Jump");
        Sprint = CrossPlatformInputManager.GetButton("Sprint");
    }

    private void MovementInput() {
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        MovementDirection = new Vector3(horizontal, 0, vertical);
    }

    private void CheckAction() {
        if (CrossPlatformInputManager.GetButtonDown("Action") && OnActionPress != null) {
            OnActionPress();
        }
    }
}
