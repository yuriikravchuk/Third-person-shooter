//using pool;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public Rigidbody rig;
    public Renderer rend;
    Material mat;
    Color initialColor;
    //public PoolObject poolObject;

    float lifeTime = 6;

    public float forceMin;
    public float forceMax;
    float force;

    private void Awake()
    {
        mat = rend.material;
        initialColor = mat.color;
    }
    void OnEnable()
    {
        force = Random.Range(forceMin, forceMax);
        rig.AddForce(transform.right * force);
        rig.AddTorque(Random.insideUnitSphere * force);
        Invoke(nameof(Return), lifeTime);
    }

    private void OnDisable()
    {
        rig.angularVelocity = Vector3.zero;
        rig.velocity = Vector3.zero;
        mat.color = initialColor;
    }

    void Return()
    {
        //poolObject.Return();
    }
}