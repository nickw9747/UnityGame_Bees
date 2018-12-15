using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovement
{
    [Test]
    public void TestGroundedMovement_MoveLeftFromStationary(
        [Values(1, 2, 3)] int moveAmount
        ) {
        PlayerMovementInner pmi = new PlayerMovementInner();

        Vector3 currentMoveDirection = Vector3.zero;

        pmi.GroundedMovement(ref currentMoveDirection, Vector3.left, moveAmount);

        Assert.AreEqual(currentMoveDirection, Vector3.left * moveAmount);
    }


}
