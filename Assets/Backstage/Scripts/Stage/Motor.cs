using UnityEngine;

public class Motor : MonoBehaviour {

    public static Vector2 wind;
    public static float gravity = -18.6f;

    public float ground = -2.3f, groundError = 1e-2f;

    [HideInInspector]
    public Vector2 velocity;

    public bool isGrounded {
        get { return cachedTransform.position.y <= ground + groundError; }
    }

    private Transform cachedTransform;

    private void Awake() {
        cachedTransform = transform;
    }

    private void FixedUpdate() {
        var oldVelocity = velocity;
        velocity.y += gravity * Time.fixedDeltaTime;

        var meanVelocity = (oldVelocity + velocity) * 0.5f + wind;

        cachedTransform.position += (Vector3)(meanVelocity * Time.fixedDeltaTime);

        if (cachedTransform.position.y <= ground) {
            cachedTransform.position = cachedTransform.position.WithY(ground);
            velocity.y = 0f;
        }
    }
}
