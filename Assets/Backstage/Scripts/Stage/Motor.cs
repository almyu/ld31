using UnityEngine;

public class Motor : MonoBehaviour {

    public static Vector2 wind;
    public static float gravity = -18.6f;

    public float windReach = 7.89f;
    public float ground = -2.3f, groundError = 1e-2f;

    [HideInInspector]
    public Vector2 velocity;

    public float dynamicGround {
        get {
            var value = ground;

            var platformMgr = PlatformManager.instance;
            if (platformMgr)
                platformMgr.GetPlatformFloor(cachedTransform.position, ref value);

            return value;
        }
    }

    public bool isGrounded {
        get { return cachedTransform.position.y <= dynamicGround + groundError; }
    }

    private Transform cachedTransform;

    private void Awake() {
        cachedTransform = transform;
    }

    private void Update() {
        var oldVelocity = velocity;
        velocity.y += gravity * Time.deltaTime;

        var meanVelocity = (oldVelocity + velocity) * 0.5f;

        if (windReach <= 0f || Mathf.Abs(cachedTransform.position.x) < windReach)
            meanVelocity += wind;

        var floor = dynamicGround;

        cachedTransform.position += (Vector3)(meanVelocity * Time.deltaTime);

        if (cachedTransform.position.y <= floor) {
            cachedTransform.position = cachedTransform.position.WithY(floor);
            velocity.y = 0f;
        }
    }

    public void ResetVelocity() {
        velocity = Vector2.zero;
    }
}
