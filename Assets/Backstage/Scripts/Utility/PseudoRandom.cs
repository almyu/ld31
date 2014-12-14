using UnityEngine;

public static class PseudoRandom {

    private static int[] cache = new int[1024];

    static PseudoRandom() {
        for (int i = 0; i < cache.Length; ++i)
            cache[i] = Random.Range(0, int.MaxValue);
    }

    public static int Value(int index) {
        return cache[index % cache.Length];
    }

    public static int Range(int index, int min, int max) {
        return Value(index) % (max - min) + min;
    }
}
