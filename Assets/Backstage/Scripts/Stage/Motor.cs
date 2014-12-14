using UnityEngine;

public class Motor : MonoBehaviour {

    public static Vector2 wind;
    public static float gravity = -18.6f;
    public static int freezeCounter = 0;

    public float windReach = 7.89f;
    public float ground = -2.3f, groundError = 1e-2f;

    [HideInInspector]
    public Vector2 velocity;

    [HideInInspector]
    public float lastFloorLevel;

    public bool isGrounded {
        get { return cachedTransform.position.y <= lastFloorLevel + groundError; }
    }

    private Transform cachedTransform;

    private void Awake() {
        cachedTransform = transform;
    }

    private void Update() {
        if (freezeCounter > 0) return;

        var oldVelocity = velocity;
        velocity.y += gravity * Time.deltaTime;

        var meanVelocity = (oldVelocity + velocity) * 0.5f;

        if (windReach <= 0f || Mathf.Abs(cachedTransform.position.x) < windReach)
            meanVelocity += wind;

        lastFloorLevel = ground;
        PlatformManager.instance.GetPlatformFloor(cachedTransform.position, ref lastFloorLevel);

        cachedTransform.position += (Vector3)(meanVelocity * Time.deltaTime);

        if (cachedTransform.position.y <= lastFloorLevel) {
            cachedTransform.position = cachedTransform.position.WithY(lastFloorLevel);
            velocity.y = 0f;
        }
    }

    public void ResetVelocity() {
        velocity = Vector2.zero;
    }
}
