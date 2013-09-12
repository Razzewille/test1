using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class TDConfiguration
{
	public TDConfiguration()
	{
		playerHP             = 10;
		towerDamage          = 3;
		enemyHP              = 10;
		killReward           = 20;
		enemySpeed           = 3f;
		rocketSpeed          = 6f;
		towerRestoration 	   = 1f;
		uberTowerRestoration = 0.1f;

		readFromResource();
	}

	void readFromResource()
	{
		Dictionary<string, string> gameConf = getLines();

		System.Type type = typeof(TDConfiguration);
		type.GetFields();
		FieldInfo[] fields = type.GetFields();
		foreach (var field in fields)
		{
		    string name = field.Name;
			if (!gameConf.ContainsKey(name))
				continue;
			
		    object val = field.GetValue(this);
		    if (val is uint) 
		    {
				field.SetValue(this, Convert.ToUInt32(gameConf[name]));
			}
		    else if (val is float)
		    {
				field.SetValue(this, Convert.ToDouble(gameConf[name]));
			}
		}
	}

	public Dictionary<string, string> getLines()
	{
		Dictionary<string, string> dic = new Dictionary<string, string>();
		TextAsset textFile = (TextAsset)Resources.Load("Configuration", typeof(TextAsset));
        System.IO.StringReader textStream = new System.IO.StringReader(textFile.text);
   		string line;
        while ((line = textStream.ReadLine()) != null)
		{
			// Skip comment
			if (line.Length < 2)
				continue;
			if (line.Contains("//"))
				continue;
			string [] aToken = line.Split(' ');
			if (aToken.Length < 3)
				continue;
			foreach (string str in aToken)
			{
				dic[aToken[0]] = aToken[2];
			}
		}
		return dic;
	}

	public uint playerHP;

	public uint towerDamage;
	public uint enemyHP;
	public uint killReward;

	public float enemySpeed; // units/sec
	public float rocketSpeed; // units/sec

	public float towerRestoration; // sec
	public float uberTowerRestoration; // sec
}
