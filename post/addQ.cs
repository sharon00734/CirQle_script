using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class addQ : MonoBehaviour {
	//public GameObject Q;
	//public GameObject main;
	public GameObject Q_content;
	private UIPlayTween targetvalue_Q;
	//private UILabel labelvalue;
	//private string test = "HI";
	private Vector3 Q_pos = new Vector3 (1, 1, 1);
	private Vector3 QC_pos = new Vector3 (1, 1, 1);

	void OnClick () {
		/*Instantiate (Q, Q_pos, Quaternion.identity);
		Instantiate (Q_content, QC_pos, Quaternion.identity);
		Debug.Log (Q_pos);
		Debug.Log (Q.transform.position);
		Debug.Log (Q_content.transform.position);*/
		Instantiate_Q ();

	}


	void Instantiate_Q(){
		GameObject GO_current = (GameObject)Instantiate(Resources.Load("Q"));
		GameObject GO_current_C = (GameObject)Instantiate(Resources.Load("Q3_page"));
	//	GameObject GO_current= (GameObject)Instantiate (Q, Q_pos, Quaternion.identity);
	//	GameObject GO_current_C= (GameObject)Instantiate (Q_content, QC_pos, Quaternion.identity);


		// 取得物件上的 UIPlayTween script
		targetvalue_Q = GO_current.transform.GetComponent<UIPlayTween> ();
	
		targetvalue_Q.tweenTarget = GO_current_C;

		//labelvalue.text = test.ToString();

		//GO_current.name = Q.name;
		//GO_current_C.name = Q3_page.name;
	
		//GO_current.transform.parent = GameObject.Find("12").transform;  
		//GameObject item1 = GameObject.Find(GO_current.name);  
		GO_current.transform.localPosition = new Vector3(35,0,0);  
		GO_current.transform.localScale= new Vector3(1,1,1);  

		//GO_current_C.transform.parent = GameObject.Find("12").transform;  
		//GameObject item = GameObject.Find(GO_current_C.name);
		//GO_current_C.transform.localPosition = new Vector3(20,35,0);
		//GO_current_C.transform.localScale= new Vector3(1,1,1);

		//GO_current.SetActive(false);
		//GO_current_C.SetActive(false);

 
	//	GameObject.Find("Panel").GetComponent<UIPanel>().widgetsAreStatic = true;
		//UILabel content = GO_current_C.GetComponent<UILabel>(); 
		
		//content.text = post;
		//string text_str = "貼文內容 " + GO_current_C.GetComponent<UILabel> ().text;   
		  
	
		//GUI.Label(new Rect(10,50,200,200),"文章內容: "+ post);  
		//GameObject GO_current_C= (GameObject)Instantiate (Q_content);


		//GO_current.transform.position = Q_pos;
		//GO_current_C.transform.position = QC_pos;

	}

	/*void CreatLabel(GameObject GO_current_C)  
	{  
		UILabel label = NGUITools.AddChild<UILabel>(gameObject);  
		label.text = "Test";  
	}  */

}
