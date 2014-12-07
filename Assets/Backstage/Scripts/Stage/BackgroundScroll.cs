using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    [System.Serializable]
    public struct TileVariant {
        public Sprite sprite;
        public int weight;

        [HideInInspector]
        public float chance;
    }

    public TileVariant[] tileVariants;
    public Vector2 extent;
    public int sortingOrder;

    public float speed;

    private void Awake() {
        if (tileVariants.Length == 0) return;

        var refSize = tileVariants[0].sprite.rect.size;
        var weightSum = 0;

        for (int i = 1; i < tileVariants.Length; ++i) {
            var size = tileVariants[i].sprite.rect.size;

            if (!Mathf.Approximately(size.x, refSize.x) || !Mathf.Approximately(size.y, refSize.y))
                Debug.LogWarning("Background tile " + tileVariants[i].sprite + " seems to differ in size from the others");

            weightSum += tileVariants[i].weight;
        }

        for (int i = 0; i < tileVariants.Length; ++i)
            tileVariants[i].chance = (float) tileVariants[i].weight / weightSum;

        for (var pos = -extent; pos.x + refSize.x < extent.x; pos.x += refSize.x) {
            var tile = new GameObject("BackgroundTile", typeof(SpriteRenderer));
            tile.transform.parent = transform;
            tile.transform.localPosition = pos.WithZ(0f);

            var ren = tile.GetComponent<SpriteRenderer>();
            ren.sprite = RollSprite();
            ren.sortingOrder = sortingOrder;
        }
    }

    private Sprite RollSprite() {
        var roll = Random.value;

        foreach (var tile in tileVariants) {
            if (roll <= tile.chance) return tile.sprite;
            roll -= tile.chance;
        }

        return tileVariants[0].sprite;
    }

    private void Update() {
        foreach (Transform child in transform) {
            child.localPosition += Vector3.right * speed * Time.deltaTime;

            if (Mathf.Abs(child.localPosition.x) > extent.x) {
                child.GetComponent<SpriteRenderer>().sprite = RollSprite();
                child.localPosition = Vector2.right * extent.x * (child.localPosition.x < 0f ? 2f : -2f);
            }
        }
    }
}
