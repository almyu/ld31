using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Motor))]
public class PlayerController : MonoSingleton<PlayerController> {

    public float speed = 5f;
    public float jumpForce = 10f;

    private Motor cachedMotor;

    private void Awake() {
        cachedMotor = GetComponent<Motor>();
    }

    private void Update() {
        cachedMotor.velocity.x = Input.GetAxis("Horizontal") * speed;

        if (Input.GetButtonDown("Jump")) Jump();
    }

    public void Jump() {
        if (cachedMotor.isGrounded)
            cachedMotor.velocity.y = jumpForce;
    }
}
