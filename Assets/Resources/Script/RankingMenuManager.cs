using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 



public class RankingMenuManager : MonoBehaviour {

	[SerializeField]
	RectTransform prefab = null;

	void Start () 
	{
		List<ScoreAndName> rank = RankingData.instance.rankingData;
		for(int i=0; i<rank.Count; i++)
		{
			var item = GameObject.Instantiate(prefab) as RectTransform;
			item.SetParent(transform, false);

			var text = item.GetComponentInChildren<Text>();
			var j = i + 1;
			//text.text = j.ToString() + "位 " + rank[i].name + "さん  点数 : " + rank[i].score;
			text.text = j.ToString() + "位 " + "  点数 : " + rank[i].score;
		}
	}
	public void toTitle(){
		SceneManager.LoadScene ("title");
	}
}
