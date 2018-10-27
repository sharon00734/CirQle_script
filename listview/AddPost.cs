using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddPost : MonoBehaviour {
	public UIScrollView scrollview;
	int count=1;


	// Use this for initialization
	void Start () {
		scrollview=GameObject.Find("Scroll View").GetComponent<UIScrollView>();
	}
	
	// Update is called once per frame
	void OnClick () {
		GameObject o  =(GameObject) Instantiate(Resources.Load("item"));
		//为每个预设设置一个独一无二的名称
		o.name = "item" + count;
		//将新预设放在Panel对象下面
		o.transform.parent = GameObject.Find("Scroll View").transform;

		////下面这段代码是因为创建预设时 会自动修改旋转缩放的系数，
		//我不知道为什么会自动修改，所以MOMO重新为它赋值
		//有知道的朋友麻烦告诉我一下 谢谢！！！
		Vector3 temp = new Vector3(0,-0.44f*count,0);

		GameObject item = GameObject.Find(o.name);

		item.transform.localPosition = new Vector3(0,0,0);
		item.transform.localScale= new Vector3(1,1,1);
		item.transform.position += temp;
		count ++;

		scrollview.ResetPosition ();
	}
}
