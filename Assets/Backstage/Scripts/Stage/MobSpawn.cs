using UnityEngine;

public class MobSpawn : MonoBehaviour {

    public bool leftSide;
    public Vector2 checkIntervalRange = new Vector2(2f, 4f);

    public static int totalAlive, totalBerserks;
    [HideInInspector]
    public int numAlive;

    public float proportion {
        get { return (float) numAlive / totalAlive; }
    }

    public bool hasPriority {
        get { return proportion <= (leftSide ? Balance.instance.spawnPriority : 1f - Balance.instance.spawnPriority); }
    }

    public bool worldUnderpopulated {
        get { return totalAlive < Balance.instance.mobs; }
    }

    public static float dangerometer {
        get { return totalBerserks * 0.5f / Balance.instance.berserkers; }
    }

    public static float peaceometer {
        get { return 1f - dangerometer; }
    }


    public GameObject prefab;
    public Vector2 groundRange = new Vector2(-2.1f, -2.5f);
    public Vector2 sortingRange = new Vector2(0, 8);

    public void Spawn() {
        var mob = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
        var mortal = mob.GetComponent<Mortal>();

        mortal.health = Balance.instance.mobHealth;

        ++numAlive;
        ++totalAlive;

        mortal.onDeath.AddListener(() => {
            --numAlive;
            --totalAlive;
        });

        var layer = Random.value;

        mob.GetComponent<Motor>().ground = Mathf.Lerp(groundRange[0], groundRange[1], layer);
        mob.GetComponentInChildren<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(Mathf.Lerp(sortingRange[0], sortingRange[1], layer));

        mob.GetComponent<Berserker>().damage = Balance.instance.mobDamage;
    }

    private float RollCheckInterval() {
        return Random.Range(checkIntervalRange[0], checkIntervalRange[1]);
    }

    private void Start() {
        Invoke("CheckForSpawn", RollCheckInterval());
    }

    private void CheckForSpawn() {
        if (worldUnderpopulated)
            Spawn();

        Invoke("CheckForSpawn", RollCheckInterval());
    }
}
