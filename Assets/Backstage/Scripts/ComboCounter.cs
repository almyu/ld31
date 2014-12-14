using UnityEngine;
using UnityEngine.UI;

public class ComboCounter : MonoSingleton<ComboCounter> {

    public Text pointsLabel, hitsLabel;
    public bool freemode = false;

    private long points, hits;

    private void Update() {
        pointsLabel.text = points + "";
        hitsLabel.text = hits + "";
    }

    public void AddPoints(long pts) {
        if (!freemode)
            points += pts;
    }

    public void AddHit() {
        ++hits;
    }

    public void ResetHits() {
        if (!freemode)
            points += hits * hits * hits;

        hits = 0;
    }
}
