using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions controls;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerIndicator;

    public float playerSpeed = 150f;
    public bool hasPowerUp;
    private float powerStrength = 15.0f;

    void Awake()
    {
        controls = new InputSystem_Actions();
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }


    void Update()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
        float forwardInput = moveInput.y;
        powerIndicator.transform.position = transform.position + new Vector3(0,-0.5f,0);
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * playerSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            powerIndicator.gameObject.SetActive(hasPowerUp);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
        }
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerIndicator.gameObject.SetActive(hasPowerUp);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigibody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collided Whith" + collision.gameObject.name + "with power up set to" + hasPowerUp);
            enemyRigibody.AddForce(awayFromPlayer * powerStrength, ForceMode.Impulse);
        }
    }
}
