using UnityEngine;
using UnityEngine.Events;

public class Mortal : MonoBehaviour {

    public Collider2D refCollider;

    public int health = 10000;
    public float hitParticlesMaxAngle = 15f;
    public GameObject hitParticles, deathParticles;
    public UnityEvent onHit, onDeath;

    private void Awake() {
        if (!refCollider) refCollider = GetComponentInChildren<Collider2D>();
    }

    public void Hit(int amount, Vector3 source) {
        onHit.Invoke();

        health -= amount;

        var center = refCollider.bounds.center;
        Instantiate(hitParticles, center, Quaternion.RotateTowards(Quaternion.LookRotation(center - source), Random.rotation, hitParticlesMaxAngle));

        if (health <= 0) Kill();
    }

    public void Kill() {
        onDeath.Invoke();

        Destroy(gameObject);

        if (deathParticles)
            Instantiate(deathParticles, refCollider.bounds.center, Quaternion.identity);
    }
}
