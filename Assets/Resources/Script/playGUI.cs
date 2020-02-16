using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 



public class playGUI : MonoBehaviour {
	private float time; 
	private int score; 
	public GameObject timeTextObject;
	public GameObject scoreTextObject;
	private Text timeText;
	private Text scoreText;

	public GameObject gameover;
	// Use this for initialization
	void Start () {
		timeText = timeTextObject.GetComponent<Text> ();
		scoreText = scoreTextObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		this.time = GameManager.instance.time;
		this.score = GameManager.instance.score;
		timeText.text = ((int)time).ToString();
		scoreText.text = "score :" + score.ToString ();
	}

	public void GameOver(){
		gameover.SetActive (true);
	}
	public void toResult(){
		RankingData.instance.tmpScore = score;
		SceneManager.LoadScene ("result");
	}
}
