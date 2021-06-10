using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private GameController gameController;
    bool onRegenHealth = false;

    private void Start()
    {
        if (!gameController)
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, 1) * gameController.speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * gameController.jumpIntensity, ForceMode.Impulse);
        // Allow player to drop down while in mid-air using down-arrow or S
        if(!isGrounded && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * gameController.jumpIntensity * 2, ForceMode.Impulse);
        if (transform.position.y < -10)
            gameController.GOscreen();
        //Debug.Log("Update - " + Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
            isGrounded = true;
        /*
        if (collision.gameObject.tag == "Obstacle")
        {
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Obstacle Hit");
            gameController.health -= 10;
        }
        */
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
            isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("Obstacle Hit");
            gameController.health -= 10;
        }
        if (other.gameObject.tag == "Slime")
        {
            Debug.Log("Slime HIT");
            gameController.health -= 5;
        }

        if (other.gameObject.tag == "Cheese")
        {
            Debug.Log("CHEESE HIT");
            gameController.cheeseCount += 1;
            gameController.totalCheese += 1;
        }
        if (other.gameObject.tag == "Poison")
        {
            Debug.Log("Poison Hit");
            if (gameController.health != 1)
            {
                gameController.health = 1;
            }
        }
        if (other.gameObject.tag == "Heal")
        {
            Debug.Log("Heal Hit");
            if (gameController.health < gameController.maxHealth && onRegenHealth == false)
            {
                onRegenHealth = true;
                StartCoroutine("RegenHealth");
            }
        }
    }
    IEnumerator RegenHealth()
    {
        for (int i = 0; i < 10; i++)
            if (gameController.health < gameController.maxHealth)
            {
                gameController.health += 1;
                yield return new WaitForSeconds(1);
                onRegenHealth = false;
            }
    }
}
