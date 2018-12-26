using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[CreateAssetMenu(menuName = "AI/Action/Chase")]
public class ChaseAction : StateAction {

    [SerializeField]
    private float speed = 2.5f;

    public override void OnStateEnter(StateController controller) {
        controller.Agent.enabled = true;
    }

    public override void OnStateExit(StateController controller) {
        controller.Agent.enabled = false;
    }

    public override void Act(StateController controller) {
        ChasePlayer(controller);
    }

    private void ChasePlayer(StateController controller) {
        if (!controller.Agent.pathPending) {
            controller.Agent.SetDestination(controller.Target.position);
        }
    }
}
