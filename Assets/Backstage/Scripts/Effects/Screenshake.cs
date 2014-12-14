using UnityEngine;

public class Screenshake : MonoBehaviour {

    public bool shakeOnEnable = false;

    public void Shake() {
        MrScreenshaker.instance.Activate(1f);
    }

    public void ShakeWithPower(float factor) {
        MrScreenshaker.instance.Activate(factor);
    }

    private void OnEnable() {
        if (shakeOnEnable) Shake();
    }
}
