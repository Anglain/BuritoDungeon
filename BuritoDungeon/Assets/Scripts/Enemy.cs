using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Настя супер, но это не точно. Максик тычка.А я - баклажан.
/// </summary>

public class Enemy : MonoBehaviour {

	private Animator animator;

	[System.Serializable]
	public class EnemyStats {
		public float health = 100;
	}

	void Awake () {
		animator = GetComponent<Animator> ();
	}

	public EnemyStats stats = new EnemyStats();

	public void DamageEnemy (float damage) {
		stats.health -= damage;

		//if (stats.health <= 0)
			//GameMaster.KillEnemy (this);
	}
}
