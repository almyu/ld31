using UnityEngine;
using System.Collections;

public class FrameLagProcess : MonoSingleton<FrameLagProcess> {

    public void Lag(int frames) {
        StartCoroutine(DoLag(frames));
    }

    private IEnumerator DoLag(int frames) {
        TimescaleStack.Push(0f);

        while (frames-- > 0)
            yield return null;

        TimescaleStack.Pop();
    }
}
