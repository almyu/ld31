using UnityEngine;

public class Tactic : MonoBehaviour {

    [System.Serializable]
    public struct Transition {
        public Tactic next;
        public int weight;
    }

    public Transition[] transitions;
    public float rethinkInterval = 1f;

    private int weightSum;


    private void Awake() {
        foreach (var trans in transitions)
            weightSum += trans.weight;
    }

    private void OnEnable() {
        Invoke("Rethink", rethinkInterval);
    }

    private void OnDisable() {
        CancelInvoke("Rethink");
    }

    private Tactic RollNext() {
        var roll = Random.Range(0, weightSum);

        foreach (var trans in transitions) {
            if (roll < trans.weight) return trans.next;
            roll -= trans.weight;
        }

        return this;
    }

    public void SwitchTo(Tactic other) {
        enabled = false;
        other.enabled = true;
    }

    public void Rethink() {
        var next = RollNext();
        if (next != this)
            SwitchTo(next);
        else
            Invoke("Rethink", rethinkInterval);
    }
}
