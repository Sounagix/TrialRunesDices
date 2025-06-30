using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int _maxSlots;

    [SerializeField]
    private int _maxCapacityPerSlot;

    private Dictionary<Item, int> _inventory = new();

    public static Inventory Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanAddItem()
    {
        return _inventory.Count < _maxSlots;
    }

    public void AddItem(Item item)
    {
        if (_inventory.ContainsKey(item) && item._stackeable && _inventory[item] < _maxCapacityPerSlot)
        {
            _inventory[item]++;
            print("Stack of " + item._itemName + " " + _inventory[item] + " amount");
        }
        else
        {
            print("new item " + item._itemName);
            _inventory.Add(item, 1);
        }
    }

    public Dictionary<Item,int> GetInventory()
    {
        return _inventory;
    }

    public void RemoveItem(Item item)
    {
        if (_inventory.ContainsKey(item))
        {
            if(item._stackeable && _inventory[item] > 1)
            {
                _inventory[item]--;
            }
            else
            {
                _inventory.Remove(item);
            }
        }
    }
}
