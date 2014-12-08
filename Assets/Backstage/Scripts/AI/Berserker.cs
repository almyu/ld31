using UnityEngine;

public class Berserker : Tactic {

    public void Update() {
        var vel = Mathf.Clamp(PlayerController.instance.transform.position.x - transform.position.x, -4f, 4f);
        GetComponent<Motor>().velocity.x = vel;
        GetComponent<Looks>().SetApparentVelocity(vel);
    }
}
