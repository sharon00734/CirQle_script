using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;

public class change_new_city : MonoBehaviour {
	public string City;
	public string tag1;
	public string tag2;
	public string tag3;
	public string exist;
	public GameObject dark; 
	void OnClick(){
		GameObject []items =  GameObject.FindGameObjectsWithTag("buildings");
		for (var i = 0; i < items.Length; i++) {			
			Destroy(items[i]);			
		}
		GameObject [] buttons = GameObject.FindGameObjectsWithTag("cloud");
		for (var j = 0; j < buttons.Length; j++) {			
			buttons[j].SetActive(false);			
		}
		dark.SetActive (true);
		ScorePrinciple iniobj = GameObject.Find ("background_main").GetComponent<ScorePrinciple> ();
		Debug.Log (iniobj);
		iniobj.City = City;
		iniobj.Rank (City);
		RingValue value= GameObject.Find ("radar").GetComponent<RingValue> ();
		value.City = City;
		value.GetValue (City);

		Level level = GameObject.Find ("ScoreLevel").GetComponent<Level> ();
		Debug.Log ("level:" + level);
		level.City = City;
		level.GetLevel (City);

		if (exist != null) {

			//hottag city =  GameObject.Find ("HotTag").GetComponent<hottag> ();
			//city.city=City;//tag 要隨地區變化

			q_ca q_city = GameObject.Find ("Q_button").GetComponent<q_ca> ();
			q_city.city = City;
			q_city.exist = exist;
			
			kao_angel angel_city = GameObject.Find ("angel_button").GetComponent<kao_angel> ();
			angel_city.city = City;
			angel_city.exist = exist;
			
			kao_devil devil_city = GameObject.Find ("devil_button").GetComponent<kao_devil> ();
			devil_city.city = City;
			devil_city.exist = exist;
			
			allpost all_city = GameObject.Find ("all_button").GetComponent<allpost> ();
			all_city.city = City;
			all_city.exist = exist;
			/*
			kao_q_pop pop_city = GameObject.Find ("hot").GetComponent<kao_q_pop> ();
			pop_city.city = City;
			pop_city.exist = exist;*/
		}

		var query = ParseObject.GetQuery("City_Data").WhereEqualTo("City",City);
		query.FindAsync().ContinueWith(t =>
		                               {
			IEnumerable<ParseObject> result3 = t.Result;
			foreach (var ob in result3) {
				
				tag1 = ob ["Tag1"].ToString ();
				tag2 = ob ["Tag2"].ToString ();
				tag3 = ob ["Tag3"].ToString ();
				
				Debug.Log ("資料庫" + tag1);  
				Debug.Log ("資料庫" + tag2);  
				Debug.Log ("資料庫" + tag3);  
			}
			Loom.QueueOnMainThread(()=>
			                       {
				UILabel label1 = GameObject.Find("HotTag/tag1").GetComponent<UILabel>();
				label1.text = tag1;
				UILabel label2 = GameObject.Find("HotTag/tag2").GetComponent<UILabel>();
				label2.text = tag2;
				UILabel label3 = GameObject.Find("HotTag/tag3").GetComponent<UILabel>();
				label3.text = tag3;
			});	
		});
		
	}
}
