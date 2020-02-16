using UnityEngine;
using System.Collections;

public class E_Puni : Enemy {
	public AudioClip ac;
	public override void Killed (int n){
		EnemyManager.instance.allKill ();
		GameManager.instance.PlaySE (ac);
	}
}
