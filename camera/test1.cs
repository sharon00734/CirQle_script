using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class test1 : MonoBehaviour {
	public Texture2D img=null;
	public UITexture picture;
	public string postid;
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
		var query = ParseObject.GetQuery ("POST").WhereEqualTo("objectId",postid).Limit(1);
		//"vRRpJdZYSF"
		var queryTask = query.FindAsync();
		while (!queryTask.IsCompleted) yield return null;
		
		IEnumerable<ParseObject> results= queryTask.Result;
		//ParseObject obj= queryTask.Result;
		foreach (var obj in results) {
			var imagefile = obj.Get<ParseFile> ("file");
			var imageRequest = new WWW (imagefile.Url.AbsoluteUri);
			yield return imageRequest;
			img= imageRequest.texture;
			picture.mainTexture = img;
			//string resumeText = resumeTextRequest.text;
			
			//testPlane.GetComponent<Renderer>().material.mainTexture = imageRequest.texture;
			
		}
		
	}
}
