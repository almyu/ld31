using UnityEngine;

public class DamagingObject : MonoBehaviour {

    public int damage = 500;
    public float force = 10;

    private void OnTriggerStay2D(Collider2D coll) {
        var mortal = coll.GetComponent<Mortal>();
        if (mortal)
            mortal.Hit(damage, transform.position);

        var motor = coll.GetComponent<Motor>();
        if (motor) {
            motor.velocity = (motor.transform.position - transform.position).normalized * force;
        }
    }
}
