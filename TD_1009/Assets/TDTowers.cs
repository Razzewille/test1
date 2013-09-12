using UnityEngine;
using System.Collections;

public class TDBasicTower : TDTower
{
	public override uint getTowerDamage()
	{
		return TDWorld.s_world.m_configuration.towerDamage;
	}
	public override float getRocketSpeed()
	{
		return TDWorld.s_world.m_configuration.towerRocketSpeed;
	}
	public override float getRestoration()
	{
		return TDWorld.s_world.m_configuration.towerRestoration;
	}	
}

public class TDUberTower : TDTower
{
	public override uint getTowerDamage()
	{
		return TDWorld.s_world.m_configuration.uberTowerDamage;
	}
	public override float getRocketSpeed()
	{
		return TDWorld.s_world.m_configuration.uberTowerRocketSpeed;
	}
	public override float getRestoration()
	{
		return TDWorld.s_world.m_configuration.uberTowerRestoration;
	}	
}
