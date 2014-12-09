using UnityEngine;

[RequireComponent(typeof(Motor), typeof(Looks))]
public class AirborneMob : MonoBehaviour {

    public float kickThreshold = 10f;
    public Sprite sprite;

    private Looks cachedLooks;


    private void Awake() {
        cachedLooks = GetComponent<Looks>();
    }

    public void HandleCombo(ComboNode combo) {
        if (combo.targetVelocityAdd.y < kickThreshold) return;

        cachedLooks.SetSprite(sprite);
        Invoke("RestoreLook", 1f);
    }

    private void RestoreLook() {
        cachedLooks.SetDefault();
    }
}
