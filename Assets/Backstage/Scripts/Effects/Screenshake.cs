using UnityEngine;

public class Screenshake : MonoBehaviour {

    public bool shakeOnEnable = false;

    public void Shake() {
        MrScreenshaker.instance.Activate();
    }

    private void OnEnable() {
        if (shakeOnEnable) Shake();
    }
}
