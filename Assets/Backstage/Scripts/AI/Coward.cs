using UnityEngine;

public class Coward : Tactic {

    public void Update() {
        GetComponent<Motor>().velocity.x = 0f;
        GetComponent<Looks>().SetApparentVelocity(0f);
    }
}
