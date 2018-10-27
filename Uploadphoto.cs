using UnityEngine;
using System.Collections;
using Parse;
using System.Collections.Generic;
public class Uploadphoto : MonoBehaviour {
	public Texture2D img;
	// Use this for initialization
	public void uploadphoto () {
		byte[] data =img.EncodeToJPG();
		ParseFile file = new ParseFile("photo.jpg",data);
		file.SaveAsync ().ContinueWith (t =>
		                                {
			Debug.Log(file.Name);
			Updatenewphoto (file);
		});


	}
	void Updatenewphoto(ParseFile file){
		ParseUser.Query.WhereEqualTo ("username", ParseUser.CurrentUser.Username).FindAsync ().ContinueWith (t =>
		 {
			IEnumerable<ParseUser> photo = t.Result;
			foreach(var obj in photo){
				obj["file"] = file;
				obj.SaveAsync();
			}
			
		});
	}

}
