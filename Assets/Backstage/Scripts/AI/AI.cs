using UnityEngine;

public class AI : MonoBehaviour {

    public Tactic initialTactic;

    private void Awake() {
        foreach (var tactic in GetComponentsInChildren<Tactic>())
            if (tactic != initialTactic)
                tactic.enabled = false;
    }
}
