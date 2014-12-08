using UnityEngine;

public class Skyguard : MonoSingleton<Skyguard> {

    public Transform imp;
    public GameObject[] idleObjects, activeObjects;
    public float timeout = 1f;
    public float force = -10f;
    public int damage = 500;

    public void Activate(Motor motor) {
        Disappear(imp.gameObject);
        imp.transform.position = imp.position.WithX(motor.transform.position.x);
        Appear(imp.gameObject);

        foreach (var obj in idleObjects) Disappear(obj);
        foreach (var obj in activeObjects) Appear(obj);

        motor.velocity.y = force;

        var mortal = motor.GetComponent<Mortal>();
        if (mortal)
            mortal.Hit(damage, transform.position + Vector3.up);

        CancelInvoke("Return");
        Invoke("Return", timeout);
    }

    public void Return() {
        foreach (var obj in activeObjects) Disappear(obj);
        foreach (var obj in idleObjects) Appear(obj);
    }

    private void Appear(GameObject obj) {
        if (obj.activeSelf) return;

        obj.SetActive(true);
        obj.GetComponentInChildren<ParticleSystem>().Play();
    }

    private void Disappear(GameObject obj) {
        if (!obj.activeSelf) return;

        var psObjProto = obj.GetComponentInChildren<ParticleSystem>().gameObject;
        var psObj = Instantiate(psObjProto, psObjProto.transform.position, Quaternion.identity) as GameObject;
        var ps = psObj.GetComponent<ParticleSystem>();

        psObj.AddComponent<Lifetime>().lifetime = ps.duration + ps.startLifetime;

        obj.SetActive(false);
    }
}
