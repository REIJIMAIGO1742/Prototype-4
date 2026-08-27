using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;

    private float pushFoce = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        OnEnable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);

        if (transform.position.y < -10)
        {
            OnDisable();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Vector3 theBullet = (transform.position - collision.transform.position);
            enemyRb.AddForce(theBullet * pushFoce, ForceMode.Impulse);
        }
    }


    private void OnEnable()
    {
        EnemyManager.activeEnemy.Add(gameObject);
    }
    void OnDisable()
    {
        EnemyManager.activeEnemy.Remove(gameObject);
    }
}
