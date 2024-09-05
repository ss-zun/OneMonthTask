using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;  // ȭ���� �ӵ�
    public float lifeTime = 3f;  // ȭ���� ����ִ� �ð�
    public int damage = 100;  // ȭ���� ������ ������

    [SerializeField] private Rigidbody2D rb;

    private Vector2 direction = Vector2.right;

    private void OnEnable()
    {
        rb.velocity = direction * speed;
        Invoke(nameof(ReturnToPool), lifeTime);  // ���� �ð��� ������ Ǯ�� ��ȯ
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
        GameManager.Instance.ObjectPool.ReturnToPool("Arrow", gameObject);  // Ǯ�� ��ȯ
    }
}
