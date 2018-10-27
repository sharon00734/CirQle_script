using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;  
using System.Threading.Tasks;
using System.Linq;  

public class ShowTags : MonoBehaviour {
	public UILabel[] TagText=new UILabel[3];
	//public GameObject Label_Tags;
	private string[] Tstr = new string[3];
	// Use this for initialization
	void Start (){
		StartCoroutine(GetTags());
	}
	
	IEnumerator GetTags () {
		GameObject g = GameObject.Find("Panel").transform.FindChild("CirQle_slide").gameObject;
		Debug.Log (g);
		submit GetTagID = g.GetComponent<submit> ();

		var query = ParseObject.GetQuery("TAG");

		query.GetAsync(GetTagID.TagID[0]).ContinueWith(t =>
			                                               {
			Loom.QueueOnMainThread(()=>{
				if (t.IsCanceled || t.IsFaulted) {

					Debug.Log ("NONON.");
						
				} else {
					ParseObject tags = t.Result;
						
					Tstr[0] = tags.Get<string>("TagContent");
					TagText[0].text=Tstr[0];
					Debug.Log("在這裡"+Tstr[0]);
				}
			});
			
		});
		query.GetAsync(GetTagID.TagID[1]).ContinueWith(t =>
		                                               {
			Loom.QueueOnMainThread(()=>{
				if (t.IsCanceled || t.IsFaulted) {

					Debug.Log ("NONON.");
					
				} else {
					ParseObject tags = t.Result;
					
					Tstr[1] = tags.Get<string>("TagContent");
					TagText[1].text=Tstr[1];
					Debug.Log("在這裡"+Tstr[1]);
				}
			});
			
		});
		query.GetAsync(GetTagID.TagID[2]).ContinueWith(t =>
		                                               {
			Loom.QueueOnMainThread(()=>{
				if (t.IsCanceled || t.IsFaulted) {

					Debug.Log ("NONON.");
					
				} else {
					ParseObject tags = t.Result;
					
					Tstr[2] = tags.Get<string>("TagContent");
					TagText[2].text=Tstr[2];
					Debug.Log("在這裡"+Tstr[2]);
				}
			});
		});

		//string ShowTag = "#" + str[0].ToString() + " " + "#" + str[1].ToString() + " " + "#" + str[2].ToString();

		yield return null;
		/*IEnumerable<ParseObject> results= queryTask.Result;
		Debug.Log(results);
		foreach (var obj in results) {
			Debug.Log("hey");
			string tagcontent = obj["TagContent"].ToString();
			PostTag =tagcontent.ToString();
			Debug.Log(PostTag);
			postlabelvalue = Label_Tags.GetComponent<UILabel> ();
			postlabelvalue.text = PostTag.ToString ();
		} */
		
	}
}
