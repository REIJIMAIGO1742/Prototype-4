using UnityEngine;
using UnityEngine.Rendering;

public class SmashField : MonoBehaviour
{
    private Rigidbody fieldRb;

    public float pushPower;

    private void Start()
    {
        fieldRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 pushField = (collision.gameObject.transform.position - transform.position);
            enemyRb.AddForce(pushField * pushPower, ForceMode.Impulse);
        }
        
    }
}
