using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyManager : MonoBehaviour {
	public static EnemyManager instance;//シングルトンの生成.
	public GameObject[] prefabs;//todo修正.

	void Awake (){
		//GameMangerインスタンスが存在したら
		if (instance != null) {
			//今回インスタンス化したEnemyManagerを破棄
			Destroy(this.gameObject);
			//EnemyManagerインスタンスがなかったら
		} else if (instance == null){
			//このEnemyManagerをインスタンスとする
			instance = this;
		}
		//シーンを跨いでもAudioManagerインスタンスを破棄しない
		//DontDestroyOnLoad (this.gameObject);
	}
	// Use this for initialization

	public GameObject CreateEnemyRandamPosison(int id){
		return CreateEnemyRandamPosison (prefabs [id]);
	}
	public GameObject CreateEnemyRandamPosison(GameObject obj){
		Vector2 posion = new Vector2 (Random.Range (-2.0f, 2.0f), Random.Range (-4.0f, 4.0f));
		return CreateEnemy(obj,posion);
	}
	public GameObject CreateEnemy(int id,Vector2 posion){
		return CreateEnemy(prefabs [id],posion);
	}
	public GameObject CreateEnemy(GameObject obj,Vector2 posion){
		var go = ObjectPool.instance.GetGameObject (obj, posion, Quaternion.identity);
		return go;
	}

	public void allKill(){
		Dictionary<int, List<GameObject>> pool = ObjectPool.instance.pooledGameObjects;
		foreach(List<GameObject> list in pool.Values){
			foreach (GameObject obj in list) {
				if(obj.activeSelf && obj.tag == "enemy"){
					Enemy enemy = obj.GetComponent<Enemy> ();
					enemy.Die ();
				}
			}
		}
	}
}
