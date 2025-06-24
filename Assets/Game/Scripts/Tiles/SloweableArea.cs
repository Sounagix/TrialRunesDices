using Isometric2DGame.Player;
using UnityEngine;

public class SloweableArea : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GeneralData.playerTag))
        {
            PlayerBehaviour playerBehaviour = collision.GetComponent<PlayerBehaviour>();
            if (playerBehaviour)
                playerBehaviour.SlowsDownPlayer();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(GeneralData.playerTag))
        {
            PlayerBehaviour playerBehaviour = collision.GetComponent<PlayerBehaviour>();
            if (playerBehaviour)
                playerBehaviour.ReturnNormalSpeed();
        }
    }
}
