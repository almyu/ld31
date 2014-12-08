using UnityEngine;

[RequireComponent(typeof(Motor))]
public class Skylimited : MonoBehaviour {

    private Transform cachedTransform;
    private Motor cachedMotor;

    private void Awake() {
        cachedTransform = transform;
        cachedMotor = GetComponent<Motor>();
    }

    private void Update() {
        var guard = Skyguard.instance;

        if (cachedMotor.velocity.y > 0f && cachedTransform.position.y >= guard.transform.position.y)
            guard.Activate(cachedMotor);
    }
}
