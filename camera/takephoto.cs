using System.Collections;
using Prime31;
using UnityEngine.UI;
using UnityEngine;
public class takephoto : MonoBehaviour {
	public Texture2D img=null;
	public Image picture;

	#if UNITY_ANDROID
	// Use this for initialization
	void Start () {
		Debug.Log ("hello");
		EtceteraAndroid.initTTS();
		picture = GameObject.Find("CirQle_slide/Canvas/Image").GetComponent<Image>();
	}
	void OnEnable()
	{
		// Listen to the texture loaded methods so we can load up the image on our plane

		EtceteraAndroidManager.photoChooserSucceededEvent += textureLoaded;
	}

	
	void OnDisable()
	{
		EtceteraAndroidManager.photoChooserSucceededEvent -= textureLoaded;
	}
	// Update is called once per frame
	/*
		if (img != null)  
	{  
		GUI.DrawTexture(new Rect(0, 20, 50, 50), img);  
	}  
	*/
	public void takephotos(){
		EtceteraAndroid.promptToTakePhoto (512, 512, "photo.jpg");
	}
	public void textureLoaded( string imagePath, Texture2D texture )
	{
		//GUILayout.Label(imagePath);
		//EtceteraAndroid.scaleImageAtPath( imagePath, 1f );
		//
		//Debug.Log (texture.name);
		Debug.Log ("path" + imagePath);
		img = texture;
		//Debug.Log (img.name);
		//GUI.DrawTexture(new Rect(0, 30, 200, 200),testPlane); 
		//picture.mainTexture = texture;
		//SpriteRenderer spr=gameObject.GetComponent<SpriteRenderer> ();
		//Images picture = gameObject.GetComponent<Images> ();
		Sprite sprite = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (0.5f, 0.5f));
		picture.sprite = sprite;
		//picture.GetComponent<SpriteRenderer>().material.mainTexture = texture;

		if (img != null) {
			item img_now = GameObject.Find ("submit").GetComponent<item> ();
			img_now.img = img;
			Debug.Log("ok!!!");
		}
	}	
	
	#endif
}
