﻿using UnityEngine;
using System.Collections;

public class TDImp : TDEnemy {

	public override uint getStartHP()
	{
		return TDWorld.getWorld().m_configuration.enemyHP;
	}
	public override float getStartSpeed()
	{
		return TDWorld.getWorld().m_configuration.enemySpeed;
	}
	protected override bool canFly()
	{
		return false;
	}
	protected override float flyHeight()
	{
		return 5.0f;
	}
	public override float getResistance(TDDamage.Type type)
	{
		return 0f;
	}
}
