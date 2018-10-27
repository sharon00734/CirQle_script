using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class Profile : MonoBehaviour {
	public UILabel name;
	public UILabel place;
	public Texture2D img=null;
	public UITexture picture;
	// Use this for initialization
	void Start(){
		//UILabel username = name.GetComponent<UILabel> ();
		//UILabel userplace = place.GetComponent<UILabel> ();
		name.text=ParseUser.CurrentUser.Get<string>("name");
		place.text=ParseUser.CurrentUser.Get<string>("place");
		StartCoroutine ("Getimage");
		
		if (img != null)  
		{  
			picture = GetComponent<UITexture>();

		}  

	}
	public void GetProfile () {
		//UILabel username = name.GetComponent<UILabel> ();
		//UILabel userplace = place.GetComponent<UILabel> ();
		name.text=ParseUser.CurrentUser.Get<string>("name");
		place.text=ParseUser.CurrentUser.Get<string>("place");
		Debug.Log ("Get!"+ParseUser.CurrentUser.Username);
	}
	IEnumerator Getimage() {
		var query = ParseUser.Query.WhereEqualTo ("username", ParseUser.CurrentUser.Username);
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
