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
}
