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

    public void OnBeforeSerialize() {}

    public void OnAfterDeserialize() {
        attackDirection = Quaternion.AngleAxis(editableAttackDirection, Vector3.forward) * Vector3.right * editableAttackRadius;
    }

    public void Execute(Sectorcaster caster) {
        caster.Cast(attackDirection, attackAngle, coll => coll.GetComponent<Mortal>().Hit(damage, transform.position));
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
