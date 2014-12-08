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

        foreach (Transform child in current.transform) {
            var next = child.GetComponent<ComboNode>();

            if (!Input.GetButtonDown(next.button)) continue;

            next.Execute(cachedCaster);
            next.action.Invoke();
            current = child;
            expirity = Time.timeSinceLevelLoad + next.timeout;
            break;
        }
    }
}
