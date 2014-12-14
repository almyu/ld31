using UnityEngine;
using System.Collections;

public class FrameLag : MonoBehaviour {

    public void Lag(int frames) {
        FrameLagProcess.instance.Lag(frames);
    }
}
