using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovement
{
    public enum Directions { Forward, Backward, Left, Right };

    public Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };

    [Test]
    public void TestGroundedMovement_MoveFromStationary(
        [Values(-1, 0, 1)] int moveAmount,
        [Values()] Directions direction
        ) {
        PlayerMovementInner pmi = new PlayerMovementInner();

        Vector3 currentMoveDirection = Vector3.zero;

        pmi.GroundedMovement(ref currentMoveDirection, directions[(int)direction], moveAmount);

        Assert.AreEqual(currentMoveDirection, directions[(int)direction] * moveAmount);
    }

    [Test]
    public void TestGroundedMovement_MoveWhileMoving(
        [Values(-1, 0, 1)] int moveAmount,
        [Values()] Directions direction,
        [Values()] Directions currentDirection
        ) {
        PlayerMovementInner pmi = new PlayerMovementInner();

        Vector3 currentMoveDirection = directions[(int)currentDirection];

        pmi.GroundedMovement(ref currentMoveDirection, directions[(int)direction], moveAmount);

        Assert.AreEqual(currentMoveDirection, directions[(int)direction] * moveAmount);
    }
}
