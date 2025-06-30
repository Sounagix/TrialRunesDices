using Isometric2DGame.Player;
using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private float _interactionRange;

    [SerializeField]
    private Dialogue _dialogue;



    private void Awake()
    {
        GetComponent<CircleCollider2D>().radius = _interactionRange;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehaviour playerBehaviour = collision.gameObject.GetComponent<PlayerBehaviour>();
        if (playerBehaviour)
        {
            playerBehaviour.RequestNPCAction(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerBehaviour playerBehaviour = collision.gameObject.GetComponent<PlayerBehaviour>();
        if (playerBehaviour)
        {
            playerBehaviour.RemoveRequestNPCAction(this);
            UiActions.CloseDialogueUI?.Invoke();
        }
    }

    public void StartToTalk()
    {
        UiActions.OpenDialogueUI?.Invoke(_dialogue);
    }
}
