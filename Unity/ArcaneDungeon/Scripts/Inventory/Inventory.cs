using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public bool inventoryEnabled;
	public GameObject inventory;

	private int allSlots;
	private GameObject[] slot;

	public GameObject slotHolder;

	public GameObject ui;

	void Start()
	{
		/*allSlots = 42;
		slot = new GameObject[allSlots];
		for (int i = 0; i < allSlots; i++)
		{
			slot[i] = slotHolder.transform.GetChild(i).gameObject;

			if (slot[i].GetComponent<Slot>().item == null)
				slot[i].GetComponent<Slot>().empty = true;
		}*/
	}

	void Update()
	{
		ToggleInventory();
	}

	void ToggleInventory()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			inventoryEnabled = !inventoryEnabled;
			if (inventoryEnabled == true)
			{
				ui.SetActive(false);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			else
			{
				ui.SetActive(true);
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
		if (inventoryEnabled == true)
		{
			inventory.SetActive(true);
		}
		else
		{
			inventory.SetActive(false);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Item")
		{
			/*GameObject itemPickedUp = other.gameObject;
			Item item = itemPickedUp.GetComponent<Item>();
			AddItem(itemPickedUp, item.ID, item.type, item.description, item.icon);*/
		}
	}

	void AddItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon)
	{
		for (int i = 0; i < allSlots; i++)
		{
			var itemSlot = slot[i].GetComponent<Slot>();
			if (itemSlot.empty)
			{
				itemObject.GetComponent<Item>().pickedUp = true;

				itemSlot.item = itemObject;
				itemSlot.icon = itemIcon;
				itemSlot.ID = itemID;

				itemObject.transform.parent = slot[i].transform;
				itemObject.SetActive(false);

				itemSlot.UpdateSlot();
				itemSlot.empty = false;
				return;
			}
		}
	}
}