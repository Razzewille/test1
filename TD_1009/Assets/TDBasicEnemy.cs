﻿using UnityEngine;
using System.Collections;

public class TDBasicEnemy : TDEnemy
{

	protected override uint getStartHP()
	{
		return TDWorld.getWorld().m_configuration.enemyHP;
	}
	protected override float getSpeed()
	{
		return TDWorld.getWorld().m_configuration.enemySpeed;
	}
	protected override Color getColor()
	{
		return new Color(0, 1, 1, 1);
	}
}

