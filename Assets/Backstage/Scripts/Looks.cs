using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class Looks : MonoBehaviour {

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

    public void FaceRight() {
        transform.rotation = Quaternion.identity;
    }

    public void FaceLeft() {
        transform.rotation = Quaternion.AngleAxis(180f, Vector3.up);
    }

    public void SetApparentVelocity(float vel) {
        if (vel < -Mathf.Epsilon) FaceLeft();
        else if (vel > Mathf.Epsilon) FaceRight();

        cachedAnimator.SetFloat("Velocity", vel);
        Debug.Log("Vel: " + vel);
    }
}
