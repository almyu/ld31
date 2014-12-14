using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class MrScreenshaker : MonoSingleton<MrScreenshaker> {

    public Sprite activeSprite, inactiveSprite;
    public Vector2 power = Vector2.one;
    public float duration = 0.5f;

    private SpriteRenderer cachedRenderer;


    private void Awake() {
        cachedRenderer = GetComponent<SpriteRenderer>();
    }

    public void Activate() {
        StartCoroutine(Act());
    }

    public IEnumerator Act() {
        var cam = Camera.main.transform;
        var end = Time.time + duration;

        if (cachedRenderer.sprite == activeSprite) {
            cachedRenderer.sprite = inactiveSprite;
            yield return null;
        }

        cachedRenderer.sprite = activeSprite;

        while (Time.time < end) {
            var offset = Vector2.Scale(Random.onUnitSphere, power).WithZ(0f);

            cam.position += offset;
            yield return null;
            cam.position -= offset;
        }

        cachedRenderer.sprite = inactiveSprite;
    }
}
