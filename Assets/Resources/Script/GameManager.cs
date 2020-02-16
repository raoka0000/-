using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(WeaponParam))]


public class GameManager : MonoBehaviour {
	public static GameManager instance;//シングルトンの生成.
	public int score = 0;
	public float time = 60;

	private bool gameoverFlg = false;
	[SerializeField]
	public WeaponParam weapon;

	AudioSource audioSource;


	void Awake (){
		//GameMangerインスタンスが存在したら
		if (instance != null) {
			//今回インスタンス化したAudioManagerを破棄
			Destroy(this.gameObject);
			//GameMangerインスタンスがなかったら
		} else if (instance == null){
			//このGameMangerをインスタンスとする
			instance = this;
		}
		//シーンを跨いでもAudioManagerインスタンスを破棄しない
		//DontDestroyOnLoad (this.gameObject);
	}

	// Use this fori nitialization
	void Start (){

		EnemyManager.instance.CreateEnemyRandamPosison (0);
		this.weapon = this.gameObject.GetComponent<WeaponParam>();
		this.audioSource = gameObject.GetComponent<AudioSource>();
	}

	private float timeTrigger = 0;

	// Update is called once per frame
	void Update () {
		if (gameoverFlg)return;

		this.time -= Time.deltaTime; 
		if (time <= 0)
			GameOver ();
		this.timeTrigger += Time.deltaTime;

		if(timeTrigger >1){
			int randam = Random.Range (0,100);
			randam = (randam != 0) ? 0 : 1;
			var child = EnemyManager.instance.CreateEnemyRandamPosison (randam);
			Enemy enemy = child.GetComponent<Enemy> ();
			enemy.size = enemy.Maxsize;
			enemy.age = enemy.maxAge;
			this.timeTrigger = 0;
		}
		UserInput ();
	}
	void GameOver(){
		gameoverFlg = true;
		GameObject obj = GameObject.Find("PlayGUI");
		obj.GetComponent<playGUI> ().GameOver();
		//Time.timeScale = 0;
	}

	void Hit(Collider2D aCollider2d){Hit (aCollider2d, false);}
	void Hit(Collider2D aCollider2d,bool isSlide){
		if (aCollider2d) {
			GameObject obj = aCollider2d.transform.gameObject;
			if(obj.tag == "enemy"){
				Enemy enemy = obj.GetComponent<Enemy> ();
				if (isSlide)
					enemy.HitSlide ();
				else
					enemy.Hit ();
			}
			if(obj.tag == "item"){
				DropItem item = obj.GetComponent<DropItem> ();
				item.Hit ();
			}
		}
	}
	void UserInput(){
		if (!Input.touchSupported) {
			//print("タッチ入力に対応している");
			if (Input.GetMouseButtonDown(0)) {
				Vector3 aTapPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				Collider2D aCollider2d = Physics2D.OverlapPoint (aTapPoint);
				this.Hit (aCollider2d);
			}else if(Input.GetMouseButton(0)){
				Vector3 aTapPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				Collider2D aCollider2d = Physics2D.OverlapPoint (aTapPoint);
				this.Hit (aCollider2d,true);
			}
			return;
		}
		foreach (Touch t in Input.touches){
			Vector3 aTapPoint = Camera.main.ScreenToWorldPoint(t.position);
			Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint);
			switch(t.phase){
			case TouchPhase.Began:
				this.Hit (aCollider2d);
				break;
			case TouchPhase.Moved:
				this.Hit (aCollider2d,true);
				break;
			case TouchPhase.Ended:
			case TouchPhase.Canceled:
				//Debug.LogFormat("{0}:いま離された", id);
				break;
			}
		}
	}

	public void PlaySE(AudioClip audioClip){
		audioSource.PlayOneShot( audioClip );
	}

	public int SucoreUp(int n){
		this.score += n;
		return this.score;
	}
}
