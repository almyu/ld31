using UnityEngine;
using UnityEngine.Events;

public class StageController : MonoBehaviour {

    public Transform player;
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

    public float scrollThreshold = 2f, factor = 2f, exponent = 2f;

    private void LateUpdate() {
        var delta = player.position - transform.position;

        var power = Mathf.Max(0f, Mathf.Abs(delta.x) - scrollThreshold) * Mathf.Sign(-delta.x);
        power *= Mathf.Pow(Mathf.Abs(power), Mathf.Max(0f, exponent - 1f)) * factor;

        background.speed = power;
        transporter.speed = power;

        foreach (var anim in animators)
            anim.speed = power;

        Motor.wind.x = power;
    }

    private void Update() {
        foreach (var spinner in spinners)
            //spinner.xf.localRotation *= Quaternion.AngleAxis(spinner.speed * Motor.wind.x * Time.deltaTime, spinner.axis);
            spinner.xf.Rotate(Vector3.forward, spinner.speed * Motor.wind.x * Time.deltaTime);
    }
}
