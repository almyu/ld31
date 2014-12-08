using UnityEngine;

public class Lifetime : MonoBehaviour {

    public float lifetime = 1f;

    private void Start() {
        Destroy(gameObject, lifetime);
    }
}
