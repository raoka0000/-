using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]

public class Enemy : MonoBehaviour
{
	public int id = 0;
	// 体力.
	public int maxHp = 1;
	public int smashed = 1;
	public int age = 1;
	public int maxAge = 1;
	public float Maxsize = 1;
	public float size = 1;
	public GameObject dropItem;
	public GameObject hitEffect;
	public AudioClip audioClip;

	[SerializeField]
	private int score = 1;
	private int hp = 1;

	// Use this fori nitialization
	void Start (){
		
	}
	//アクティブ時の処理.
	void OnEnable(){
		transform.localScale = new Vector3(size,size,size);
		hp = maxHp;
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> (); 
		Color c = sr.color;
		c.a = 1.0f;
		sr.color = c;
	}

	private float timeTrigger = 0;
	// Update is called once per frame
	void Update (){
		timeTrigger += Time.deltaTime;
		float hightScale = size * (1 - Mathf.PingPong (timeTrigger, 0.12f));
		transform.localScale = new Vector3(size, hightScale,size);
	}

	protected void PuruPuru(){
		iTween.ShakeScale(this.gameObject,iTween.Hash("x",0.3f,"y",0.3f,"time",0.5f));
	}
	protected void Pan(){
		var vec = Random.insideUnitCircle;
		var power = GameManager.instance.weapon.power;
		iTween.MoveBy(this.gameObject,iTween.Hash("x",vec.x * power,"y",vec.y * power,"time",0.5f,"oncomplete", "notVisibleEnemyRelease"));
	}

	private void notVisibleEnemyRelease(){
		if (!GetComponent<Renderer>().isVisible) {
			ObjectPool.instance.ReleaseGameObject (gameObject);
		}
	}

	public virtual void Hit (){
		GameManager.instance.PlaySE (this.audioClip);
		int n = Random.Range (smashed, smashed + GameManager.instance.weapon.smash);
		hp -= 1;
		if(hp <= 0){
			Killed (n);
			return;
		}
		PuruPuru();
	}
	public virtual void HitSlide (){
		int n = Random.Range (smashed, smashed + (GameManager.instance.weapon.smash/2));
		GameManager.instance.PlaySE (this.audioClip);
		hp -= 1;
		if(hp <= 0){
			Killed (n);
			return;
		}
		PuruPuru();
	}

	protected virtual void Pop(){
		var child = EnemyManager.instance.CreateEnemy (this.id,gameObject.transform.position);
		child.SetActive (false);//todo修正.
		Enemy enemy = child.GetComponent<Enemy> ();
		enemy.id = this.id;
		enemy.size = this.size * 0.6f;
		enemy.smashed = this.smashed;
		enemy.maxHp = this.maxHp;
		enemy.maxAge = this.maxAge;
		enemy.age = this.age + 1;
		child.SetActive (true);
		enemy.Pan ();
	}

	public virtual void Killed (int n){
		if (age <= maxAge) {
			for(int i = 0; i < n; i++){
				this.Pop ();
			}
		} else {
			//EnemyManager.instance.allKill ();
		}
		iTween.ColorTo(this.gameObject,iTween.Hash("a",0.0f,"time",10,"oncomplete", "Die","delay", 10));
		if (dropItem != null) {
			int random = Random.Range (0, 100);
			if (random <= 2)Instantiate (dropItem, transform.position, Quaternion.identity);
			if (random == 99)EnemyManager.instance.CreateEnemy (1,this.gameObject.transform.position);
		}
		this.Die ();
	}

	public virtual void Die (){
		GameObject effect = Instantiate(hitEffect , this.gameObject.transform.position , Quaternion.identity) as GameObject;		// エフェクト発生
		Destroy(effect , 1.0f);
		GameManager.instance.SucoreUp (score);
		StartCoroutine("Fade");
	}
	IEnumerator Fade() {
		SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> (); 
		Color c = sr.color;
		for (float f = 1f; f >= 0; f -= 0.1f) {
			c.a = f;
			sr.color = c;
			yield return null;
		}
		ObjectPool.instance.ReleaseGameObject (gameObject);
	}

}
