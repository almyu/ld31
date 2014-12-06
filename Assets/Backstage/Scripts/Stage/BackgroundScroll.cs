using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    public Sprite[] tileVariants;
    public Vector2 extent;
    public int sortingOrder;

    public float speed;

    private void Awake() {
        if (tileVariants.Length == 0) return;

        var refSize = tileVariants[0].rect.size;

        for (int i = 1; i < tileVariants.Length; ++i) {
            var size = tileVariants[i].rect.size;

            if (!Mathf.Approximately(size.x, refSize.x) || !Mathf.Approximately(size.y, refSize.y))
                Debug.LogWarning("Background tile " + tileVariants[i] + " seems to differ in size from the others");
        }

        for (var pos = -extent; pos.x + refSize.x < extent.x; pos.x += refSize.x) {
            var tile = new GameObject("BackgroundTile", typeof(SpriteRenderer));
            tile.transform.parent = transform;
            tile.transform.localPosition = pos.WithZ(0f);

            var ren = tile.GetComponent<SpriteRenderer>();
            ren.sprite = tileVariants[Random.Range(0, tileVariants.Length)];
            ren.sortingOrder = sortingOrder;
        }
    }

    private void Update() {
        foreach (Transform child in transform)
            child.localPosition = child.localPosition.WithX(Mathf.Repeat(child.localPosition.x + speed * Time.deltaTime, extent.x * 2f));
    }
}
