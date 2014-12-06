using UnityEngine;
using UnityEngine.Events;

public class ComboNode : MonoBehaviour {

    public string button = "Fire1";
    public float timeout = 0.5f;

    public Vector2 attackCenter = new Vector2(0f, 0.5f), attackAngles = new Vector2(30f, 180f);
    public float attackRadius = 1f;

    public UnityEvent action;

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        if (UnityEditor.Selection.activeTransform != transform) return;

        var pos = attackCenter.WithZ(0f);

        var pl = FindObjectOfType<PlayerController>();
        if (pl)
            pos += pl.transform.position;

        var angle = attackAngles[1] - attackAngles[0];
        var nrm = Quaternion.AngleAxis(attackAngles[0], Vector3.forward) * Vector3.right;

        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireArc(pos, Vector3.forward, nrm, angle, attackRadius);
    }
#endif
}
