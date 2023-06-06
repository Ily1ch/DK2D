using UnityEngine;


public class Projectile : MonoBehaviour
{
    [SerializeField] private float sspeed;
    private float direction;
    private bool hit;
    private float lifeetime;
    public int damage;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = sspeed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifeetime += Time.deltaTime;
        if (lifeetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TEstEnemy enemy = collision.GetComponent<TEstEnemy>();
        if (enemy != null)
        {
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("Explode");
            enemy.TakeDamage(damage);
        }
    }

    public void SetDirection(float _direction)
    {
        lifeetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
