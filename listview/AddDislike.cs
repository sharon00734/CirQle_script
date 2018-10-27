using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;  
using System.Linq;  


public class AddDislike : MonoBehaviour {
	private UILabel Label;
	private int amount;
	public string Post_Id;
	public int b=5;
	public GameObject red;
	// Use this for initialization
	void Start(){
		//Resources.UnloadUnusedAssets();

		GetAmountofDisLike ();
		var query1 = ParseObject.GetQuery ("JUDGE").WhereEqualTo ("User",ParseUser.CurrentUser.Username).WhereEqualTo("Post_Id",Post_Id);
		query1.FirstAsync().ContinueWith (t =>
		                                  {
			
			//Loom.QueueOnMainThread(()=>{
			if (t.IsFaulted || t.IsCanceled) {
				b = 0;
				//Debug.Log ("DisLike 0!");
			} else {
				ParseObject result = t.Result;
				string str = result.Get<string> ("Judge_type");
				Debug.Log (str);
				if (str == "DisLike") {
					b = 1;
					red.SetActive(true);
					Debug.Log ("DisLike 1!");
				}
				if (str == "Like") {
					b = -1;
					Debug.Log ("DisLike -1!");
				}
			}

		});
	}

	//1為已經按過DisLike(true)
	//0為還沒按過DisLike(false)
	//-1已按過Like
	
	void OnClick(){
		var query1 = ParseObject.GetQuery ("JUDGE").WhereEqualTo ("User",ParseUser.CurrentUser.Username).WhereEqualTo("Post_Id",Post_Id);
		query1.FirstAsync().ContinueWith (t =>
		                                   {
				if (t.IsFaulted || t.IsCanceled) {
					b = 0;
					//Debug.Log ("DisLike 0!");
				} else {
					ParseObject result = t.Result;
					string str = result.Get<string> ("Judge_type");
					Debug.Log (str);
					if (str == "DisLike") {
						b = 1;
						Debug.Log ("DisLike 1!");
					}
					if (str == "Like") {
						b = -1;
						Debug.Log ("DisLike -1!");
					}
				}
				amount = int.Parse (Label.text);
				Debug.Log ("amount:"+amount);
			Loom.QueueOnMainThread(()=>{
				if (b == 1) {
					Debug.Log ("b:"+b);
					int i = int.Parse (Label.text);
					i--;
					Label.text = i.ToString ();
					AmountMinus();
				}
				else if (b == 0) {
					Debug.Log ("b:"+b);
					int i = int.Parse (Label.text);
					i++;
					Label.text = i.ToString ();
					UpdateAmount();
				}
				else if(b == -1) {
					Debug.Log ("b:"+b);
					Debug.Log("No You Can't");
				}
			});
		});


	}

	void GetAmountofDisLike(){		
		Label = GetComponentInChildren<UILabel> ();		
		var query = ParseObject.GetQuery("POST").WhereEqualTo("objectId",Post_Id);
		
		query.FindAsync ().ContinueWith (t => {
			IEnumerable<ParseObject> result2 = t.Result;
			
			foreach (var obj in result2) {
				string str=obj["DisLike"].ToString();
				Label.text=str;	
			}
		});
		//while (!queryTask.IsCompleted) yield return null;


	}
	
	void UpdateAmount(){
		red.SetActive(true);
		Label = GetComponentInChildren<UILabel> ();
		ParseObject JUDGE = new ParseObject("JUDGE");
		JUDGE ["Post_Id"] = Post_Id.ToString();
		JUDGE ["Judge_type"] = "DisLike";
		JUDGE ["User"] = ParseUser.CurrentUser.Username;
		JUDGE.SaveAsync ();
		Debug.Log("AddDislike!");

		var query = ParseObject.GetQuery("POST").WhereEqualTo("objectId",Post_Id);
		
		query.FindAsync ().ContinueWith (t =>
		{
			IEnumerable<ParseObject> result = t.Result;
			amount++;
			Debug.Log(amount);
			
			foreach (var obj in result) {
				string str=amount.ToString();
				obj["DisLike"] = str;
				int i = obj.Get<int>("Sum");
				i++;
				obj["Sum"]=i;
				obj.SaveAsync();
				Label.text=str;	
			}

			b = 1;
			Debug.Log("Update!");
		});
		IDictionary<string, object> parms = new Dictionary<string, object>
		{
			{ "score", Post_Id }
		};
		ParseCloud.CallFunctionAsync<IDictionary<string, object>>("score_computing_addquit", parms).ContinueWith(t3 => {
			var score = t3.Result;
		});
		//while (!queryTask.IsCompleted) yield return null;


		
	}
	void AmountMinus(){
		red.SetActive(false);
		Label = GetComponentInChildren<UILabel> ();
		amount--;
		Debug.Log(amount);
		string str=amount.ToString();
				
		var query = ParseObject.GetQuery("POST").WhereEqualTo("objectId",Post_Id);
		
		query.FirstAsync().ContinueWith(t=>
		 {

				
				ParseObject obj = t.Result;
				obj["DisLike"]=str;
				int i = obj.Get<int>("Sum");
				i--;
				obj["Sum"]=i;
				obj.SaveAsync();
				Label.text=str;	
		
		});
		IDictionary<string, object> parms = new Dictionary<string, object>
		{
			{ "score", Post_Id }
		};
		ParseCloud.CallFunctionAsync<IDictionary<string, object>>("score_computing_add", parms).ContinueWith(t => {
			var score = t.Result;
		}); 
		var query2 = ParseObject.GetQuery("JUDGE").WhereEqualTo ("User",ParseUser.CurrentUser.Username).WhereEqualTo("Post_Id",Post_Id);
		query2.FirstAsync ().ContinueWith (t2 =>
		                                   {
			ParseObject obj2=t2.Result;
			obj2.DeleteAsync();
		});	
		b = 0;
		Debug.Log("Minus!");
	}
}
