using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class TDConfiguration
{
	public TDConfiguration()
	{
		drawGrid = 1;
		drawPath = 1;
		gridNbCellsX = 20;
		gridNbCellsY = 20;

		playerHP             = 10;
		
		// Enemies
		enemyHP              = 10;
		killReward           = 20;
		enemySpeed           = 9.0f; // units/sec
		
		bossEnemyHP          = 20;
		bossKillReward       = 30;
		bossEnemySpeed       = 6.0f;// units/sec
		
		//Towers
		towerDamage          = 3;
		towerRocketSpeed     = 12.0f; // units/sec
		towerRestoration 	 = 0.3f; // sec
		towerEfficientRadius = 30f;
		
		uberTowerDamage      = 6;
		uberTowerRocketSpeed = 18.0f;
		uberTowerRestoration = 0.1f; // sec
		uberTowerEfficientRadius = 70f;
	}

	public void readFromResource()
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
				uint x = Convert.ToUInt32(gameConf[name]);
				field.SetValue(this, x);
			}
		    else if (val is float)
		    {
				float x = Convert.ToSingle(gameConf[name]);
				field.SetValue(this, x);
			}
		}
	}

	public Dictionary<string, string> getLines()
	{
		Dictionary<string, string> dic = new Dictionary<string, string>();
		TextAsset textFile = (TextAsset)Resources.Load("Configuration", typeof(TextAsset));
		if (textFile == null)
			return dic;
        System.IO.StringReader textStream = new System.IO.StringReader(textFile.text);
   		string line;
        while ((line = textStream.ReadLine()) != null)
		{
			// Skip comment
			if (line.Length < 2)
				continue;
			if (line.Contains("//"))
				continue;
			string [] aToken = line.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries );
			if (aToken.Length < 3)
				continue;
			dic[aToken[0]] = aToken[2];
		}
		return dic;
	}

	public uint drawGrid; // Draws grid over terrain
	public uint drawPath; // Draws paths for enemies
	public uint gridNbCellsX;
	public uint gridNbCellsY;
	
	public uint playerHP;
	
	// Enemies
	public uint  enemyHP;
	public uint  killReward;
	public float enemySpeed; // units/sec
	
	public uint  bossEnemyHP;
	public uint  bossKillReward;
	public float bossEnemySpeed;// units/sec
	
	//Towers
	public uint  towerDamage;
	public float towerRocketSpeed; // units/sec
	public float towerRestoration; // sec
	public float towerEfficientRadius;
	
	public uint  uberTowerDamage;
	public float uberTowerRocketSpeed;
	public float uberTowerRestoration; // sec
	public float uberTowerEfficientRadius;
}
