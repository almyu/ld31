using UnityEngine;

public class Coward : Tactic {

    public Vector2 safeDistanceRange = new Vector2(3f, 5f);

    private float safeDistance;

    protected new void OnEnable() {
        base.OnEnable();
        safeDistance = Random.Range(safeDistanceRange[0], safeDistanceRange[1]);
    }

    protected new void Update() {
        base.Update();
        if (IsStunned()) return;

        Follow(safeDistance);
    }
}
