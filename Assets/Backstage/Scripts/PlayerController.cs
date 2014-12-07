using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Motor))]
public class PlayerController : MonoSingleton<PlayerController> {

    public float speed = 5f;
    public float jumpForce = 10f;

    [System.Serializable]
    public class VelocityChangeEvent : UnityEvent<string, float> {}
    public VelocityChangeEvent onVelocityChange;

    private Motor cachedMotor;
    private bool wannaJump = false;

    private void Awake() {
        cachedMotor = GetComponent<Motor>();
    }

    private void Update() {
        wannaJump = wannaJump || Input.GetButtonDown("Jump");
    }

    private void FixedUpdate() {
        var axis = Input.GetAxis("Horizontal");
        var vel = Mathf.Approximately(axis, 0f) ? 0f : axis * speed; // axis < 0f ? -speed : speed;

        cachedMotor.velocity = cachedMotor.velocity.WithX(vel);
        onVelocityChange.Invoke("Velocity", cachedMotor.velocity.x);

        if (vel != 0f) {
            var facingRight = Quaternion.Angle(transform.rotation, Quaternion.identity) < 15f;
            if (facingRight != vel > 0f)
                transform.rotation = Quaternion.AngleAxis(facingRight ? 180f : 0f, Vector3.up);
        }

        if (wannaJump) {
            wannaJump = false;
            Jump();
        }
    }

    public void Jump() {
        if (cachedMotor.isGrounded)
            cachedMotor.velocity.y = jumpForce;
    }
}
