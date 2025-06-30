using Isometric2DGame.Player;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private TextMeshProUGUI _itemName;

    [SerializeField]
    private TextMeshProUGUI _itemCount;

    [SerializeField]
    private Button _button;

    private Item _item;


    public void SetUp(Item item, int num)
    {
        _item = item;
        _image.sprite = item._icon;
        _itemName.text = item._itemName;
        if (_item._stackeable)
            _itemCount.text = num.ToString();
        _button.interactable = true;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnEquipItem);
    }

    public void Clean()
    {
        if (!_item) return;
        _image.sprite = null;
        _itemName.text = "";
        if (_item._stackeable)
            _itemCount.text = "";
        _button.interactable = false;
        _item = null;
    }

    private void OnEquipItem()
    {
        if (_item is Potion) 
        {
            PlayerActions.EquipPotion?.Invoke(_item as Potion);
            Inventory.Instance.RemoveItem(_item);
            UiActions.UpdateInventory();
            return;
        }
        else if (_item is Weapon)
        {
            PlayerActions.EquipWeapon?.Invoke(_item as Weapon);
            Inventory.Instance.RemoveItem(_item);
            UiActions.UpdateInventory();
            return;
        }

    }
}
