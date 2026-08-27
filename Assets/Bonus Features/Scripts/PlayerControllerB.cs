using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using NUnit.Framework;

public class PlayerControllerB : MonoBehaviour
{
    private InputSystem_Actions controls;
    
    private GameObject focalPoint;
    public GameObject powerIndicator;
    public GameObject smashField;
    public GameObject bulletPrefab;
    private GameObject enemytarget;

    private Rigidbody playerRb;

    public float playerSpeed = 150f;
    private float powerStrength = 15.0f;
    private float jumpPower = 15f;
    private float downPower = 60f;

    public bool hasPowerUp;
    public bool hasPowerUp2;
    public bool hasPowerUp3;
    public bool bulletCooldown;
    

    void Awake()
    {
        controls = new InputSystem_Actions(); // Save for new input
        playerRb = GetComponent<Rigidbody>(); // Get rigidbody component
        focalPoint = GameObject.Find("Focal Point"); // Find object by name string
        
    }

    private void OnEnable()
    {
        controls.Player.Enable(); // Enable Input
    }


    void Update()
    {

        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>(); // Chang value for nove input to vector2 x/y
        float forwardInput = moveInput.y; // Save y value
        powerIndicator.transform.position = transform.position + new Vector3(0,-0.5f,0); // PowerIndicator follow player position and move y -0.5
        smashField.transform.position = transform.position + new Vector3(0,-0.5f,0);
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * playerSpeed * Time.deltaTime); // When press move key player move by forward of focalpoint

    }

    // When collider enter and is trigger
    private void OnTriggerEnter(Collider other)
    {
        // If that collider have tag powerup then
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            powerIndicator.gameObject.SetActive(hasPowerUp); // Show powerIndicator white hasPowerup is true
            Destroy(other.gameObject); // Destroy gameobject onEnter
            StartCoroutine(PowerUpCountDownRoutine()); // Start powerUpCountDownRoutine
        }
        // else If that collider have tag powerup2 then
        else if (other.CompareTag("PowerUp2"))
        {
            hasPowerUp2 = true;
            bulletCooldown = true;
            powerIndicator.gameObject.SetActive(hasPowerUp2);
            Destroy(other.gameObject);
            StartCoroutine(FireBullet()); // Start FireBullet
            StartCoroutine(PowerUpCountDownRoutine());

        }
        else if (other.CompareTag("PowerUp3"))
        {
            hasPowerUp3 = true;
            powerIndicator.gameObject.SetActive(hasPowerUp3);
            Destroy(other.gameObject);
            StartCoroutine(SmashPowerUpRoutine());
            StartCoroutine(PowerUpCountDownRoutine());
        }
    }

    IEnumerator FireBullet()
    {
        // Save gameobject this time when new gameobject not count until player get powerup againt 
        List<GameObject> target = new List<GameObject>(EnemyManager.activeEnemy);
        float time = 0f;
        // Loop until time out
        while (time < 7f)
        {
            for (int i = 1; i < target.Count; i++)
            {
                enemytarget = target[i];
                Vector3 spawnPos = transform.position + new Vector3(0, 0, 2);
                if (enemytarget != null)
                {
                    GameObject newBullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
                    newBullet.GetComponent<Bullet>().SetTarget(enemytarget);
                }
                else
                {
                    Debug.Log("No Enemy in scean");
                }
            }

            yield return new WaitForSeconds(1f);
            time += 1f;
        }
    }

    IEnumerator SmashPowerUpRoutine()
    {

        float time = 0f;
        while (time < 7)
        {
            smashField.gameObject.SetActive(false);
            playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            yield return new WaitForSeconds(1f);
            smashField.gameObject.SetActive(true);
            playerRb.AddForce(Vector3.down * downPower, ForceMode.Impulse);
            yield return new WaitForSeconds(0.2f);
            smashField.gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);
            time += 3.2f;
        }

        smashField.gameObject.SetActive(false);
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        hasPowerUp2 = false;
        hasPowerUp3 = false;
        powerIndicator.gameObject.SetActive(hasPowerUp);
        powerIndicator.gameObject.SetActive(hasPowerUp2);
        powerIndicator.gameObject.SetActive(hasPowerUp3);
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
