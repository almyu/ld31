using UnityEngine;
using System.Collections;

public class FrameLag : MonoBehaviour {

    public static void Lag(int frames) {
        FrameLagProcess.instance.Lag(frames);
    }
}
