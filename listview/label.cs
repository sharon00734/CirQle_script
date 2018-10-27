using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class label : MonoBehaviour {
	private GameObject Label_post;
	public UIScrollView scrollview;
	int i = 0;
	//string[] labeltext = new string[15];
	List<ParseObject> post;
	private List<string> label_list=null;
	private int limit=15;
	// Use this for initialization

	void Start () {
		StartCoroutine("Getlabel");
	
	
	}
	
	// Update is called once per frame
	IEnumerator Getlabel() {

		ArrayList label_list = new ArrayList();
		ParseQuery<ParseObject> query = new ParseQuery<ParseObject> ("POST").OrderByDescending ("createdAt");
		query = query.Limit(limit);
		var queryTask = query.FindAsync();
		while (!queryTask.IsCompleted) yield return null;
	
		IEnumerable<ParseObject> post= queryTask.Result;
		label_list.Clear ();
		foreach (var obj in post) {
			string text = obj ["postfield"].ToString ();
			Debug.Log ("資料庫傳回:" + text);  
			string o= "Q_list" +i;
			Label_post=GameObject.Find (o);

			UILabel label = Label_post.GetComponentInChildren<UILabel> ();
		
			label.text = text;

			i++;
			label_list.Add (text);
				
		}

	

	}
}

