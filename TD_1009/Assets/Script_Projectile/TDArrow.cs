using UnityEngine;
using System.Collections;

public class TDArrow : TDProjectile {

	public override float speed()
	{
		return TDWorld.getWorld().m_configuration.towerArcherProjectileSpeed;
	}
	public override void moveToTarget()
	{
		Vector3 dir = m_target.transform.position - transform.position;
		dir.Normalize();
		float angleY = -90f + Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
		dir *= speed()*Time.deltaTime;
		transform.Translate(dir);
		Vector3 euler = gameObject.transform.rotation.eulerAngles;
		float rotX = euler.x; 
		float rotY = angleY;
		float rotZ = euler.z;
		transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);
	}
	public override void onTargetReached()
	{
		if (m_target != null)
		{
			m_target.receiveDamage(m_damage);
		}
	}
}
