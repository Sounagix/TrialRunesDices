using Isometric2DGame.Player;
using UnityEngine;

public class ItemOnGame : MonoBehaviour
{
    [SerializeField]
    private Item _item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehaviour playerBehaviour = collision.gameObject.GetComponent<PlayerBehaviour>();
        if(playerBehaviour)
        {
            playerBehaviour.RequestItemToPick(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerBehaviour playerBehaviour = collision.gameObject.GetComponent<PlayerBehaviour>();
        if (playerBehaviour)
        {
            playerBehaviour.RemoveRequestItemToPick(this);
        }
    }

    public Item GetItem()
    {
        return _item;
    }

    public void PickItem()
    {
        Inventory.Instance.AddItem(_item);
        Destroy(gameObject);
    }
}
