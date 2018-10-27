using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;   
using Parse;

public class AddLike_R : MonoBehaviour {
	private UILabel Label;
	private int amount;
	public string Response_Id;
	private int b;
	public GameObject red;
	// Use this for initialization
	void Start(){
		Label = GetComponentInChildren<UILabel> ();
		GetAmountofLike_R ();
		var query = ParseObject.GetQuery ("JUDGE").WhereEqualTo ("User",ParseUser.CurrentUser.Username).WhereEqualTo("Post_Id",Response_Id);
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
	}
	
	void OnClick(){
		Label = GetComponentInChildren<UILabel> ();
		var query = ParseObject.GetQuery ("JUDGE2").WhereEqualTo ("User",ParseUser.CurrentUser.Username).WhereEqualTo("Response_Id",Response_Id);
		query.FirstAsync ().ContinueWith (t =>
		                                  {
			if (t.IsFaulted || t.IsCanceled) {
				b=0;
				//Debug.Log ("Like 0!");
			} 
			else {
				ParseObject result = t.Result;
				string str = result.Get<string>("Judge_type");
				
				Debug.Log(str);
				if(str == "Like"){
					b=1;
					//Debug.Log ("Like 1!");
				}
				if(str=="DisLike"){
					b=-1;
					//Debug.Log ("Like -1!");
				}
			}
		});

		amount = int.Parse (Label.text);
	
		Label.text = amount.ToString ();
		if (b == 1) {
			int i = int.Parse (Label.text);
			i--;
			Label.text = i.ToString ();
			AmountMinus_R();
		}
		else if (b == 0) {
			int i = int.Parse (Label.text);
			i++;
			Label.text = i.ToString ();
			UpdateAmount_R ();
		}
		else if (b == -1) {
			Debug.Log("No You Can't");
		}

	}
	
	void GetAmountofLike_R(){
		Label = GetComponentInChildren<UILabel> ();
		var query = ParseObject.GetQuery("RESPONSE").WhereEqualTo("objectId",Response_Id);		
		query.FindAsync ().ContinueWith (t =>
		                                 {
			IEnumerable<ParseObject> result=t.Result;
			foreach(var obj in result){
				Label.text=obj["Like"].ToString();
			}
			
			//Label.text=count.ToString();	
		});

	}
	
	void UpdateAmount_R(){
		red.SetActive (true);
		Label = GetComponentInChildren<UILabel> ();
		ParseObject JUDGE2 = new ParseObject("JUDGE2");
		JUDGE2 ["Response_Id"] = Response_Id.ToString();
		JUDGE2 ["Judge_type"] = "Like";
		JUDGE2 ["User"] = ParseUser.CurrentUser.Username;
		JUDGE2.SaveAsync ();
		amount++;
		var query = ParseObject.GetQuery("RESPONSE").WhereEqualTo("objectId",Response_Id);
		query.FindAsync ().ContinueWith (t =>
		                                 {
			IEnumerable<ParseObject> result=t.Result;
			foreach(var obj in result){
				string str=amount.ToString();
				obj["Like"]=str;
				Label.text=str;	
				obj.SaveAsync();
			}			
			//Label.text=count.ToString();	
		});
		Debug.Log("AddLike!");
	}

	void AmountMinus_R(){
		red.SetActive (false);
	//	Label = GetComponentInChildren<UILabel> ();
		amount--;
		//Debug.Log(amount);
		string str=amount.ToString();
		Label.text=str;	
		
		var query2 = ParseObject.GetQuery("JUDGE2").WhereEqualTo ("User",ParseUser.CurrentUser.Username).WhereEqualTo("Response_Id",Response_Id);
		query2.FirstAsync ().ContinueWith (t2 =>
		                                   {
			ParseObject obj2=t2.Result;
			obj2.DeleteAsync();
		});
		var query = ParseObject.GetQuery("RESPONSE").WhereEqualTo("objectId",Response_Id);
		query.FindAsync ().ContinueWith (t =>
		                                 {
			IEnumerable<ParseObject> result=t.Result;
			foreach(var obj in result){
				string str2=amount.ToString();
				obj["Like"]=str;
				obj.SaveAsync();
			}			
			//Label.text=count.ToString();	
		});

		b = 0;
		Debug.Log("Minus!");
	}
}
