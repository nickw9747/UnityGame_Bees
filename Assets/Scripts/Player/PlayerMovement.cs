using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Health playerHealth;

    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float gravity = 30.0f;
    [SerializeField]
    private float gravityWhileJumping = 20.0f;
    [SerializeField]
    private float jumpSpeed = 10.0f;
    [SerializeField]
    private float airSpeed = 0.5f;

    private Vector3 moveDirection = Vector3.zero;

    private bool allowMovement = true;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        playerHealth = GetComponent<Health>();
        allowMovement = true;
    }

    private void Update() {
        if (allowMovement) {
            Move();
        }
    }

    private void DisableMovement() {
        allowMovement = false;
    }

    private void Move() {
        if (characterController.isGrounded) {
            GroundedMovement();

            if (InputManager.Instance.Jump) {
                Jump();
            }
        }
        else {
            AirMovement();
        }

        ApplyGravity();

        ApplyMove();
    }

    private void AirMovement() {
        Vector3 horizontalVelocity = new Vector3(moveDirection.x, 0, moveDirection.z);
        horizontalVelocity.x += InputManager.Instance.MovementDirection.x * airSpeed;
        horizontalVelocity.z += InputManager.Instance.MovementDirection.z * airSpeed;
        horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, moveSpeed);
        moveDirection.x = horizontalVelocity.x;
        moveDirection.z = horizontalVelocity.z;
    }

    private void GroundedMovement() {
        moveDirection.x = InputManager.Instance.MovementDirection.x * moveSpeed;
        moveDirection.z = InputManager.Instance.MovementDirection.z * moveSpeed;
    }

    private void ApplyGravity() {
        if (InputManager.Instance.Jump && moveDirection.y >= 0.0f) {
            moveDirection.y -= gravityWhileJumping * Time.deltaTime;
        }
        else {
            moveDirection.y -= gravity * Time.deltaTime;
        }
    }

    private void ApplyMove() {
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void Jump() {
        moveDirection.y = jumpSpeed;
    }

    private void OnEnable() {
        playerHealth.OnDeath += DisableMovement;
    }

    private void OnDisable() {
        playerHealth.OnDeath -= DisableMovement;
    }

}

public class PlayerMovementInner {
    public void GroundedMovement(ref Vector3 currentMoveDirection, Vector3 newDirection, float moveSpeed) {
        currentMoveDirection.x = newDirection.x * moveSpeed;
        currentMoveDirection.z = newDirection.z * moveSpeed;
    }

    public void AirMovement(ref Vector3 currentMoveDirection, Vector3 newDirection, float airSpeed, float moveSpeed) {
        Vector3 horizontalVelocity = new Vector3(currentMoveDirection.x, 0, currentMoveDirection.z);
        horizontalVelocity.x += InputManager.Instance.MovementDirection.x * airSpeed;
        horizontalVelocity.z += InputManager.Instance.MovementDirection.z * airSpeed;
        horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, moveSpeed);
        currentMoveDirection.x = horizontalVelocity.x;
        currentMoveDirection.z = horizontalVelocity.z;
    }
}
