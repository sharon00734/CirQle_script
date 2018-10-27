using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;

public class hottag : MonoBehaviour {
	public string tag1;
	public string tag2;
	public string tag3;
	public string city;
	// Use this for initialization
	void Start () {
		/*
		UILabel label = GameObject.Find("city_now").GetComponent<UILabel>();
		city = label.text;
		Debug.Log(city);
*/
		var query = ParseObject.GetQuery("City_Data").WhereEqualTo("City",city);
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
