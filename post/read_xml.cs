using System;  
using UnityEngine;  
using System.IO;  
using System.Xml;  
using System.Linq;  
using System.Text;  
using System.Collections.Generic;  
using System.Collections;
using Parse;
public class read_xml : MonoBehaviour {

		
		IEnumerator  Start()
		{
		string geo_x=null;
		string geo_y=null;
		string geo_name;
		string CMD;
		string post = "台北101購物中心";
		string url = "http://egis.moea.gov.tw/innoserve/toolLoc/GetFastLocData.aspx?cmd=searchLayer2&group=0&db=ALL&param="+post+"&coor=84";

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


				geo_name = _province.GetAttribute("Addr");
				geo_x = _province.GetAttribute("Cx");
				geo_y = _province.GetAttribute("Cy");
							//获取实际城市名  
				CMD = _province.GetAttribute("CMD");  
				Debug.Log (geo_name);
				Debug.Log (geo_x);
				Debug.Log (geo_y);

		
			}

		}
		double result = Convert.ToDouble(geo_x);
		double result2 = Convert.ToDouble(geo_y);
		
		ParseObject POST = new ParseObject("POST");
		var point = new ParseGeoPoint(result2, result);
		POST ["post_geo"] = point;
		
		POST.SaveAsync ().ContinueWith (t =>
		                                {
			Debug.Log ("文章內容:" + point);
		});	  
	}	
}

