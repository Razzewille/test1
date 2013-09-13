using UnityEngine;
using System.Collections;

public abstract class TDEnemy : MonoBehaviour {

	public enum Type
	{
		eBasic = 0,
		eBoss  = 1
	};

	public delegate void EventHandler(TDEnemy enemy);
	public event EventHandler OnEventDestroy;

	void DestroyThis()
	{
		OnEventDestroy(this);
		Destroy(gameObject);
	}		

	// Use this for initialization
	void Start () {
		m_maxHP = m_HP = (int) getStartHP();
		gameObject.renderer.material.color = getColor();
		GameObject enemyHealthPrefab = (GameObject) Resources.Load("EnemyHealthBarPrefab");
		m_healthBar = (GameObject) Instantiate(enemyHealthPrefab, new Vector3(0.5f, 0.5f), new Quaternion());
		updateHealthBar();
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
			DestroyThis();
			return;
		}
		dir.Normalize();
		dir *= getSpeed()*Time.deltaTime;
		transform.Translate(dir);
		
		updateHealthBar();
	}

	void updateHealthBar()
	{
		Vector3 txtPos = Camera.main.WorldToViewportPoint(transform.position);
		m_healthBar.transform.position = txtPos;
		float hpLeft = ((float) m_HP)/((float) m_maxHP);
		hpLeft *= 30;
		m_healthBar.guiTexture.pixelInset = new Rect(0, 0, hpLeft, 10);
	}

	void OnDestroy()
	{
		Destroy(m_healthBar);
	}

	public void receiveDamage(uint damage)
	{
		m_HP -= (int) damage;
		if (m_HP <= 0)
		{
			DestroyThis();
		}
	}

	public abstract Type type();

	public abstract uint getStartHP();
	public abstract float getSpeed();
	public abstract Color getColor();
	
	TDGrid.Cell[] m_path;
	
	int m_maxHP;
	int m_HP;
	GameObject m_healthBar;
}
