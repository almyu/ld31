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

    public Vector2 missVelocityMul = Vector2.one, missVelocityAdd;
    public Vector2 selfVelocityMul, selfVelocityAdd = Vector2.right * 10f;
    public Vector2 targetVelocityMul, targetVelocityAdd = Vector2.right * 10f;

    public enum ModCondition {
        DontCare, Yes, No
    }
    public enum Mod {
        Up, Down, Forward, Back
    }

    public bool aerial;

    public ModCondition modCond;
    public Mod mod;

    public UnityEvent action;

    public void OnBeforeSerialize() {}

    public void OnAfterDeserialize() {
        attackDirection = Quaternion.AngleAxis(editableAttackDirection, Vector3.forward) * Vector3.right * editableAttackRadius;
    }

    public void ApplyVelocity(GameObject obj, Vector2 mul, Vector2 add) {
        var motor = obj.GetComponent<Motor>();
        if (motor)
            motor.velocity = Vector2.Scale(motor.velocity, mul) + new Vector2(add.x * transform.right.x, add.y);
    }

    public void Execute(Sectorcaster caster) {
        var player = PlayerController.instance.gameObject;

        var numHits = caster.Cast(attackDirection, attackAngle, coll => {
            coll.GetComponent<Mortal>().Hit(damage, transform.position);

            ApplyVelocity(player, selfVelocityMul, selfVelocityAdd);
            ApplyVelocity(coll.gameObject, targetVelocityMul, targetVelocityAdd);

            coll.SendMessage("HandleCombo", this, SendMessageOptions.DontRequireReceiver);
            coll.SendMessage("StunByDamage", damage, SendMessageOptions.DontRequireReceiver);
        });

        if (numHits == 0)
            ApplyVelocity(player, missVelocityMul, missVelocityAdd);
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
