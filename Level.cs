using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;
public class Level : MonoBehaviour {
	private int[] ScoreArray=new int[]{0,100,200,300,400,500,600,700,800,900,1000,1150,1300,1450,1600,1750,1900,2050,2200,2350,2500,2700,2900,3100,3300,3500,3700,3900,4100,4300,4500,4501};
	public string City;
	public UILabel level;

	private string score;
	// Use this for initialization
	void Start () {
		GetLevel (City);
	}

	public void GetLevel(string city){

		var query = ParseObject.GetQuery("Location").WhereEqualTo("City",city);
		query.FirstAsync ().ContinueWith (t =>
		                                  {
			ParseObject obj=t.Result;
			score = obj.Get<string>("Score");

			Debug.Log("score:"+score);
			for (int i=0; i<ScoreArray.Length; i++) {
				if(int.Parse(score) >= ScoreArray[i] && int.Parse (score) < ScoreArray[i+1]){
					level.text=i.ToString();
				}
			}
		});
	}

}
