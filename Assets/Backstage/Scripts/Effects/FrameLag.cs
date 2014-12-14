using UnityEngine;
using System.Collections;

public class FrameLag : MonoBehaviour {

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
