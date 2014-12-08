using UnityEngine;
using UnityEngine.Events;

public class StageController : MonoBehaviour {

    public BackgroundScroll background;
    public Transporter transporter;

    public Animator[] animators;
    public float animatorsSpeed = 1f;

    [System.Serializable]
    public class Spinner {
        public Transform xf;
        public float speed = 45f;
        public Vector3 axis = Vector3.forward;
    }

    public Spinner[] spinners;

    [System.Serializable]
    public class Trigger {
        public float windThreshold;
        public UnityEvent on, off;

        [HideInInspector]
        public bool isOn;
    }

    public Trigger[] triggers;

    public float scrollThreshold = 2f, factor = 2f, exponent = 2f;

    private void LateUpdate() {
        var delta = PlayerController.instance.transform.position - transform.position;

        var power = Mathf.Max(0f, Mathf.Abs(delta.x) - scrollThreshold) * Mathf.Sign(-delta.x);
        power *= Mathf.Pow(Mathf.Abs(power), Mathf.Max(0f, exponent - 1f)) * factor;

        background.speed = power;
        transporter.speed = power;

        foreach (var anim in animators)
            anim.speed = power;

        foreach (var trigger in triggers) {
            var state = trigger.windThreshold < 0f
                ? power <= trigger.windThreshold
                : power >= trigger.windThreshold;

            if (state == trigger.isOn) continue;

            (state ? trigger.on : trigger.off).Invoke();
            trigger.isOn = state;
        }

        Motor.wind.x = power;
    }

    private void Update() {
        foreach (var spinner in spinners)
            spinner.xf.Rotate(Vector3.forward, spinner.speed * Motor.wind.x * Time.deltaTime);
    }
}
