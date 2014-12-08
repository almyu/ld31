using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Sectorcaster))]
public class ComboInput : MonoBehaviour {

    public Transform root;
    public Transform current; // make private
    public float expirity = 0f;
    public UnityEvent onBreak;

    private Sectorcaster cachedCaster;

    private void Awake() {
        cachedCaster = GetComponent<Sectorcaster>();
    }

    private void Update() {
        if (expirity < Time.timeSinceLevelLoad || current == null) {
            if (current != root) onBreak.Invoke();
            current = root;
        }

        var airborne = !PlayerController.instance.GetComponent<Motor>().isGrounded;
        var axes = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (transform.right.x < 0f)
            axes.x = -axes.x;

        foreach (Transform child in current.transform) {
            var next = child.GetComponent<ComboNode>();

            if (airborne != next.aerial) continue;
            if (!Input.GetButtonDown(next.button)) continue;

            if (next.modCond != ComboNode.ModCondition.DontCare) {
                if (next.mod == ComboNode.Mod.Forward && (axes.x > 0f) != (next.modCond == ComboNode.ModCondition.Yes)) continue;
                if (next.mod == ComboNode.Mod.Back && (axes.y < 0f) != (next.modCond == ComboNode.ModCondition.Yes)) continue;
                if (next.mod == ComboNode.Mod.Up && (axes.y > 0f) != (next.modCond == ComboNode.ModCondition.Yes)) continue;
                if (next.mod == ComboNode.Mod.Down && (axes.y < 0f) != (next.modCond == ComboNode.ModCondition.Yes)) continue;
            }

            next.Execute(cachedCaster);
            next.action.Invoke();
            current = child;
            expirity = Time.timeSinceLevelLoad + next.timeout;
            break;
        }
    }
}
