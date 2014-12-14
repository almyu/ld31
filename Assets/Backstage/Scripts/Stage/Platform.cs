using UnityEngine;

public class Platform : MonoBehaviour {

    public float width = 1f;

    private void OnDrawGizmos() {
        Gizmos.DrawLine(
            transform.position + Vector3.left * width * 0.5f,
            transform.position + Vector3.right * width * 0.5f);
    }
}
