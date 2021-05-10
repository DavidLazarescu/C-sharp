using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public int ID;
	public string type;
	public string description;
	public Sprite icon;
	public bool pickedUp;
	public bool playersWeapon;
	[HideInInspector] public bool equipped;
	[HideInInspector] public GameObject weapon;
	[HideInInspector] public GameObject weaponManager;

	private void Start()
	{
		weaponManager = GameObject.FindWithTag("Weapon_Manager");

		if (!playersWeapon)
		{
			int allWeapons = weaponManager.transform.childCount;
			for (int i = 0; i < allWeapons; i++)
			{
				if (weaponManager.transform.GetChild(i).gameObject.GetComponent<Item>().ID == ID)
				{
					weapon = weaponManager.transform.GetChild(i).gameObject;
				}
			}
		}
	}

	void Update()
	{
		
	}

	private void dropItem()
    {
		if (equipped)
		{
			if (Input.GetKeyDown(KeyCode.Q)) {
				equipped = false;
				this.gameObject.SetActive(false);
			}
		}
	}

	public void ItemUsage()
	{
		//Wepaon
		if (type == "Weapon")
		{
			weapon.SetActive(true);
			weapon.GetComponent<Item>().equipped = true;
		}
		//Health Item
	}
}
