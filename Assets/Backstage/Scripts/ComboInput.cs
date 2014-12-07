using UnityEngine;
using UnityEngine.Events;

public class ComboInput : MonoBehaviour {

    public Transform root;
    public Transform current; // make private
    public float expirity = 0f;
    public UnityEvent onBreak;

    private void Update() {
        if (expirity < Time.timeSinceLevelLoad || current == null) {
            if (current != root) onBreak.Invoke();
            current = root;
        }

        foreach (Transform child in current.transform) {
            var next = child.GetComponent<ComboNode>();

            if (!Input.GetButtonDown(next.button)) continue;

            next.Execute();
            next.action.Invoke();
            current = child;
            expirity = Time.timeSinceLevelLoad + next.timeout;
            break;
        }
    }
}
