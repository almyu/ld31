using UnityEngine;

public class ComboSplash : MonoBehaviour {

    public static float abyss = -10f;
    public static float zenith = 5f;

    public Vector2 velocity;

    private void Update() {
        transform.position += (velocity * Time.deltaTime).WithZ(0f);
        
        velocity.y += Motor.gravity;

        if (transform.position.y < -10f)
            Destroy(gameObject);
    }
}
