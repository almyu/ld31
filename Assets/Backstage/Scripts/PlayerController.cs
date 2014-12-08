using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Motor))]
public class PlayerController : MonoSingleton<PlayerController> {

    public float speed = 5f;
    public float jumpForce = 10f;

    public float doubleJumpDetectHeight = 3f;
    public LayerMask doubleJumpMobs;

    private Motor cachedMotor;

    private void Awake() {
        cachedMotor = GetComponent<Motor>();
    }

    private void Update() {
        cachedMotor.velocity.x = Input.GetAxis("Horizontal") * speed;

        if (Input.GetButtonDown("Jump")) Jump();
    }

    public bool HasMobsAbove() {
        var pos = (Vector2) transform.position;
        return Physics2D.OverlapArea(pos - new Vector2(0.5f, 0f), pos + new Vector2(0.5f, doubleJumpDetectHeight), doubleJumpMobs.value) != null;
    }

    public void Jump() {
        if (cachedMotor.isGrounded || HasMobsAbove())
            cachedMotor.velocity.y = jumpForce;
    }
}
