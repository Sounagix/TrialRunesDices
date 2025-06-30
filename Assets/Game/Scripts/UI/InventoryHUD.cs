using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryHUD : MonoBehaviour
{
    [SerializeField]
    private List<ItemInventory> _items = new();

    [SerializeField]
    private GameObject _mainPanel;

    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    private void OnEnable()
    {
        UiActions.OnOpenInventory += OpenInventory;
        UiActions.UpdateInventory += UpdateInventory;
    }

    private void OnDisable()
    {
        UiActions.OnOpenInventory -= OpenInventory;
        UiActions.UpdateInventory -= UpdateInventory;
    }


    private void OpenInventory()
    {
        if (_mainPanel.activeInHierarchy)
        {
            _mainPanel.SetActive(false);
        }
        else
        {
            _mainPanel.SetActive(true);
            Dictionary<Item, int> inv = Inventory.Instance.GetInventory();
            int index = 0;
            foreach (Item i in inv.Keys)
            {
                _items[index].SetUp(i, inv[i]);
                index++;
            }
        }

    }

    private void UpdateInventory()
    {
        CleanInventory();
        Dictionary<Item, int> inv = Inventory.Instance.GetInventory();
        int index = 0;
        foreach (Item i in inv.Keys)
        {
            _items[index].SetUp(i, inv[i]);
            index++;
        }
    }

    private void CleanInventory()
    {

        foreach (ItemInventory i in _items)
        {
            i.Clean();
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            raycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                Button button = result.gameObject.GetComponent<Button>();
                if (button && button.interactable)
                {
                    button.onClick?.Invoke();
                    break;
                }
            }
        }
    }

}
