using UnityEngine;

public class Tactic : MonoBehaviour {

    public bool isDangerous;
    public Tactic otherTactic;
    public float rethinkInterval = 1f;

    public float speed = 5f;
    public float jumpForce = 8.5f;

    private Motor cachedMotor;

    private float rethinkTimer, stunTimer;


    protected void Awake() {
        cachedMotor = GetComponent<Motor>();
    }

    protected void OnEnable() {}

    protected void OnDisable() {}

    protected void Update() {
        if (stunTimer > 0f) {
            stunTimer -= Time.deltaTime;
            return;
        }

        if (rethinkTimer > 0f)
            rethinkTimer -= Time.deltaTime;
        else
            Rethink();
    }

    public bool IsStunned() {
        return stunTimer > 0f;
    }

    public void Stun(float time) {
        stunTimer = time;
    }

    public void SwitchTactic() {
        enabled = false;
        otherTactic.enabled = true;
    }

    public void Rethink() {
        var scale = isDangerous ? MobSpawn.dangerometer : MobSpawn.peaceometer;

        if (Random.value < scale)
            SwitchTactic();

        rethinkTimer = rethinkInterval;
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
