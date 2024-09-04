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

        // Ÿ�̸ӷ� ���� �ð��� ������ Ǯ�� ��ȯ
        Invoke(nameof(ReturnToPool), lifeTime);      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �浹 �� Ǯ�� ��ȯ
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        CancelInvoke();  // lifeTime Ÿ�̸Ӹ� ���
        GameManager.Instance.ObjectPool.ReturnToPool("Arrow", gameObject);  // Ǯ�� ��ȯ
    }
}
