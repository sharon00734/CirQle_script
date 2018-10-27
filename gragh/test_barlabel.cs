using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class test_barlabel : MonoBehaviour {
	//main.js 中cloud code 去觸動後端運算今日與昨日貼文
	public string city_ch;
	// Use this for initialization
	void Start () {
		/*ParseCloud.CallFunctionAsync<IDictionary<string, object>>("postnum", new Dictionary<string, object>()).ContinueWith(t => {
			var result = t.Result;//有可能是json關係 只有手機看的
		});
		Debug.Log("ok");*/

		//Loom.RunAsync (() => {
		String city = ParseUser.CurrentUser.Get<string>("place");
		Debug.Log ("資料庫傳回:" + city);  

	
		if (city != null) {
			if (city == "高雄市") {
				city_ch = "Kaohsiung";
			} else if (city == "台中市") {
				city_ch = "Taichung";
			} else if (city == "台北市") {
				city_ch = "Taipei";
			} else if (city == "新北市") {
				city_ch = "NewTaipei";
			} else if (city == "台南市") {
				city_ch = "Tainan";
			} else if (city == "桃園市") {
				city_ch = "Taoyuan";
			}
		}
		IDictionary<string, object> parms = new Dictionary<string, object>
		{
			{ "city", city_ch }
		};
		ParseCloud.CallFunctionAsync<IDictionary<string, object>>("postnum", parms).ContinueWith(t => {
			var score = t.Result;
			// ratings is 4.5
		}); 

		var query = ParseObject.GetQuery ("City_Post").WhereEqualTo ("City",city);
		
		query.FindAsync ().ContinueWith (t =>
		{   
			
			IEnumerable<ParseObject> post = t.Result;
			foreach (var obj in post) {
				int d_d1 = obj.Get<int> ("Angel_d1");
				int d_d2 = obj.Get<int> ("Angel_d2");
				int d_d3 = obj.Get<int> ("Angel_d3");
				int d_d4 = obj.Get<int> ("Angel_d4");
				int d_d5 = obj.Get<int> ("Angel_d5");
				//string result = obj ["Angel_d1"].ToString ();

				Debug.Log ("資料庫傳回:" + d_d1);  
				Debug.Log ("資料庫傳回:" + d_d2);  
			
				Loom.QueueOnMainThread (() => {
				WMG_Series test1 = GetComponent<WMG_Series> ();
				test1.pointValues [0] = new Vector2 (1, d_d5);
				test1.pointValues [1] = new Vector2 (2, d_d4);
				test1.pointValues [2] = new Vector2 (3, d_d3);
				test1.pointValues [3] = new Vector2 (4, d_d2); 
				test1.pointValues [4] = new Vector2 (5, d_d1);
		
				});
				
			}
			

		});
		//});
	}			
}			
	
	

