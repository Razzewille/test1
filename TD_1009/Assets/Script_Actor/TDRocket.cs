using UnityEngine;
using System.Collections;

public class TDRocket : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.renderer.material.color = new Color(1, 0, 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (m_target == null)
		{
			Destroy (gameObject);
			return;
		}
		Vector3 dir = m_target.transform.position - transform.position;
		if (dir.magnitude < 0.2)
		{
			TDEnemy enemy = TDWorld.getWorld().getTDEnemy(m_target);
			if (enemy != null)
			{
				TDDamage damage = new TDDamage(TDDamage.Type.ePhysical, m_damage, 0f);
				damage.setTarget(enemy);
				enemy.receiveDamage(damage);
			}
			Destroy(gameObject);
		}
		else
		{
			dir.Normalize();
			dir *= m_speed*Time.deltaTime;
			transform.Translate(dir);
		} 
	}

	public GameObject m_target;
	public uint m_damage;
	public float m_speed;
}
