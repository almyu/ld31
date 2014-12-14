using UnityEngine;

public class PlatformManager : MonoBehaviour {

    [System.Serializable]
    public class PlatformSetup {
        public GameObject prefab;
        public int weight;
    }

    [WeighedProperty]
    public PlatformSetup[] prefabs;

    public Vector2 unitsPerPlatformRange = new Vector2(10f, 20f);

    private Transform cachedTransform;
    private int weightSum;
    private float lastSpawnX, nextSpawnDistance;


    private void Awake() {
        cachedTransform = transform;

        foreach (var prefab in prefabs)
            weightSum += prefab.weight;

        nextSpawnDistance = RollDistance();
    }

    private GameObject RollPrefab() {
        var roll = Random.Range(0, weightSum);

        foreach (var prefab in prefabs) {
            if (roll < prefab.weight) return prefab.prefab;
            roll -= prefab.weight;
        }

        return null;
    }

    private float RollDistance() {
        return Random.Range(unitsPerPlatformRange[0], unitsPerPlatformRange[1]);
    }

    private void Update() {
        cachedTransform.position += Vector3.right * (Motor.wind.x * Time.deltaTime);

        if (Mathf.Abs(cachedTransform.position.x - lastSpawnX) >= nextSpawnDistance) {
            lastSpawnX = cachedTransform.position.x;
            nextSpawnDistance = RollDistance();

            SpawnPlatform();
        }
    }

    private void SpawnPlatform() {
        var obj = (GameObject) Instantiate(RollPrefab(), Vector3.zero, Quaternion.identity);
        obj.transform.SetParent(cachedTransform, true);
        // obj.transform.localPosition = Vector3.up * Random.value;
    }
}
