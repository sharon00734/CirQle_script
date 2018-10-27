using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;

public class ScorePrinciple : MonoBehaviour {
	private int[,] ScoreArray=new int[,]{{0,100,200,300,400,500},{500,600,700,800,900,1000},{1000,1150,1300,1450,1600,1750},{1750,1900,2050,2200,2350,2500},{2500,2700,2900,3100,3300,3500},{3500,3700,3900,4100,4300,4500}};
	private string score;
	public string City;
	// Use this for initialization
	void Start(){
		Rank (City);
	}

	public void Rank (string city) {
		Loom.RunAsync (() => {
			var query = ParseObject.GetQuery("Location").WhereEqualTo("City",city);
			query.FindAsync ().ContinueWith (t =>
			{
				Debug.Log("here");
				Loom.QueueOnMainThread (() => {
					IEnumerable<ParseObject> result = t.Result;
					foreach (var obj in result) {
						score = obj ["Score"].ToString ();
					}
					for(int j=0; j<6; j++){
						for (int i=0; i<6; i++) {

							Debug.Log("i:"+i);
							if ( i<5 && int.Parse (score) >= ScoreArray [j,i] && int.Parse (score) < ScoreArray [j,i + 1] ) {
								//GameObject o = (GameObject)Instantiate (Resources.Load ("level_0"));

								InstantiateObj(city,j,i);

															
							}
							if( i==5 && int.Parse (score) == ScoreArray [j,5]){
							
								InstantiateObj(city,j,i);
							}
						}
					}

				});
			});
		});
	}

	void InstantiateObj(string city,int j,int i){
		if (j <= 4) {
			if (i > 0) {
				UISprite ball = GameObject.Find ("Ball").GetComponent<UISprite> ();
				string str = "Level/level_" + j.ToString ();
				
				UIAtlas a = Resources.Load (str, typeof(UIAtlas)) as UIAtlas;
				Debug.Log (a);
				ball.atlas = a.GetComponent<UIAtlas> ();
				ball.spriteName = j.ToString () + "_0";		
				/*int t = i*j;
				UILabel label = GameObject.Find ("Level").GetComponent<UILabel> ();
				label.text=t.ToString();*/

				for (int count=1; count<=i; count++) {
					string str_o = "Level/" + j.ToString () + "_" + count.ToString ();
					Debug.Log (str_o);
					GameObject o = (GameObject)Instantiate (Resources.Load (str_o));
					Debug.Log (o);
					o.transform.parent = GameObject.Find ("background_main").transform;
					o.transform.localScale = new Vector3 (1, 1, 1);
				}
			}
		}

		if (j > 4) {
			if (i > 0) {
				UISprite ball = GameObject.Find ("Ball").GetComponent<UISprite> ();
				string str = "Level/" + city + "/level_" + j.ToString ()+"_"+city;

				UIAtlas a = Resources.Load (str, typeof(UIAtlas)) as UIAtlas;
				Debug.Log (a);
				ball.atlas = a.GetComponent<UIAtlas> ();
				ball.spriteName = j.ToString () + "_0";
				/*int t = (i+1)*j;
				UILabel label = GameObject.Find ("Level").GetComponent<UILabel> ();
				label.text=t.ToString();*/
				for (int count=1; count<=i; count++) {
					string str_o = "Level/" + city + "/" + j.ToString () + "_" + count.ToString ();
					Debug.Log (str_o);
					GameObject o = (GameObject)Instantiate (Resources.Load (str_o));
					o.transform.parent = GameObject.Find ("background_main").transform;
					o.transform.localScale = new Vector3 (1, 1, 1);
				}
			}
		}


	}

}
