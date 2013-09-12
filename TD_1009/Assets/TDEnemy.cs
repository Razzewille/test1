using UnityEngine;
using System.Collections;

public abstract class TDEnemy : MonoBehaviour {

	public enum Type
	{
		eBasic = 0,
		eBoss  = 1
	};

	// Use this for initialization
	void Start () {
		m_HP = (int) getStartHP();
		gameObject.renderer.material.color = getColor();
	}

	// Update is called once per frame
	void Update () {
		GameObject player = TDWorld.getWorld().getPlayer();
		Vector3 dir = player.transform.position - gameObject.transform.position;
		dir.y = 0;
		if (dir.magnitude < 0.2f)
		{
			TDPlayer tdPlayer = TDWorld.getWorld().getTDPlayer();
			tdPlayer.receiveDamage(1);
			Destroy(gameObject);
			return;
		}
		dir.Normalize();
		dir *= getSpeed()*Time.deltaTime;
		transform.Translate(dir);
	}

	public void receiveDamage(uint damage)
	{
		m_HP -= (int) damage;
		if (m_HP <= 0)
			Destroy (gameObject);
	}

	public abstract Type type();

	protected abstract uint getStartHP();
	protected abstract float getSpeed();
	protected abstract Color getColor();
	
	TDGrid.Cell[] m_path;

	int m_HP;
}
