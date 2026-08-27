using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    private GameObject[] enemies; // save gameobject is arry becuase Have Many gamobject to save
    private Rigidbody bulletRb;
    private GameObject enemyTarget;
    public float bulletSpeed;

    public void SetTarget(GameObject target)
    {
        enemyTarget = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTarget != null) // If that enemy is destroy then
        {
            transform.LookAt(enemyTarget.transform.position); // Bullet lookat target
            Vector3 enemyPos = enemyTarget.transform.position; // Read Vector 3 of enemy Position save to enemyPos
            transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime); // go forward
        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // OncollisionEnter and this gameobjact have tag Enemy then
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
