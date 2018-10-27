using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;

public class q : MonoBehaviour {

	void Start () {
		int i = 0;
		Debug.Log("!!!!");

			ArrayList post_Id = new ArrayList ();
			SortedDictionary<int, string> sd = new SortedDictionary<int, string>();
			Loom.RunAsync (() => {
			var query = ParseObject.GetQuery ("POST2").WhereEqualTo ("post_type", "q").WhereEqualTo ("Location", "kaoshiung").Limit (5);
			query.FindAsync ().ContinueWith (t =>
			{
				IEnumerable<ParseObject> results = t.Result;
				
				foreach (var objs in results) {

					string id = objs.ObjectId;
					Debug.Log ("資料庫TAG:" + id);  
					post_Id.Add (id);
				
				}
				String[] postId = (String[])post_Id.ToArray (typeof(string));
				ArrayList post_score = new ArrayList ();

				for (i = 0; i < postId.Length; i++)
				{
					string happy=postId[i];
					Debug.Log(happy);
				
					var queryT = ParseObject.GetQuery ("Judge2").WhereEqualTo ("Post_Id",happy);
					var queryTask = queryT.FindAsync ().ContinueWith (t2 => {
						
						IEnumerable<ParseObject> result2 = t2.Result;

						Loom.QueueOnMainThread (() => {
							foreach (var obj in result2) {
							int like = obj.Get<int> ("Like");
							int dislike = obj.Get<int> ("DisLike"); 
							int sum = like + dislike;
							Debug.Log ("資料庫傳回:" + sum); 

							sd.Add(sum,happy);
							post_score.Add (sum);

						}
						
						foreach (KeyValuePair<int, string> item in sd)
						{
							Debug.Log("键名：" + item.Key + " 键值：" + item.Value);
						}
						});
					});
				}
			Debug.Log ("hoho"); 
		//	});
		});
		});
	}
}
