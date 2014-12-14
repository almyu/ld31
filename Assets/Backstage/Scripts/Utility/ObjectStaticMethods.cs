using UnityEngine;

public class ObjectStaticMethods : MonoBehaviour {

    public void Instantiate(GameObject prefab) {
        Object.Instantiate(prefab);
    }

    public void InstantiateHere(GameObject prefab) {
        Object.Instantiate(prefab, transform.position, Quaternion.identity);
    }

    public void Destroy(GameObject obj) {
        Object.Destroy(obj);
    }

    public void DestroyImmediate(GameObject obj) {
        Object.DestroyImmediate(obj);
    }

    public void DestroySelf() {
        Object.Destroy(gameObject);
    }
}
