using UnityEngine;
using UnityEngine.Events;

public class StageController : MonoBehaviour {

    public Transform player;

    public float scrollThreshold = 2f, windThreshold = 1f;

    public UnityEvent scrollLeftOn, scrollRightOn, scrollOff, windLeftOn, windRightOn, windOff;

    private int scroll = 0, wind = 0;

    private void Update() {
        var delta = player.position - transform.position;

        var newScroll = delta.x < -scrollThreshold ? -1 : delta.x > scrollThreshold ? 1 : 0;
        var newWind = delta.y > windThreshold ? scroll : 0;

        scroll = newScroll;
        wind = newWind;
    }
}
