using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 


public class ResultManager : MonoBehaviour {
	private int sequence = 0;
	public GameObject[] ui;
	public AudioClip auC;
	AudioSource audioSource;

	void Start(){
		//RankingData.instance.test ();
		this.audioSource = gameObject.GetComponent<AudioSource>();
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			go ();
		}
	}
	void go(){
		switch (sequence) {
		case 0:
		case 1:
			ui [sequence].SetActive (true);
			break;
		case 2:
			ui [sequence].GetComponent<Text> ().text = RankingData.instance.tmpScore.ToString ();
			ui [sequence].SetActive (true);
			break;
		case 3:
			ui [sequence].SetActive (true);
			break;
		case 4:
			ui [sequence].GetComponent<Text> ().text = RankingData.instance.AddRankingData ().ToString () + "位";
			ui [sequence].SetActive (true);
			break;
		case 5:
			ui [sequence].SetActive (true);
			break;
		}
		sequence++;
		audioSource.PlayOneShot(auC);
	}
	public void toTitle(){
		SceneManager.LoadScene ("title");
	}
}
