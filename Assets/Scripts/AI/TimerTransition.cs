using UnityEngine;

[CreateAssetMenu(menuName = "AI/Transition/Timer")]
public class TimerTransition : Transition {

    [SerializeField]
    private float time = 5;
    private float elapsedTime = 0;

    public override bool CheckTransition(StateController controller) {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= time) {
            elapsedTime = 0;
            return true;
        }
        return false;
    }

    public override void TransitionGizmo(StateController controller) {
        float remainingTime = time - elapsedTime;

        remainingTime.ToString().DrawStringInScene(controller.transform.position, Color.white);
    }

    private void OnEnable() {
        elapsedTime = 0;
    }
}
