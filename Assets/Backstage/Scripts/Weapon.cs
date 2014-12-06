using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class Weapon : MonoBehaviour {

    private HashSet<Mob> targets = new HashSet<Mob>();

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Mob") targets.Add(collider.GetComponent<Mob>());
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == "Mob") targets.Remove(collider.GetComponent<Mob>());
    }

    public void Swing(ComboNode combo) {
        foreach (var target in targets)
            if (combo.CheckReach(transform, target.collider2D))
                target.health -= 1000;
    }
}
