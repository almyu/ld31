using UnityEngine;

public class Shadow : MonoBehaviour {

    public Motor motor;

    private Transform cachedTransform;


    private void Awake() {
        if (!motor) motor = GetComponentInParent<Motor>();
        cachedTransform = transform;
    }

    private void LateUpdate() {
        cachedTransform.position = cachedTransform.position
            .WithX(motor.transform.position.x)
            .WithY(motor.lastFloorLevel);
    }
}
