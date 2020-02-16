using UnityEngine;
using System.Collections;

public class wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerExit2D (Collider2D c){
		GameObject obj = c.gameObject;
		if(obj.tag == "enemy"){
			Enemy enemy = obj.GetComponent<Enemy> ();
			enemy.Killed(0);
		}
	}
}
