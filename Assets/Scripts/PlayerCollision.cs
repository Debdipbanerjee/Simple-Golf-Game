using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    string currentScene;
    public BallController ballController;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Danger")
        {
            Destroy(gameObject);
            SceneManager.LoadScene(currentScene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Powerup")
        {
            ballController.shotPower *= 2;
            Destroy(other.gameObject);

        }

        if (other.name == "Poison")
        {
            ballController.shotPower /= 2;
            Destroy(other.gameObject);
        }
    }
}
