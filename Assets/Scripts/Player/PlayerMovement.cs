using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float gravityWhileJumping;
    [SerializeField]
    private float jumpSpeed;

    private Vector3 moveDirection = Vector3.zero;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        moveDirection.x = InputManager.Instance.MovementDirection.x * moveSpeed;
        moveDirection.z = InputManager.Instance.MovementDirection.z * moveSpeed;

        if (InputManager.Instance.Jump && characterController.isGrounded) {
            Jump();
        }
        
        ApplyGravity();

        ApplyMove();
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

}
