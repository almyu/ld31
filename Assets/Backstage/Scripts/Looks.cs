using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class Looks : MonoBehaviour {

    public string stance = "Stance";
    public Sprite jump, fall;

    private Animator cachedAnimator;
    private SpriteRenderer cachedRenderer;

    private void Awake() {
        cachedAnimator = GetComponent<Animator>();
        cachedRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite) {
        cachedAnimator.enabled = false;
        cachedRenderer.sprite = sprite;
    }

    public void SetAnimation(string stateName) {
        cachedAnimator.enabled = true;
        cachedAnimator.Play(stateName);
    }

    public void SetDefault() {
        SetAnimation(stance);
    }

    public bool IsDefault() {
        return cachedAnimator.enabled || cachedRenderer.sprite == jump || cachedRenderer.sprite == fall;
    }

    public void FaceRight() {
        transform.rotation = Quaternion.identity;
    }

    public void FaceLeft() {
        transform.rotation = Quaternion.AngleAxis(180f, Vector3.up);
    }

    public void SetApparentVelocity(float velx) {
        if (velx < -Mathf.Epsilon) FaceLeft();
        else if (velx > Mathf.Epsilon) FaceRight();

        cachedAnimator.SetFloat("Velocity", velx);
    }

    public void SetApparentVelocity(Vector2 vel) {
        SetApparentVelocity(vel.x);

        if (!IsDefault()) return;

        if (Mathf.Approximately(vel.y, 0f))
            SetAnimation(stance);
        else
            SetSprite(vel.y > 0f ? jump : fall);
    }
}
