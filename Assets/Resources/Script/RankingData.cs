using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct ScoreAndName {
	public int score;
	public string name;
	public ScoreAndName(int n, string m){
		score = n;
		name = m;
	}
}

public class RankingData{

	public int tmpScore = 0;
	private static RankingData _instance;
	// シングルトン
	public static RankingData instance {
		get {
			if (_instance == null) {
				_instance = new RankingData ();
			}
			return _instance;
		}
	}
	static int CompareRanking(ScoreAndName x, ScoreAndName y){
		// Keyで比較した結果を返す
		if(x.score == y.score)return 0;
		if (x.score < y.score)return 1;
		if (x.score > y.score)return -1;
		return 0;
	}

	//SortedDictionary<int, string> dict = new SortedDictionary<int, string>();
	List<ScoreAndName> _rankingData = new List<ScoreAndName>();
	public List<ScoreAndName> rankingData{
		get{
			_rankingData.Sort (CompareRanking);//ソートしてから返す
			return _rankingData;
		}
	}

	public int AddRankingData(){return AddRankingData (tmpScore);}
	public int AddRankingData(int score){
		ScoreAndName SAN = new ScoreAndName (score, "");
		_rankingData.Add (SAN);
		_rankingData.Sort (CompareRanking);
		int k;
		for(k = 0;k<_rankingData.Count;k++){
			if(_rankingData[k].score == SAN.score){
				break;
			}
		}
		return k + 1;
	}
	public void test(){
		_rankingData.Add (new ScoreAndName(10,"hiraoka"));
		_rankingData.Add (new ScoreAndName(12,"takasi"));
		_rankingData.Add (new ScoreAndName(5,"gonnzaresu"));
		_rankingData.Add (new ScoreAndName(8,"aitu"));
		_rankingData.Sort (CompareRanking);
		foreach(ScoreAndName n in _rankingData){
			Debug.Log (n.score +" : "+n.name);
		}
	}
}
