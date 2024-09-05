using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;  // 화살의 속도
    public float lifeTime = 3f;  // 화살이 살아있는 시간
    public int damage = 100;  // 화살이 입히는 데미지

    [SerializeField] private Rigidbody2D rb;

    private Vector2 direction = Vector2.right;

    private void OnEnable()
    {
        rb.velocity = direction * speed;
        Invoke(nameof(ReturnToPool), lifeTime);  // 일정 시간이 지나면 풀로 반환
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        rb.velocity = Vector2.zero;
        CancelInvoke();
        GameManager.Instance.ObjectPool.ReturnToPool("Arrow", gameObject);  // 풀로 반환
    }
}
