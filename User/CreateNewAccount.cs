using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;

public class CreateNewAccount : MonoBehaviour {
	public UIInput name;
	public UIInput email;
	public UIInput password;
	public UIInput passwordconfirm;
	public UIPopupList place;
	public GameObject success;
	public GameObject failed;
	public Texture2D img;
	public Texture2D img1;
	// Use this for initialization
	

	public void photo(){
	
		Loom.QueueOnMainThread(()=>{
			if(img==null){
				byte[] data =img1.EncodeToJPG();
				ParseFile file = new ParseFile("none.jpg",data);
				file.SaveAsync ().ContinueWith (t =>
				                                {
					Debug.Log(file.Name);
					createnewaccount(file);
				});
			
			}else{
				byte[] data =img.EncodeToJPG();
				ParseFile file = new ParseFile("photo.jpg",data);
				file.SaveAsync ().ContinueWith (t =>
				                                {
					Debug.Log(file.Name);
					createnewaccount(file);
				});
		
			}
		});
	
	}

	public void createnewaccount(ParseFile file){
		Loom.QueueOnMainThread (() => {
			if (name.value != null && password.value != null && place.value != null && passwordconfirm.value != null) {
				
				//if(password.value==passwordconfirm.value){
				var user = new ParseUser ()
			{
				Username = email.value,
				Password = password.value,
				Email = email.value
			};
				user ["file"] = file;
				user ["name"] = name.value;
				user ["place"] = place.value;

				if (password.value == passwordconfirm.value) {
					Task signUpTask = user.SignUpAsync ().ContinueWith (t =>
					{
						Loom.QueueOnMainThread (() => {

							if (t.IsFaulted || t.IsCanceled) {
								// The login failed. Check t.Exception to see why.
								Debug.Log ("Parse Registration failed!");
								Debug.Log (t.Exception);
								failed.SetActive (true);

							} else {
								Debug.Log ("Parse Registration was successful!");
								/*TweenTransform transform=GameObject.Find("SignUp").GetComponent<TweenTransform>();
							transform.ResetToBeginning();*/

								success.SetActive (true);
							}
						}); 		
					}); 
				} else {
					Debug.Log ("passwordfailed");
				}
				//}
				//if(password!=passwordconfirm){
				//	Debug.Log("password!=passwordconfirm");
				//}
				
		
			}
		});
	}
}
			