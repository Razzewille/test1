using UnityEngine;
using System.Collections;

public class TDBasicTower : TDTower
{
	public override uint getTowerDamage()
	{
		return TDWorld.getWorld().m_configuration.towerDamage;
	}
	public override float getRocketSpeed()
	{
		return TDWorld.getWorld().m_configuration.towerRocketSpeed;
	}
	public override float getRestoration()
	{
		return TDWorld.getWorld().m_configuration.towerRestoration;
	}	
}
