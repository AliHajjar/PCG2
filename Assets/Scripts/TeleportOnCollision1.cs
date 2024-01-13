using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportOnCollision1 : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Make sure your player has the tag "Player"
        {
            SceneManager.LoadScene("Terrain");
        }
    }
}