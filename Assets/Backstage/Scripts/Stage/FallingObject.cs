using UnityEngine;
using UnityEngine.Events;

public class FallingObject : MonoBehaviour {

    public float ground = -2.3f;
    public UnityEvent onImpact;

    private void Update() {
        if (transform.position.y <= ground)
            onImpact.Invoke();
    }
}
