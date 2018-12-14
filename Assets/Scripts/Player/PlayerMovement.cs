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
    private float jumpSpeed;

    private Vector3 moveDirection;


    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        moveDirection = InputManager.Instance.MovementDirection;


        if (!characterController.isGrounded) {
            Fall();
        } else if (InputManager.Instance.Jump) {
            Jump();
        }

        ApplyMove();
    }

    private void Fall() {
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
    }

    private void ApplyMove() {
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Jump() {
        moveDirection.y = jumpSpeed * Time.deltaTime;
    }

}
