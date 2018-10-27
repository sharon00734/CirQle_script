using UnityEngine;  
using System.Collections;  

public class GPSManager2 : MonoBehaviour {
	xml_test geo_y; 
	xml_test geo_x;	
	public string gps_info = "";  
	public int flash_num = 1;  
	public float latitude;
	public float longitude;
		
	void Start(){
		StartCoroutine(StartGPS());
	}
		
		/*void OnGUI () {  
			GUI.skin.label.fontSize = 28;  
			GUI.Label(new Rect(20,20,600,48),this.gps_info);   
			GUI.Label(new Rect(20,50,600,48),this.flash_num.ToString());   
			
			GUI.skin.button.fontSize = 50;  
			if (GUI.Button(new Rect(Screen.width/2-110,200,220,85),"GPS定位"))  
			{  
				// 这里需要启动一个协同程序  
				StartCoroutine(StartGPS());  
			}  
			if (GUI.Button(new Rect(Screen.width/2-110,500,220,85),"刷新GPS"))  
			{  
				this.gps_info = "N:" + Input.location.lastData.latitude + " E:"+Input.location.lastData.longitude;  
				this.gps_info = this.gps_info + " Time:" + Input.location.lastData.timestamp;  
				this.flash_num += 1;   
			}  
		}  */
		
		// Input.location = LocationService  
		// LocationService.lastData = LocationInfo   
		
	void StopGPS () {  
		Input.location.Stop();  
	}  
		
	IEnumerator StartGPS () {  
			// Input.location 用于访问设备的位置属性（手持设备）, 静态的LocationService位置  
			// LocationService.isEnabledByUser 用户设置里的定位服务是否启用  
		if (!Input.location.isEnabledByUser) {  
			this.gps_info = "isEnabledByUser value is:"+Input.location.isEnabledByUser.ToString()+" Please turn on the GPS";   
			UILabel label = GameObject.Find("gps").GetComponent<UILabel>();
			label.text = this.gps_info;

			return false;  
		}  
			
			// LocationService.Start() 启动位置服务的更新,最后一个位置坐标会被使用  
		Input.location.Start(10.0f, 10.0f);  
			
		int maxWait = 30;  
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {  
				// 暂停协同程序的执行(1秒)  
			yield return new WaitForSeconds(1);  
			maxWait--;  
		}  
			
		if (maxWait < 1) {  
			this.gps_info = "Init GPS service time out";  
			UILabel label = GameObject.Find("gps").GetComponent<UILabel>();
			label.text = this.gps_info;

			return false;  
		}  
			
		if (Input.location.status == LocationServiceStatus.Failed) {  
			this.gps_info = "Unable to determine device location";  
			UILabel label = GameObject.Find("gps").GetComponent<UILabel>();
			label.text = this.gps_info;

			return false;  
		}   
		else {  
			this.gps_info = "N:" + Input.location.lastData.latitude + " E:"+Input.location.lastData.longitude;  
			this.gps_info = this.gps_info + " Time:" + Input.location.lastData.timestamp;  
		
			latitude=Input.location.lastData.latitude;
			longitude=Input.location.lastData.longitude;

			string geoy= latitude.ToString();
			string geox= longitude.ToString();
			
			UILabel label = GameObject.Find("gps").GetComponent<UILabel>();
			label.text = this.gps_info;
			
			xml_test geo_x = GameObject.Find("city_now").GetComponent<xml_test>();
			geo_x.geo_x = geox;

			xml_test geo_y = GameObject.Find("city_now").GetComponent<xml_test>();
			geo_y.geo_y = geoy;

			xml_test gps = GameObject.Find("city_now").GetComponent<xml_test>();
			gps.get_gps();
			yield return new WaitForSeconds(100);  
		}  
	}
}
