using UnityEngine;
using UnityEngine.Events;

public class Spotlight : MonoBehaviour {

    public UnityEvent onTrigger;

    private void OnTriggerEnter2D(Collider2D coll) {
        onTrigger.Invoke();
    }
}
