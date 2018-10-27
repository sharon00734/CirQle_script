using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;  
using System.Threading.Tasks;
using System.Linq;  

public class computing : MonoBehaviour {
	//public int like;
	//public int dislike;
	public string tag;
	public string city = "kaoshiung";
	public int time;
	// Use this for initialization
	void Start () {

		int tag1_num;
		int tag2_num;
		int tag3_num;
		string tag1;
		string tag2;
		string tag3;
		var queryT = ParseObject.GetQuery ("TAG").WhereEqualTo ("TagContent", tag).WhereEqualTo ("Tag_city", city);
		
		queryT.FindAsync ().ContinueWith (t2 => 
		{
			IEnumerable<ParseObject> result2 = t2.Result;
			
			foreach (var obj in result2) {

				time = obj.Get<int> ("Tag_time");
				time = time + 1;
				Debug.Log ("資料庫TAG:" + time);  
			}
			var gameScore = new ParseObject("TAG");
			gameScore.SaveAsync().ContinueWith(t =>
			{

				gameScore["Tag_time"] = time;
				gameScore.SaveAsync();
			}); /*
			ParseObject POST = new ParseObject ("TAG");
		
			POST.SaveAsync ().ContinueWith (t =>
			{
				POST ["Tag_time"] = time;
				Debug.Log ("資料庫TAG 存入後:" + time);  
			});	  */

			var query = ParseObject.GetQuery ("city_data").WhereEqualTo ("city", city);
			
			query.FindAsync ().ContinueWith (t1 => 
			{
				IEnumerable<ParseObject> result = t1.Result;
				
				foreach (var ob in result) {
					tag1_num = ob.Get<int> ("Tag1_num");
					tag2_num = ob.Get<int> ("Tag2_num");
					tag3_num = ob.Get<int> ("Tag3_num");
					tag1 = ob ["Tag1"].ToString ();
					tag2 = ob ["Tag2"].ToString ();
					tag3 = ob ["Tag3"].ToString ();
					Debug.Log ("資料庫TAG1:" + tag1 + ",次數:" + tag1_num);  
					Debug.Log ("資料庫TAG2:" + tag2 + ",次數:" + tag2_num);  
					Debug.Log ("資料庫TAG3:" + tag3 + ",次數:" + tag3_num);  
				}
				
				
			});


		});





		/*
		if (like - dislike>=10) {
			if ((like - dislike)%10==0){
				Debug.Log("ADD!");

				float sum=-1*2;
				Debug.Log(sum);

			}else{
				Debug.Log("Don't ADD!");
			}
		
		
		}else{
			Debug.Log("Don't ADD!");
		}
		
	}*/
	}
}
