using UnityEngine;

public class Sectorcaster : MonoBehaviour {

    public LayerMask layers;

    private static Collider2D[] hitCache = new Collider2D[32];

    public void Cast(Vector2 dir, float angle, System.Action<Collider2D> action) {
        var pos = (Vector2) transform.position;

        var numHits = Physics2D.OverlapCircleNonAlloc(pos, dir.magnitude, hitCache, layers.value);
        if (numHits == 0) return;

        dir = transform.rotation * dir;

        for (int i = 0; i < numHits; ++i) {
            var toTarget = (Vector2) hitCache[i].bounds.center - pos;

            if (Vector2.Angle(dir, toTarget) <= angle)
                action(hitCache[i]);
        }
    }
}
