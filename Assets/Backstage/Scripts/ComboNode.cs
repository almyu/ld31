using UnityEngine;
using UnityEngine.Events;

public class ComboNode : MonoBehaviour, ISerializationCallbackReceiver {

    public string button = "Fire1";
    public float timeout = 0.5f;

    public float editableAttackDirection = 0f, editableAttackRadius = 1f;
    [HideInInspector]
    public Vector2 attackDirection = Vector2.right;
    public float attackAngle = 60f;
    public int damage = 0;
    public GameObject particles;

    public UnityEvent action;

    private static readonly int layerMask = 1 << 9;
    private static Collider2D[] targetCache = new Collider2D[32];

    public void OnBeforeSerialize() {}

    public void OnAfterDeserialize() {
        attackDirection = Quaternion.AngleAxis(editableAttackDirection, Vector3.forward) * Vector3.right * editableAttackRadius;
    }

    public void Execute() {
        var pos = transform.position;

        var numHits = Physics2D.OverlapCircleNonAlloc((Vector2) pos, attackDirection.magnitude, targetCache, layerMask);
        if (numHits == 0) return;

        var dir = transform.rotation * attackDirection;

        for (int i = 0; i < numHits; ++i) {
            var toTarget = targetCache[i].bounds.center - pos;

            if (Vector3.Angle(dir, toTarget) > attackAngle) continue;

            targetCache[i].GetComponent<Mob>().health -= damage;

            if (particles)
                Instantiate(particles, targetCache[i].bounds.center,
                    Quaternion.RotateTowards(Quaternion.LookRotation(toTarget), Random.rotation, 15f));
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        if (UnityEditor.Selection.activeTransform != transform) return;

        var pos = transform.position;

        UnityEditor.Handles.color = Color.red.WithA(0.5f);
        UnityEditor.Handles.DrawSolidArc(pos, Vector3.forward, attackDirection, attackAngle, attackDirection.magnitude);
        UnityEditor.Handles.DrawSolidArc(pos, Vector3.forward, attackDirection, -attackAngle, attackDirection.magnitude);
    }
#endif
}
