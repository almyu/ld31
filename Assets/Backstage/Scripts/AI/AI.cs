using UnityEngine;

public class AI : MonoBehaviour {

    public Tactic initialTactic;

    private void Start() {
        foreach (var tactic in GetComponentsInChildren<Tactic>())
            tactic.enabled = tactic == initialTactic;
    }
}
