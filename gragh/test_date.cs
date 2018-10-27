using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading;
using System.Linq;
using System.Globalization;
public class test_date : MonoBehaviour {

	// Use this for initialization
	void Start () {
		WMG_Axis_Graph test = GetComponent<WMG_Axis_Graph> ();
		DateTime dtObj = DateTime.Now;
		
		//以 月份 日, 年 的格式輸出
		string yesterday= dtObj.AddDays(-1).ToString("MM/dd", new CultureInfo("en-US"));
		string  two= dtObj.AddDays(-2).ToString("MM/dd", new CultureInfo("en-US"));
		string  three= dtObj.AddDays(-3).ToString("MM/dd", new CultureInfo("en-US"));
		string  four= dtObj.AddDays(-4).ToString("MM/dd", new CultureInfo("en-US"));
		string  five= dtObj.AddDays(-5).ToString("MM/dd", new CultureInfo("en-US"));
		//結果為 March 11, 2010
		test.xAxisLabels[0]=four;
		test.xAxisLabels[1]=three;
		test.xAxisLabels[2]=two;
		test.xAxisLabels[3]=yesterday;
		test.xAxisLabels[4]="今日";


	}
	

}
