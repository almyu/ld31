using UnityEngine;

public class AI : MonoBehaviour {

    public Tactic initialTactic;

    private void Start() {
        foreach (var tactic in GetComponentsInChildren<Tactic>())
            tactic.enabled = tactic == initialTactic;
    }

    public void StunAll(float time) {
        foreach (var tactic in GetComponentsInChildren<Tactic>()) {
            if (!tactic.enabled) continue;

            tactic.Stun(time * Balance.instance.stunDurationFactor);
            return;
        }
    }
}
