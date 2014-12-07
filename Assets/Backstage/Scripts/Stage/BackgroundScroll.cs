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
    public int sortingOrder;

    public float speed, width;
    private float stride;

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

        stride = (refSize.x - 0.5f) / tileVariants[0].sprite.pixelsPerUnit;

        var numTiles = Mathf.CeilToInt(width / stride);

        width = numTiles * stride;

        for (var i = 0; i < numTiles; ++i) {
            var tile = new GameObject("Tile", typeof(SpriteRenderer));
            tile.transform.parent = transform;
            tile.transform.localPosition = Vector2.right * i * stride;

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
            var x = child.localPosition.x + speed * Time.deltaTime;

            if (0f > x || x >= width) {
                child.GetComponent<SpriteRenderer>().sprite = RollSprite();
                x = Mathf.Repeat(x + width, width);
            }

            child.localPosition = child.localPosition.WithX(x);
        }
    }
}
