﻿using UnityEngine;
using UnityEngine.Events;

public class Berserker : Tactic {

    public Sectorcaster caster;

    public int damage;
    public float attackRange = 1f, attackCooldown = 1f, recoveryTime = 0.3f;
    public UnityEvent onAttack, onRecover;

    private float nextAttack, recoveryTimer;

    protected new void Update() {
        base.Update();
        if (IsStunned()) return;

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

    protected new void OnEnable() {
        base.OnEnable();
        ++MobSpawn.totalBerserks;
    }

    protected new void OnDisable() {
        base.OnDisable();
        onRecover.Invoke();

        --MobSpawn.totalBerserks;
    }

    public void Attack() {
        onAttack.Invoke();
        caster.Cast(Vector3.right * attackRange, 60f, coll => coll.GetComponent<Mortal>().Hit(damage, caster.transform.position));
    }
}
