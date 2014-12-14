using UnityEngine;

public class GroundImpact : MonoBehaviour {

    public Rect area;
    public LayerMask targets;
    public Vector2 force = new Vector2(10f, 1f);
    public int damage = 500;
    public float stun = 0.1f;

    private Transform cachedTransform;
    private Vector2 lastPosition;
    private static Collider2D[] hitCache = new Collider2D[32];


    private void Awake() {
        cachedTransform = transform;
    }

    private void LateUpdate() {
        lastPosition = (Vector2) cachedTransform.position;
    }

    public void Activate() {
        var pos = lastPosition;

        var numHits = Physics2D.OverlapAreaNonAlloc(
            pos + area.min, pos + area.max, hitCache, targets.value);

        for (int i = 0; i < numHits; ++i) {
            var mortal = hitCache[i].GetComponent<Mortal>();
            if (mortal)
                mortal.Hit(damage, cachedTransform.position);

            var motor = hitCache[i].GetComponent<Motor>();
            if (motor) {
                var dir = ((Vector2) motor.transform.position - pos).normalized;
                motor.velocity = new Vector2(dir.x * force.x, force.y);
            }

            var ai = hitCache[i].GetComponent<AI>();
            if (ai)
                ai.StunAll(stun);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red.WithA(0.3f);
        Gizmos.DrawCube(transform.position + area.center.WithZ(0f), area.size);
    }
}
