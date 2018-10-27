using System;  
using UnityEngine;  
using System.IO;  
using System.Xml;  
using System.Linq;  
using System.Text;  
using System.Collections.Generic;  
using System.Collections;

public class xml_test : MonoBehaviour {
	public string geo_name;
	public string geo_x;
	public string geo_y;
	change_new_city exist_place;



	public void get_gps(){
	
		if (geo_x !=null && geo_y != null) {
			/*
			float x = 120.299991f;
			float y = 22.612185f;
			string x1= x.ToString('R');
			string y1= y.ToString();
			Debug.Log(x1);
			Debug.Log(y1);*/
			StartCoroutine ("Start");
		}

	}

	IEnumerator  Start()
	{


		//string CMD;
		//string post = "台北101購物中心";
		//string url = "http://egis.moea.gov.tw/innoserve/toolLoc/GetFastLocData.aspx?cmd=searchLayer2&group=0&db=ALL&param="+post+"&coor=84";
		string url = "http://ngis.moea.gov.tw/NgisFxData/webservice/XMLFunc_basic.aspx?cmd=basic&x="+geo_x+"&y="+geo_y+"&coor=84&buffer=500";
	
		WWW www = new WWW (url);
		
		//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
		yield return www;
		if (www.error == null) {
			//Sucessfully loaded the XML
			Debug.Log ("Loaded following XML " + www.data);
			//geo_name=xl2.GetAttribute("Addr") + ": " + xl2.InnerText;	
			//Create a new XML document out of the loaded data
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.LoadXml (www.data);
			
			XmlNode provinces = xmlDoc.SelectSingleNode("RESULT");  
			Debug.Log ("readxml");
			/*
			XmlNodeList nodeList = xmlDoc.SelectNodes("result");
			int numGoods = nodeList.Count;
			Debug.Log(numGoods);*/
			
			foreach (XmlNode province in provinces)  
			{  
				XmlElement _province = (XmlElement)province;  
				
				
				geo_name = _province.GetAttribute("COUN");
				Debug.Log (geo_name);

				if (geo_name =="臺北市"){
					change_new_city exist_place = GameObject.Find("台北").GetComponent<change_new_city>();
					exist_place.exist = "exist";
					change_new_city exist_placek = GameObject.Find("高雄").GetComponent<change_new_city>();
					exist_placek.exist = "notexist";
					change_new_city exist_placet = GameObject.Find("台中").GetComponent<change_new_city>();
					exist_placet.exist = "notexist";
					change_new_city exist_placen = GameObject.Find("新北").GetComponent<change_new_city>();
					exist_placen.exist = "notexist";
					change_new_city exist_placena = GameObject.Find("台南").GetComponent<change_new_city>();
					exist_placena.exist = "notexist";
					change_new_city exist_placeta = GameObject.Find("桃園").GetComponent<change_new_city>();
					exist_placeta.exist = "notexist";

					break;
				}else if(geo_name =="新北市"){
					change_new_city exist_place = GameObject.Find("台北").GetComponent<change_new_city>();
					exist_place.exist = "notexist";
					change_new_city exist_placek = GameObject.Find("高雄").GetComponent<change_new_city>();
					exist_placek.exist = "notexist";
					change_new_city exist_placet = GameObject.Find("台中").GetComponent<change_new_city>();
					exist_placet.exist = "notexist";
					change_new_city exist_placen = GameObject.Find("新北").GetComponent<change_new_city>();
					exist_placen.exist = "exist";
					change_new_city exist_placena = GameObject.Find("台南").GetComponent<change_new_city>();
					exist_placena.exist = "notexist";
					change_new_city exist_placeta = GameObject.Find("桃園").GetComponent<change_new_city>();
					exist_placeta.exist = "notexist";
					
					break;
				}else if(geo_name =="桃園市"){
					change_new_city exist_place = GameObject.Find("台北").GetComponent<change_new_city>();
					exist_place.exist = "notexist";
					change_new_city exist_placek = GameObject.Find("高雄").GetComponent<change_new_city>();
					exist_placek.exist = "notexist";
					change_new_city exist_placet = GameObject.Find("台中").GetComponent<change_new_city>();
					exist_placet.exist = "notexist";
					change_new_city exist_placen = GameObject.Find("新北").GetComponent<change_new_city>();
					exist_placen.exist = "notexist";
					change_new_city exist_placena = GameObject.Find("台南").GetComponent<change_new_city>();
					exist_placena.exist = "notexist";
					change_new_city exist_placeta = GameObject.Find("桃園").GetComponent<change_new_city>();
					exist_placeta.exist = "exist";
					
					break;
				}else if(geo_name =="臺中市"){
					change_new_city exist_place = GameObject.Find("台北").GetComponent<change_new_city>();
					exist_place.exist = "notexist";
					change_new_city exist_placek = GameObject.Find("高雄").GetComponent<change_new_city>();
					exist_placek.exist = "notexist";
					change_new_city exist_placet = GameObject.Find("台中").GetComponent<change_new_city>();
					exist_placet.exist = "exist";
					change_new_city exist_placen = GameObject.Find("新北").GetComponent<change_new_city>();
					exist_placen.exist = "notexist";
					change_new_city exist_placena = GameObject.Find("台南").GetComponent<change_new_city>();
					exist_placena.exist = "notexist";
					change_new_city exist_placeta = GameObject.Find("桃園").GetComponent<change_new_city>();
					exist_placeta.exist = "notexist";
					
					break;
				}else if(geo_name =="臺南市"){
					change_new_city exist_place = GameObject.Find("台北").GetComponent<change_new_city>();
					exist_place.exist = "notexist";
					change_new_city exist_placek = GameObject.Find("高雄").GetComponent<change_new_city>();
					exist_placek.exist = "notexist";
					change_new_city exist_placet = GameObject.Find("台中").GetComponent<change_new_city>();
					exist_placet.exist = "notexist";
					change_new_city exist_placen = GameObject.Find("新北").GetComponent<change_new_city>();
					exist_placen.exist = "notexist";
					change_new_city exist_placena = GameObject.Find("台南").GetComponent<change_new_city>();
					exist_placena.exist = "exist";
					change_new_city exist_placeta = GameObject.Find("桃園").GetComponent<change_new_city>();
					exist_placeta.exist = "notexist";
					
					break;
				}else if(geo_name =="高雄市"){
					change_new_city exist_place = GameObject.Find("台北").GetComponent<change_new_city>();
					exist_place.exist = "notexist";
					change_new_city exist_placek = GameObject.Find("高雄").GetComponent<change_new_city>();
					exist_placek.exist = "exist";
					change_new_city exist_placet = GameObject.Find("台中").GetComponent<change_new_city>();
					exist_placet.exist = "notexist";
					change_new_city exist_placen = GameObject.Find("新北").GetComponent<change_new_city>();
					exist_placen.exist = "notexist";
					change_new_city exist_placena = GameObject.Find("台南").GetComponent<change_new_city>();
					exist_placena.exist = "notexist";
					change_new_city exist_placeta = GameObject.Find("桃園").GetComponent<change_new_city>();
					exist_placeta.exist = "notexist";
					
					break;
				}


				
			}
			UILabel label = GameObject.Find("place/city_now").GetComponent<UILabel>();
			label.text = geo_name;
			
		}
		/*
		  	//geo_y = _province.GetAttribute("Cy");
				//获取实际城市名  
				//CMD = _province.GetAttribute("CMD");  

			
				//Debug.Log (geo_x);
				//Debug.Log (geo_y);
		double result = Convert.ToDouble(geo_x);
		double result2 = Convert.ToDouble(geo_y);
		
		ParseObject POST = new ParseObject("POST");
		var point = new ParseGeoPoint(result2, result);
		POST ["post_geo"] = point;
		
		POST.SaveAsync ().ContinueWith (t =>
		                                {
			Debug.Log ("文章內容:" + point);
		});*/	  
	}	
}
