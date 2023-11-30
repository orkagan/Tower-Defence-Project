using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    #region Fields
    [SerializeField] float _speed;

    public float maxSpeed;
    public bool hasGravity;
    public float gravityScale;
    public float lifespan;
    private float _timeTillDeath;
    public int maxHits;
    public int hits;
    public float initialSpeed;
    public Vector3 direction;
    [HideInInspector] public float damage;

    Rigidbody rb => GetComponent<Rigidbody>();
    Collider hitbox => GetComponent<Collider>();

    bool isSpawned = false;
    #endregion

    #region Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy e = other.GetComponent<Enemy>();
            e.DecreaseHealth((int)(damage * _speed / 5));
        }
    }

    public void Spawn(Vector3 direction, Vector3 position) //this could be tonnes of things
    {
        Instantiate(gameObject, position, Quaternion.identity);        
        rb.AddForce(direction * initialSpeed, ForceMode.VelocityChange);// i think this is applying to the prefab and not the instance somehow   

        isSpawned = true;
    }

    public void Die()
    {
        DestroyImmediate(gameObject);
    }
    #endregion

    #region Unity Methods
    private void Update()
    {
        _speed = rb.velocity.magnitude;

        if (isSpawned)
        {
            _timeTillDeath -= Time.deltaTime;
        }

        if (_timeTillDeath <= 0)
        {
            Die();
        }
    }

    private void OnValidate()
    {
        rb.useGravity = hasGravity;
    }

    public void Awake()
    {

    }

    public void Start()
    {
        _timeTillDeath = lifespan;
    }
    #endregion 
}
