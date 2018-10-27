using System.Collections;
using Prime31;
using UnityEngine.UI;
using UnityEngine;
public class gallery : MonoBehaviour {


	public Texture2D img=null;
	public Image picture;

	// Use this for initialization
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
		EtceteraAndroidManager.albumChooserSucceededEvent += textureLoaded;

	}
	
	void OnDisable()
	{
		EtceteraAndroidManager.albumChooserSucceededEvent -= textureLoaded;
	
	}

	public void takegallery(){
		EtceteraAndroid.promptForPictureFromAlbum (512, 512, "albumImage.jpg");



	}
	public void textureLoaded( string imagePath, Texture2D texture )
	{
		//GUILayout.Label(imagePath);
		//EtceteraAndroid.scaleImageAtPath( imagePath, 1f );
		//
		Debug.Log (texture.name);
		Debug.Log ("path" + imagePath);
		img = texture;
		Debug.Log (img.name);
		//GUI.DrawTexture(new Rect(0, 30, 200, 200),testPlane);  	
		//testPlane.GetComponent<Renderer>().material.mainTexture = texture;
		Sprite sprite = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (0.5f, 0.5f));
		picture.sprite = sprite;
		//GUI.DrawTexture (new Rect (100, 100, img.width, img.height), img);
		if (img != null) {
			item img_now = GameObject.Find ("submit").GetComponent<item> ();
			img_now.img = img;
			Debug.Log("ok!!!");
		}
		
	}
		

	// Update is called once per frame

	#endif
}
