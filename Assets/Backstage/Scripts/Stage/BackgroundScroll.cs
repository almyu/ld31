using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    [System.Serializable]
    public struct TileVariant {
        public Sprite sprite;
        public int weight;
    }

    [WeighedProperty]
    public TileVariant[] tileVariants;
    public int sortingOrder;

    public float speed, width;

    private int weightSum;
    private float stride;

    private void Awake() {
        if (tileVariants.Length == 0) return;

        var refSize = tileVariants[0].sprite.rect.size;
        weightSum = tileVariants[0].weight;

        for (int i = 1; i < tileVariants.Length; ++i) {
            var size = tileVariants[i].sprite.rect.size;

            if (!Mathf.Approximately(size.x, refSize.x) || !Mathf.Approximately(size.y, refSize.y))
                Debug.LogWarning("Background tile " + tileVariants[i].sprite + " seems to differ in size from the others");

            weightSum += tileVariants[i].weight;
        }

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
        var roll = Random.Range(0, weightSum);

        foreach (var tile in tileVariants) {
            if (roll < tile.weight) return tile.sprite;
            roll -= tile.weight;
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
