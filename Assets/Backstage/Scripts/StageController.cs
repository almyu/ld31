using UnityEngine;
using UnityEngine.Events;

public class StageController : MonoBehaviour {

    public Transform player;
    public BackgroundScroll background;
    public Transporter transporter;

    public float scrollThreshold = 2f, factor = 2f, exponent = 2f;

    private void Update() {
        var delta = player.position - transform.position;

        var power = Mathf.Max(0f, Mathf.Abs(delta.x) - scrollThreshold) * Mathf.Sign(-delta.x);
        power *= Mathf.Pow(Mathf.Abs(power), Mathf.Max(0f, exponent - 1f)) * factor;

        background.speed = power;
        transporter.speed = power;
    }
}
