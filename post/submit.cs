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

public class submit : MonoBehaviour {

	public UIInput userpostLabel;
	private ToggleScene post_value;
	public GameObject Panel;
	private string post_type;
	public UIInput[] UserTagInput = new UIInput[3];
	public string geo_x;
	public string geo_y;
	private string UserTag_1;
	private string UserTag_2;
	private string UserTag_3;
	public string[] TagID = new string[3];
	public Texture2D img=null;
	protected string InitialAmount="0";

	IEnumerator OnClick ()
	{


		post_value = Panel.transform.GetComponent<ToggleScene> ();
		post_type = post_value.post_type;
		Debug.Log ("文章類型:" + post_type);
		Debug.Log("submit");

		UIPopupList cityselect = GameObject.Find ("CitySelect").GetComponent<UIPopupList> ();
		string city = cityselect.value;
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
		if (lbs_name != null) {
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
		
		ParseObject POST = new ParseObject("POST");


		if(img!=null){
			byte[] data =img.EncodeToJPG();
			//byte[] files = System.Text.Encoding.UTF8.GetBytes(data);
			ParseFile file = new ParseFile("resume.jpg",data);
			
			file.SaveAsync ().ContinueWith (t =>
			{
				Debug.Log(file.Name);
			});
			POST ["file"] = file;
		}

		if (lbs_name != null) {
			double result = Convert.ToDouble(geo_x);
			double result2 = Convert.ToDouble(geo_y);
			var point = new ParseGeoPoint (result2, result);
			POST ["Post_Geo"] = point;
			POST ["geo_place"] = lbs_name;
		}
		POST ["postfield"] = userpost;
		POST ["Location"] = city;
		POST ["foo"] = post_type;
		POST ["User"] = ParseUser.CurrentUser.Username;
		string str = POST.ObjectId;
		POST.SaveAsync ().ContinueWith (t =>
		{
			Debug.Log ("文章內容:" + userpost);
		});
		cityselect.value = "城市";
		lbs.value = "確認→";
		userpostLabel.value = "";

		FindorSave (UserTag_1, 0);
		FindorSave (UserTag_2, 1);
		FindorSave (UserTag_3, 2);

		ParseObject JUDGE = new ParseObject("JUDGE");
		JUDGE ["Post_Id"] = str;
		JUDGE ["Like"] = InitialAmount;
		JUDGE ["DisLike"] = InitialAmount;
		JUDGE.SaveAsync ();
		Debug.Log ("save");
	}
	/*IEnumerator xml(string lbs_name){


	}*/

	void FindorSave(string UserTag, int count){

		var query = new ParseQuery<ParseObject> ("TAG").WhereEqualTo ("TagContent", UserTag);
		query.FirstAsync().ContinueWith(t =>
		                                  {
			Loom.QueueOnMainThread(()=>{
				if (t.IsCanceled || t.IsFaulted) {
					
					Debug.Log ("noResult,saving.");
					Save(UserTag,count);

				} else {
					ParseObject obj = t.Result;
					foreach (var test in obj) {
						TagID[count]=obj.ObjectId;
						Debug.Log(TagID[count]);
						Debug.Log("find: " + UserTag);  														
					}
					
					UserTagInput[count].value = "";
				}
			}); 
		});

	}

	void Save(string UserTag, int count){

		ParseObject myTAG = new ParseObject ("TAG");
		myTAG ["TagContent"] = UserTag;					
		Task saveTask = myTAG.SaveAsync().ContinueWith (t =>
		{
			Debug.Log("ID:" + myTAG.ObjectId);
			TagID[count]=myTAG.ObjectId.ToString();
			Debug.Log("Tag:"+TagID[count]);
		});
		
		while(saveTask.IsCompleted)
			Debug.Log("done");

		UserTagInput[count].value = "";
	}

}

