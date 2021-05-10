using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
	public GameObject item;
	public int ID;
	public bool empty;

	public Transform slotIconGO;
	public Sprite icon;

	public void OnPointerClick(PointerEventData pointerEventData)
	{
		UseItem();
	}

	public void Start()
	{
		slotIconGO = transform.GetChild(0);
	}

	public void UpdateSlot()
	{
		slotIconGO.gameObject.AddComponent<Image>();
		slotIconGO.GetComponent<Image>().sprite = icon;
	}

	public void UseItem()
	{
		item.GetComponent<Item>().ItemUsage();
	}

}
