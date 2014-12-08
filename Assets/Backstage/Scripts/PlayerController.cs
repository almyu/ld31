using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Motor))]
public class PlayerController : MonoSingleton<PlayerController> {

    public float speed = 5f;
    public float jumpForce = 10f;

    private Motor cachedMotor;
    private Looks cachedLooks;

    private void Awake() {
        cachedMotor = GetComponent<Motor>();
        cachedLooks = GetComponentInChildren<Looks>();
    }

    private void Update() {
        cachedMotor.velocity = cachedMotor.velocity.WithX(Input.GetAxis("Horizontal") * speed);
        cachedLooks.SetApparentVelocity(cachedMotor.velocity);

        if (Input.GetButtonDown("Jump")) Jump();
    }

    public void Jump() {
        if (!cachedMotor.isGrounded) return;

        cachedMotor.velocity.y = jumpForce;
        cachedLooks.SetApparentVelocity(cachedMotor.velocity);
    }
}
