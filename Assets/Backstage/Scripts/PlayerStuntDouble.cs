using UnityEngine;

public class PlayerStuntDouble : MonoBehaviour {

    public void Activate() {
        GetComponentInChildren<Animator>().enabled = true;
        Destroy(this);
    }
}
