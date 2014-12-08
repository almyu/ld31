using UnityEngine;

public class Balance : MonoSingleton<Balance> {

    public int mobHealth = 1000;
    public int mobs = 8;
    public int berserkers = 4;
    public float spawnPriority = 0.5f;
}
