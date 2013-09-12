using UnityEngine;
using System.Collections;

public class TDUberTower : TDTower
{
	public override uint getTowerDamage()
	{
		return TDWorld.getWorld().m_configuration.uberTowerDamage;
	}
	public override float getRocketSpeed()
	{
		return TDWorld.getWorld().m_configuration.uberTowerRocketSpeed;
	}
	public override float getRestoration()
	{
		return TDWorld.getWorld().m_configuration.uberTowerRestoration;
	}	
}
