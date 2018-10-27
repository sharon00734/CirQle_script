using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class Getuserphoto : MonoBehaviour {
	public string UserAccount;
	public Texture2D img=null;
	public UITexture picture;
	public string postid;
	// Use this for initialization
	void Start(){
		StartCoroutine ("Getimage");
		
		if (img != null)  
		{  
			picture = GetComponent<UITexture>();
			//photo_now = img;
			//gameObject.GetComponent<UITexture>().mainTexture = te;
			
			
			//GUI.DrawTexture(new Rect(0, 20, 200, 200), img);  
		}  


	}
	IEnumerator Getimage() {
		var query = ParseUser.Query.WhereEqualTo ("username", UserAccount);
		var queryTask = query.FindAsync ();
		while (!queryTask.IsCompleted)
			yield return null;		
				
		IEnumerable<ParseUser> result = queryTask.Result;
				


		foreach (var obj in result) {
			var imagefile = obj.Get<ParseFile> ("file");
			var imageRequest = new WWW (imagefile.Url.AbsoluteUri);
			yield return imageRequest;
			img = imageRequest.texture;
			picture.mainTexture = img;
		}
	}
}
