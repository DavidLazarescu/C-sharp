using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
	//Floats
	private float sphereRadius = 2.5f;
	private float maxDistance = 0f;
	//Ints
	public int fireballDamage = 50;
	//Vectors
	private Vector3 rotation = new Vector3(-90, 0, 0);
	//References
	[SerializeField] private GameObject explosionEffect;
	private AudioManager audioManager;
	[SerializeField] private LayerMask enemyLayermask;

	private void Start()
	{
		audioManager = FindObjectOfType<AudioManager>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		//Destroys the object
		//Plays the FireballExplosionSoundEffect Sound effect
		audioManager.playSound("Fireball_Explosion_Sound_Effect", audioManager.enemySounds);
		//Explosion Effect
		GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.Euler(rotation));
		Destroy(effect, 2);
		RaycastHit[] hits = Physics.SphereCastAll(collision.collider.transform.position, sphereRadius, transform.forward, maxDistance, enemyLayermask, QueryTriggerInteraction.UseGlobal);
		foreach (var hit in hits)
		{
			if (hit.collider.CompareTag("Turtle"))
				hit.collider.gameObject.GetComponentInParent<EnemyTurtleManager>().loseHealth(fireballDamage);
			else if (hit.collider.CompareTag("SkeletonBoss"))
				hit.collider.gameObject.GetComponentInParent<EnemySkeletonBossManager>().loseHealth(fireballDamage);
		}
		Destroy(gameObject);
	}
}
