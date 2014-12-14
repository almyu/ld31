using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class MrScreenshaker : MonoSingleton<MrScreenshaker> {

    public Sprite activeSprite, inactiveSprite;
    public Vector2 power = Vector2.one;
    public float shakeDuration = 0.2f, spriteDuration = 0.5f;

    private SpriteRenderer cachedRenderer;


    private void Awake() {
        cachedRenderer = GetComponent<SpriteRenderer>();
    }

    public void Activate(float factor) {
        StartCoroutine(Act(factor));
    }

    public IEnumerator Act(float factor) {
        var cam = Camera.main.transform;
        var shakeEnd = Time.time + shakeDuration;
        var spriteEnd = Time.time + spriteDuration;

        if (cachedRenderer.sprite == activeSprite) {
            cachedRenderer.sprite = inactiveSprite;
            yield return null;
        }

        while (Time.time < shakeEnd) {
            cachedRenderer.sprite = activeSprite;

            var offset = Vector2.Scale(Random.onUnitSphere, power).WithZ(0f);

            cam.position += offset;
            yield return null;
            cam.position -= offset;
        }

        while (Time.time < spriteEnd) {
            cachedRenderer.sprite = activeSprite;
            yield return null;
        }

        cachedRenderer.sprite = inactiveSprite;
    }
}
