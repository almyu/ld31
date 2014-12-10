using UnityEngine;
using UnityEngine.Events;

public class Mortal : MonoBehaviour {

    public Collider2D refCollider;

    public int health = 10000;
    public float hitParticlesMaxAngle = 15f;
    public GameObject hitParticles, deathParticles;
    public ParticleSystem bleedingParticles;
    public UnityEvent onHit, onDeath;

    private int initialHealth;
    private float maxBleedingRate;

    private void Awake() {
        if (!refCollider)
            refCollider = GetComponentInChildren<Collider2D>();

        if (bleedingParticles) {
            maxBleedingRate = bleedingParticles.emissionRate;
            bleedingParticles.emissionRate = 0f;
        }
    }

    private void Start() {
        initialHealth = health;
    }

    public void Hit(int amount, Vector3 source) {
        onHit.Invoke();

        health -= amount;
        UpdateBleeding();

        var center = refCollider.bounds.center;
        Instantiate(hitParticles, center, Quaternion.RotateTowards(Quaternion.LookRotation(center - source), Random.rotation, hitParticlesMaxAngle));

        if (health <= 0) Kill();
    }

    public void Kill() {
        if (deathParticles)
            Instantiate(deathParticles, refCollider.bounds.center, Quaternion.identity);

        onDeath.Invoke();
    }

    public void Revive() {
        health = initialHealth;
        UpdateBleeding();
    }

    public void UpdateBleeding() {
        if (bleedingParticles)
            bleedingParticles.emissionRate = maxBleedingRate * (initialHealth - health) / initialHealth;
    }
}
