using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Transporter : MonoBehaviour {

    public float speed;
    public float tilesPerUnit = 1f;

    private MaterialPropertyBlock props;
    public Vector4 st;

    private void Awake() {
        props = new MaterialPropertyBlock();
        st = renderer.sharedMaterial.GetVector("_MainTex_ST");
    }

    private void Update() {
        st.z = Mathf.Repeat(st.z + speed * tilesPerUnit * Time.deltaTime, 1f);

        props.Clear();
        props.AddVector("_MainTex_ST", st);

        renderer.SetPropertyBlock(props);
    }
}
