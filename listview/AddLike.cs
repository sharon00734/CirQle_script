using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;  
using System.Linq;  
using Parse;

public class AddLike : MonoBehaviour {	
	private UILabel Label;
	private int amount;
	public string Post_Id;
	public int b=5;
	public GameObject red;
	// Use this for initialization
	void Start(){
		//Resources.UnloadUnusedAssets();
		GetAmountofLike ();		
		var query = ParseObject.GetQuery ("JUDGE").WhereEqualTo ("User",ParseUser.CurrentUser.Username).WhereEqualTo("Post_Id",Post_Id);
		query.FirstAsync ().ContinueWith (t =>
		                                  {
			Loom.QueueOnMainThread(()=>{
				
				if (t.IsFaulted || t.IsCanceled) {
					b=0;
					Debug.Log ("Like 0!");
				} 
				else {
					ParseObject result = t.Result;
					string str = result.Get<string>("Judge_type");
					
					Debug.Log(str);
					if(str == "Like"){
						b=1;
						red.SetActive(true);
						Debug.Log ("Like 1!");
					}
					if(str=="DisLike"){
						b=-1;
						Debug.Log ("Like -1!");
					}
				}

			});
		});
		Debug.Log ("b:"+b);
	}
	//1為已經按過Like(true)
	//0為還沒按過Like(false)
	//-1已按過DisLike

	void OnClick(){
		Resources.UnloadUnusedAssets();
		Label = GetComponentInChildren<UILabel> ();

		var query = ParseObject.GetQuery ("JUDGE").WhereEqualTo ("User",ParseUser.CurrentUser.Username).WhereEqualTo("Post_Id",Post_Id);
		query.FirstAsync ().ContinueWith (t =>
		                                  {
			Loom.QueueOnMainThread(()=>{

				if (t.IsFaulted || t.IsCanceled) {
					b=0;
					Debug.Log ("Like 0!");
				} 
				else {
					ParseObject result = t.Result;
					string str = result.Get<string>("Judge_type");
					
					Debug.Log(str);
					if(str == "Like"){
						b=1;
						Debug.Log ("Like 1!");
					}
					if(str=="DisLike"){
						b=-1;
						Debug.Log ("Like -1!");
					}
				}

				amount = int.Parse (Label.text);
				Debug.Log ("amount:"+amount);
			
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

	void GetAmountofLike(){
		Label = GetComponentInChildren<UILabel> ();
		var query = ParseObject.GetQuery("POST");

		query.GetAsync (Post_Id).ContinueWith (t => {
			ParseObject result = t.Result;
			
			foreach (var obj in result) {
				string str=result.Get<string>("Like");
				Label.text=str;	
			}
		});
		//while (!queryTask.IsCompleted) yield return null;


	}

	void UpdateAmount(){
		red.SetActive (true);
		Label = GetComponentInChildren<UILabel> ();
		ParseObject JUDGE = new ParseObject("JUDGE");
		JUDGE ["Post_Id"] = Post_Id.ToString();
		JUDGE ["Judge_type"] = "Like";
		JUDGE ["User"] = ParseUser.CurrentUser.Username;
		JUDGE.SaveAsync ();
		Debug.Log("AddLike!");

		var query = ParseObject.GetQuery("POST").WhereEqualTo("objectId",Post_Id);

		query.FindAsync ().ContinueWith (t => {
			IEnumerable<ParseObject> result = t.Result;
			amount++;
			Debug.Log(amount);
			
			foreach (var obj in result) {
				string str=amount.ToString();
				obj["Like"] = str;
				int i = obj.Get<int>("Sum");
				i++;
				obj["Sum"]=i;
				obj.SaveAsync();
				Label.text=str;	
			}
		});
		IDictionary<string, object> parms = new Dictionary<string, object>
		{
			{ "score", Post_Id }
		};
		ParseCloud.CallFunctionAsync<IDictionary<string, object>>("score_computing_add", parms).ContinueWith(t2 => {
			var score = t2.Result;
		}); 
		//while (!queryTask.IsCompleted) yield return null;


	

		b = 1;
		Debug.Log("Update!");
	}

	void AmountMinus(){
		red.SetActive (false);
		Label = GetComponentInChildren<UILabel> ();
		amount--;
		Debug.Log(amount);
		string str=amount.ToString();
		
		var query = ParseObject.GetQuery("POST").WhereEqualTo("objectId",Post_Id);
		
		query.FindAsync ().ContinueWith (t => {
			IEnumerable<ParseObject> result = t.Result;
			foreach (var obj in result){
				obj["Like"]=str;
				int i = obj.Get<int>("Sum");
				i--;
				obj["Sum"]=i;
				obj.SaveAsync();
				Label.text=str;	
				/*foreach (var ob in obj) {
					string str=amount.ToString();
					obj["Like"] = str;
					obj.SaveAsync();
					Label.text=str;	
				}*/
			}
		});
				IDictionary<string, object> parms = new Dictionary<string, object>
				{
					{ "score", Post_Id }
				};
				ParseCloud.CallFunctionAsync<IDictionary<string, object>>("score_computing_addquit", parms).ContinueWith(t3 => {
					var score = t3.Result;
				});
		
	
		var query2 = ParseObject.GetQuery("JUDGE").WhereEqualTo ("User",ParseUser.CurrentUser.Username).WhereEqualTo("Post_Id",Post_Id);
		query2.FirstAsync ().ContinueWith (t2 =>
		{
			ParseObject obj2=t2.Result;
			obj2.DeleteAsync();
		});
		/*IEnumerable<ParseObject> result2 = query2.Result;
		foreach (ParseObject obj2 in result2) {
			obj2.DeleteAsync();
			Debug.Log(obj2.ObjectId);
		}	*/
		b = 0;
		Debug.Log("Minus!");
	}
}