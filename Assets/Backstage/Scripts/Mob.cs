using UnityEngine;

public class Mob : MonoBehaviour {

    public GameObject deathParticles;

    public int health {
        get { return _health; }
        set {
            Debug.Log(value + "!!1"); _health = value;
            if (_health <= 0) {
                Destroy(gameObject);

                if (deathParticles)
                    Instantiate(deathParticles, collider2D.bounds.center, Quaternion.identity);
            }
        }
    }

    private int _health = 10000;
}
