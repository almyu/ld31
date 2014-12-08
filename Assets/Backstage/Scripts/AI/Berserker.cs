using UnityEngine;
using UnityEngine.Events;

public class Berserker : Tactic {

    public float attackRange = 1f, attackCooldown = 1f, recoveryTime = 0.3f;
    public UnityEvent onAttack, onAttackSuccess, onRecover;

    private float nextAttack, recoveryTimer;

    private void Update() {
        Follow(0.5f);

        var t = Time.timeSinceLevelLoad;

        if (recoveryTimer > 0f) {
            recoveryTimer -= Time.deltaTime;

            if (recoveryTimer <= 0f)
                onRecover.Invoke();
        }

        if (nextAttack <= t && GetDistanceToPlayer() <= attackRange) {
            nextAttack = t + attackCooldown;
            recoveryTimer = recoveryTime;
            Attack();
        }
    }

    protected new void OnDisable() {
        base.OnDisable();
        onRecover.Invoke();
    }

    public void Attack() {
        onAttack.Invoke();

        if (Vector2.Distance(transform.position + transform.right, GetPlayerPosition()) <= attackRange)
            onAttackSuccess.Invoke();
    }
}
