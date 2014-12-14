using UnityEngine;

public class PlatformManager : MonoBehaviour {

    [System.Serializable]
    public class PlatformSetup {
        public Platform prefab;
        public int weight;
    }

    [WeighedProperty]
    public PlatformSetup[] prefabs;

    public float width = 20f;

    private int weightSum;
    private float maxPlatformWidth;
    private int[] slotIndices;


    private void Awake() {
        foreach (var prefab in prefabs)
            weightSum += prefab.weight;

        maxPlatformWidth = 0f;

        foreach (var prefab in prefabs)
            if (prefab.prefab)
                if (maxPlatformWidth < prefab.prefab.width)
                    maxPlatformWidth = prefab.prefab.width;

        var numPlatforms = Mathf.CeilToInt(width / maxPlatformWidth);
        width = numPlatforms * maxPlatformWidth;

        slotIndices = new int[numPlatforms];

        for (int i = 0; i < numPlatforms; ++i) {
            var slot = new GameObject("Slot" + i);
            slot.transform.SetParent(transform, false);
            slot.transform.localPosition = Vector3.right * maxPlatformWidth * i;

            slotIndices[i] = i;
        }
    }

    private Platform RollPrefab(int index) {
        var roll = PseudoRandom.Range(Mathf.Abs(index + 100), 0, weightSum);

        foreach (var prefab in prefabs) {
            if (roll < prefab.weight) return prefab.prefab;
            roll -= prefab.weight;
        }

        return null;
    }

    private void Update() {
        var i = 0;

        foreach (Transform child in transform) {
            var x = child.localPosition.x + Motor.wind.x * Time.deltaTime;

            if (0f > x || x >= width) {
                if (0f > x)
                    slotIndices[i] += slotIndices.Length;
                else
                    slotIndices[i] -= slotIndices.Length;

                ReplacePrefab(child, RollPrefab(slotIndices[i]));
                x = Mathf.Repeat(x + width, width);
            }

            child.localPosition = child.localPosition.WithX(x);
            ++i;
        }
    }

    private void RemoveChildren(Transform slot) {
        for (int i = slot.childCount; i-- != 0; )
            DestroyImmediate(slot.GetChild(i).gameObject);
    }

    private void ReplacePrefab(Transform slot, Platform prefab) {
        var actual = slot.GetComponentInChildren<Platform>();
        if (actual && prefab && Mathf.Approximately(actual.width, prefab.width)) return;

        if (actual)
            RemoveChildren(slot);

        if (prefab) {
            var obj = (GameObject) Instantiate(prefab.gameObject, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(slot, false);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * width);
    }
}
