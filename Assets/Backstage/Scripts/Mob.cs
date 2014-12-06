using UnityEngine;

public class Mob : MonoBehaviour {

    public int health {
        get { return _health; }
        set { Debug.Log(value + "!!1"); _health = value; if (_health <= 0) Destroy(gameObject); }
    }

    private int _health = 10000;
}
