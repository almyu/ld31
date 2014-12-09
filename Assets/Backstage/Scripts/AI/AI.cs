using UnityEngine;

public class AI : MonoBehaviour {

    public Tactic initialTactic;

    private void Start() {
        foreach (var tactic in GetComponentsInChildren<Tactic>())
            tactic.enabled = tactic == initialTactic;
    }

    public void Stun(float time) {
        foreach (var tactic in GetComponentsInChildren<Tactic>()) {
            if (!tactic.enabled) continue;

            tactic.Stun(time);
            return;
        }
    }

    public void StunByDamage(int amount) {
        Stun(Balance.instance.stunSecondsPer1kDamage * amount / 1000f);
    }
}
