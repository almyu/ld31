using UnityEngine;

public class Coward : Tactic {

    public float safeDistance = 3f;

    public void Update() {
        Follow(safeDistance);
    }
}
