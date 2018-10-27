using System;  
using UnityEngine;  
using System.IO;  
using System.Xml;  
using System.Linq;  
using System.Text;  
using System.Collections.Generic;  
using System.Collections;
using Parse;

public class get_place : MonoBehaviour {
	public UILabel mysearch;
	UIInput mInput;
	UIPopupList test;
	public string geo_name;
	// Use this for initialization
	IEnumerator OnClick () {

		test = GameObject.Find("place_pop").GetComponent<UIPopupList>();
		mysearch = GetComponentInChildren<UILabel>();

		mInput = GameObject.Find("place_input").GetComponent<UIInput>();
		string place = mInput.text;
		Debug.Log (place);
		test.items.Clear();

		string geo_source;
		string geo_x=null;
		string geo_y=null;

		string CMD;
		//string post = "台北101購物中心";
		string url = "http://egis.moea.gov.tw/innoserve/toolLoc/GetFastLocData.aspx?cmd=searchLayer2&group=0&db=ALL&param="+place+"&coor=84";
		
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
			
			XmlNode provinces = xmlDoc.SelectSingleNode("result");  
			Debug.Log ("readxml");
			/*
			XmlNodeList nodeList = xmlDoc.SelectNodes("result");
			int numGoods = nodeList.Count;
			Debug.Log(numGoods);*/
			
			foreach (XmlNode province in provinces)  
			{  
				XmlElement _province = (XmlElement)province;  
				
				
				geo_name =_province.GetAttribute("Addr") ;
				geo_source= _province.GetAttribute("Source");
			

				if (geo_source=="地標"){
				mysearch.text=geo_name;
				test.items.Add(geo_name);
				//GetComponent<UILabel>().text = test.value;
				geo_x = _province.GetAttribute("Cx");
				geo_y = _province.GetAttribute("Cy");
				Debug.Log (geo_name);
				Debug.Log (geo_x);
				Debug.Log (geo_y);
				}else{
					mysearch.text="無資料呦!";

					//geo_name="無資料呦!";
					//test.items.Add(geo_name);

				}
				
			

			}


	
		
			
		}
		mInput.text = "";

		/*
		double result = Convert.ToDouble(geo_x);
		double result2 = Convert.ToDouble(geo_y);
		
		ParseObject POST = new ParseObject("POST");
		var point = new ParseGeoPoint(result2, result);
		POST ["Post_Geo"] = point;

		POST ["geo_place"] = geo_name;
		POST.SaveAsync ().ContinueWith (t =>
		                                {
			Debug.Log ("文章內容:" + point);
			Debug.Log ("文章內容:" + geo_name);

		});*/	  
	}

}
