using UnityEngine;

public class Tactic : MonoBehaviour {

    [System.Serializable]
    public struct Transition {
        public Tactic next;
        public int weight;
    }

    public float speed = 5f;

    public Transition[] transitions;
    public float rethinkInterval = 1f;

    private int weightSum;
    private Motor cachedMotor;
    private Looks cachedLooks;


    private void Awake() {
        foreach (var trans in transitions)
            weightSum += trans.weight;

        cachedMotor = GetComponent<Motor>();
        cachedLooks = GetComponentInChildren<Looks>();
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

    public Vector2 GetVelocityToFollow(float distance) {
        var toPlayer = (Vector2) PlayerController.instance.transform.position - (Vector2) transform.position;
        var toPoint = toPlayer.x - Mathf.Sign(toPlayer.x) * distance;

        var dir = Mathf.Sign(toPoint) * Mathf.Min(Mathf.Abs(toPoint), 1f);

        return Vector2.right * dir * speed;
    }

    public void SetVelocity(Vector2 vel) {
        if (cachedMotor) cachedMotor.velocity = vel;
        if (cachedLooks) cachedLooks.SetApparentVelocity(vel.x);
    }

    public void Follow(float distance) {
        SetVelocity(GetVelocityToFollow(distance));
    }
}
