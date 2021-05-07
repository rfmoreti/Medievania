using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private string destinationScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.name == "FallDeath")
                PermanentUI.UI.Reset();                            
            else
                PermanentUI.UI.coinCheckpoint = PermanentUI.UI.goldCoins;



            SceneManager.LoadScene(destinationScene);
        }
    }
}
