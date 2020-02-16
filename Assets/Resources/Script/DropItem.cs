using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class DropItem : MonoBehaviour {
	public enum type{
		powerUP,
		timeCountUP
	}
	public type itemType;
	private CircleCollider2D circleCollider;

	static GameObject[] infoObject;
	static AudioClip se;
	void Awake (){
		if (infoObject == null) {
			infoObject = new GameObject[2];
			infoObject [0] = (GameObject)Resources.Load ("Prehubs/ui/powerUP");
			infoObject [1] = (GameObject)Resources.Load ("Prehubs/ui/timePlus");
			se = (AudioClip)Resources.Load ("SE/cursor7");
		}
	}
	void Start () {
		this.circleCollider = gameObject.GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Hit(){
		HitEffect ();
		switch(itemType){
		case type.powerUP:
			this.PowerUp ();
			break;
		case type.timeCountUP:
			this.TimeCountUP ();
			break;
		default:
			break;
		}
	}
	IEnumerator HopUp(){
		GameObject a = DropItem.infoObject [(int)this.itemType];
		GameObject obj = (GameObject)Instantiate (a, this.transform.position, Quaternion.identity);
		obj.transform.parent = gameObject.transform;
		for (float f = 0f; f < 1.0f; f += 0.05f) {
			obj.transform.localPosition = new Vector2 (0.0f,f);
			Color c = obj.GetComponent<SpriteRenderer> ().color;
			c.a = 1 - f;
			obj.GetComponent<SpriteRenderer> ().color = c;
			yield return null;
		}
		Destroy (obj);
		Destroy (gameObject);
	}
	private void HitEffect(){
		this.circleCollider.enabled = false;
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();
		sr.enabled = false;
		GameManager.instance.PlaySE (se);
	}
	private void PowerUp(){
		GameManager.instance.weapon.smash += 1;
		StartCoroutine ("HopUp");//コルーチン起動.
	}
	private void TimeCountUP(){
		GameManager.instance.time += 5;
		StartCoroutine ("HopUp");//コルーチン起動.
	}
}
