using Prime31;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;  
using System.Threading.Tasks;
using System.Linq;  
using System.IO;  
using System.Xml;  
using System.Linq;  
using System.Text; 
using UnityEngine.UI;
public class upload : MonoBehaviour {
	public UIInput userpostLabel;
	private ToggleScene post_value;
	private string city;
	public GameObject Panel;
	private string post_type;
	public UIInput[] UserTagInput = new UIInput[3];
	public string geo_x;
	public string geo_y;
	private string Post_Id;
	private string UserTag_1;
	private string UserTag_2;
	private string UserTag_3;
	public string[] TagID = new string[3];
	public Texture2D img;
	public Texture2D img1;
	protected string InitialAmount="0";
	//public string lbs_name="none";
	public void upload1(){

		StartCoroutine ("start1");
	}


	IEnumerator start1 ()
	{
		//Resources.UnloadUnusedAssets();

		post_value = Panel.transform.GetComponent<ToggleScene> ();
		post_type = post_value.post_type;
		Debug.Log ("文章類型:" + post_type);
		Debug.Log("submit");
	

		UIPopupList cityselect = GameObject.Find ("CitySelect").GetComponent<UIPopupList> ();
		city = cityselect.value;
		if (city == "台北") {
			city = "Taipei";
		} else if (city == "台中") {
			city = "Taichung";
		} else if (city == "台南") {
			city = "Tainan";
		} else if (city == "新北") {
			city = "NewTaipei";
		} else if (city == "桃園") {
			city = "Taoyuan";
		} else if (city == "高雄") {
			city = "Kaohsiung";
		}
		//進經濟地理資料庫部分
		UIPopupList lbs = GameObject.Find("place_pop").GetComponent<UIPopupList>();
		string lbs_name=lbs.value;
		if (lbs_name != "") {
			//StartCoroutine(xml(lbs_name));
			string url = "http://egis.moea.gov.tw/innoserve/toolLoc/GetFastLocData.aspx?cmd=searchLayer2&group=0&db=ALL&param=" + lbs_name + "&coor=84";
			WWW www = new WWW (url);
			yield return www;
			if (www.error == null) {
				
				Debug.Log ("Loaded following XML " + www.data);
				
				XmlDocument xmlDoc = new XmlDocument ();
				xmlDoc.LoadXml (www.data);
				
				XmlNode provinces = xmlDoc.SelectSingleNode("result");  
				Debug.Log ("readxml");
				
				foreach (XmlNode province in provinces)  
				{  
					XmlElement _province = (XmlElement)province;  
					
					geo_x = _province.GetAttribute("Cx");
					geo_y = _province.GetAttribute("Cy");
					Debug.Log (geo_x);
					Debug.Log (geo_y);
					//yield return geo_x;
					//yield return geo_y;
					
				}
				
			}
		}
		
		
		UserTag_1 = UserTagInput[0].value;
		UserTag_2 = UserTagInput[1].value;
		UserTag_3 = UserTagInput[2].value;
		
		string userpost = userpostLabel.value;

		/*
		ParseObject POST = new ParseObject("POST");
		POST ["postfield"] = userpost;
		POST ["Location"] = city;
		POST ["foo"] = post_type;
		POST.SaveAsync();*/

		ParseObject POST = new ParseObject("POST");
		/*Loom.QueueOnMainThread (() => {
			if (img == null) {
				byte[] data = img1.EncodeToJPG ();
				ParseFile file = new ParseFile ("none.jpg", data);
				file.SaveAsync ().ContinueWith (t =>
				{
					Debug.Log (file.Name);
				});
				POST ["file"] = file;
			} else {
				byte[] data = img.EncodeToJPG ();
				ParseFile file = new ParseFile ("photo.jpg", data);
				file.SaveAsync ().ContinueWith (t =>
				{
					Debug.Log (file.Name);
				});
				POST ["file"] = file;
			}
		});*/
		if (lbs_name != "") {
			double result = Convert.ToDouble(geo_x);
			double result2 = Convert.ToDouble(geo_y);
			var point = new ParseGeoPoint (result2, result);
			POST ["Post_Geo"] = point;
			POST ["geo_place"] = lbs_name;
		}
		POST ["postfield"] = userpost;
		POST ["Location"] = city;
		POST ["foo"] = post_type;
		POST ["Like"] = InitialAmount;
		POST ["DisLike"] = InitialAmount;
		POST ["Tag1"] = UserTag_1;
		POST ["Tag2"] = UserTag_2;
		POST ["Tag3"] = UserTag_3;
		POST ["Sum"] = 0;
		POST ["User"] = ParseUser.CurrentUser.Username;
		Debug.Log (post_type);
		Debug.Log (city);
		//POST.SaveAsync();
		//Debug.Log ("文章內容:" + userpost);
		POST.SaveAsync ().ContinueWith (t =>
		{
			Debug.Log ("文章內容:" + userpost);

		});


		FindorSave (UserTag_1, 0);
		FindorSave (UserTag_2, 1);
		FindorSave (UserTag_3, 2);
			
		cityselect.value = "城市";

		userpostLabel.value = "";
		lbs_name = null;
		Debug.Log (lbs_name);


	}
	/*IEnumerator xml(string lbs_name){


	}*/
	/*void Judge(string id){
		ParseObject JUDGE = new ParseObject("JUDGE");
		JUDGE ["Post_Id"] = id;
		JUDGE ["Like"] = InitialAmount;
		JUDGE ["DisLike"] = InitialAmount;
		JUDGE.SaveAsync();
		Debug.Log (id);
	}*/



	void FindorSave(string UserTag, int count){

		if (UserTag != "") {
	
			var query = new ParseQuery<ParseObject> ("TAG").WhereEqualTo ("TagContent", UserTag).WhereEqualTo("Tag_City",city);
			query.FirstAsync ().ContinueWith (t2 =>
			{
				Loom.QueueOnMainThread (() => {
		
					if (t2.IsCanceled || t2.IsFaulted) {
					
						Debug.Log ("noResult,saving." + UserTag);
						Save (UserTag, count);
					
					} else {

						ParseObject obj = t2.Result;
						//foreach (var test in obj) {
						int i = obj.Get<int> ("Tag_Count");
						i++;
						obj ["Tag_Count"] = i;
						obj.SaveAsync ();
						//TagID[count]=obj.ObjectId;
						//ParseObject tag_post = new ParseObject("POST_TAG");
						//tag_post["Tag_Id"]=TagID[count];
						//tag_post["Post_Id"]=post_id;
						//tag_post.SaveAsync();
						Debug.Log ("find: " + UserTag); 
						IDictionary<string, object> parms = new Dictionary<string, object>
						{
							{ "tag1", UserTag },
							{ "tag1_city", city }
						};
						ParseCloud.CallFunctionAsync<IDictionary<string, object>>("tag_computing", parms).ContinueWith(t => {
							var score = t.Result;
						}); 
						UserTagInput [count].value = "";

						//}
					}
				}); 
			});
		} 


	}
	
	void Save(string UserTag, int count){
		
		ParseObject myTAG = new ParseObject ("TAG");
		myTAG ["TagContent"] = UserTag;
		myTAG ["Tag_City"] = city;
		myTAG ["Tag_Count"] = 1;
		Task saveTask = myTAG.SaveAsync();		
		while(saveTask.IsCompleted)
			Debug.Log("done");
		
		UserTagInput[count].value = "";
	}


}
