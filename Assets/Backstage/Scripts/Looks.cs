using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class Looks : MonoBehaviour {

    public Motor motor;

    public string stance = "Stance";
    public Sprite jump, fall;

    private Animator cachedAnimator;
    private SpriteRenderer cachedRenderer;

    private void Awake() {
        if (!motor) motor = GetComponentInChildren<Motor>();

        cachedAnimator = GetComponent<Animator>();
        cachedRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (!motor) return;

        SetApparentVelocity(motor.velocity.x);

        if (IsDefault()) SetDefault();
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
        if (!motor || motor.isGrounded)
            SetAnimation(stance);
        else
            SetSprite(motor.velocity.y > 0f ? jump : fall);
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
}
