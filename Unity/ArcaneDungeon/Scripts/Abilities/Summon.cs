using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
	[SerializeField] private GameObject projectile;
	private AudioManager audioManager;
	[SerializeField] private PlayerManager playerManager;

	private void Awake()
	{
		audioManager = FindObjectOfType<AudioManager>();
	}

	public void summonFireball(int manaCost)
	{
		if (playerManager.currentPlayerMana >= manaCost)
		{
			audioManager.playSound("Fireball_Sound_Effect", audioManager.abilitySounds);	//Plays the sound FireballSoundEffect
			playerManager.loseMana(manaCost);
			GameObject fireball = Instantiate(projectile, transform.position, Quaternion.identity); //Creates an Gameobject for the fireball
			Rigidbody rb = fireball.GetComponent<Rigidbody>();	//Acces the Rigidbody from the fireball Prefab
			rb.velocity = transform.forward * 20;				//Add Force and velocity to the fireball
			Destroy(fireball, 5);
		}
	}
}
