using UnityEngine;

public class Tactic : MonoBehaviour {

    [System.Serializable]
    public struct Transition {
        public Tactic next;
        public int weight;
    }

    public float speed = 5f;
    public float jumpForce = 8.5f;

    public Transition[] transitions;
    public float rethinkInterval = 1f;

    private int weightSum;
    private Motor cachedMotor;


    protected void Awake() {
        foreach (var trans in transitions)
            weightSum += trans.weight;

        cachedMotor = GetComponent<Motor>();
    }

    protected void OnEnable() {
        Invoke("Rethink", rethinkInterval);
    }

    protected void OnDisable() {
        CancelInvoke("Rethink");
    }

    protected Tactic RollNext() {
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

    public Vector3 GetPlayerPosition() {
        return PlayerController.instance.transform.position;
    }

    public float GetDistanceToPlayer(int dimensions = 1) {
        var playerPos = GetPlayerPosition();
        var pos = transform.position;

        switch (dimensions) {
        case 1:  return Mathf.Abs(playerPos.x - pos.x);
        case 2:  return Vector2.Distance((Vector2) playerPos, (Vector2) pos);
        default: return Vector3.Distance(playerPos, pos);
        }
    }

    public float GetVelocityToFollow(float distance) {
        var toPlayer = (Vector2) PlayerController.instance.transform.position - (Vector2) transform.position;
        var toPoint = toPlayer.x - Mathf.Sign(toPlayer.x) * distance;

        var dir = Mathf.Sign(toPoint) * Mathf.Min(Mathf.Abs(toPoint), 1f);

        return dir * speed;
    }

    public void SetMovementVelocity(float vel) {
        if (cachedMotor) cachedMotor.velocity.x = vel;
    }

    public void Jump() {
        if (cachedMotor) cachedMotor.velocity.y = jumpForce;
    }

    public void Follow(float distance) {
        SetMovementVelocity(GetVelocityToFollow(distance));
    }
}
