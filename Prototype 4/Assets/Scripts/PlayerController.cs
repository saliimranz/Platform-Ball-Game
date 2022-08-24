using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject PowerUpIndicator;
    public bool hasPowerup = false;
    public float playerSpeed = 15.0f;
    public float powerUpStrenght = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * playerSpeed * forwardInput);
        PowerUpIndicator.transform.position = transform.position - new Vector3(0,-0.3f,0);
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Powerup")){
            hasPowerup = true;
            Destroy(other.gameObject);
            PowerUpIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerUpCountDownRoutine());
        }
    }

    IEnumerator PowerUpCountDownRoutine(){
        yield return new WaitForSeconds(8);
        hasPowerup = false; 
        PowerUpIndicator.gameObject.SetActive(false);
        }
        
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup){
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

           Debug.Log("Collided with: "+ collision.gameObject.name + " with Powerup set to "+ hasPowerup);
           enemyRb.AddForce(awayFromPlayer*powerUpStrenght, ForceMode.Impulse);
        }
    }
}
