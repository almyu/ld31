using UnityEngine;

[RequireComponent(typeof(Motor), typeof(Looks))]
public class AirborneMob : MonoBehaviour {

    public Sprite sprite;

    private Motor cachedMotor;
    private Looks cachedLooks;


    private void Awake() {
        cachedMotor = GetComponent<Motor>();
        cachedLooks = GetComponent<Looks>();
    }

    public void SendToMoon(float velocity) {
        cachedMotor.velocity.y = velocity;
        cachedLooks.SetSprite(sprite);
    }
}
