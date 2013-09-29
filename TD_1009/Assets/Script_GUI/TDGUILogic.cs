using UnityEngine;
using System.Collections;

public class TDGUILogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		m_mode = Mode.eNone;
		GameObject player = TDWorld.getWorld().getPlayer();
		TDWorld.getWorld().addHero(player.transform.position);
	}
	
	// Update is called once per frame
	void Update () {

		TDWorld world = TDWorld.getWorld();
		TDPlayer tdPlayer = world.getTDPlayer();
		if (tdPlayer.health() <= 0)
		{
			Application.LoadLevel("GameOver");
		}

		if (Mode.eNone == m_mode)
			return;
		if (Input.GetMouseButtonDown(0))
		{


			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(mouseRay, out hit))
			{
				if (hit.transform.gameObject.Equals(GameObject.Find("Terrain")))
				{
					Vector3 pos = hit.point;
					pos = TDWorld.getWorld().truncate3d(pos);
					GameObject newTower;
					if (TDGrid.CellState.eFree == world.positionState(pos))
					{
						switch (m_mode)
						{
							case Mode.eArcher:
								newTower = world.addTower(TDTower.Type.eArrowTower, pos);
								break;
							case Mode.eCanon:
								newTower = world.addTower(TDTower.Type.eCanonTower, pos);
								break;
							default:
								return;
						}
						if (newTower != null)
						{
							TDTower tdTower = world.getTDTower(newTower);
							if (!tdPlayer.affords(tdTower.price()))
							{
								DestroyObject(newTower);
								return;
							}
							tdPlayer.expense(tdTower.price());
							TDWorld.getWorld().occupyPosition(pos, TDGrid.CellState.eBusy);
						}
					}
				}
			}
		}
	}

	void OnGUI ()
	{
		GUI.Box(new Rect(20, 20, 300, 80), "Player");

		string healthString;
		healthString = TDWorld.getWorld().getTDPlayer().health() + " HP";
		GUI.Label(new Rect(30, 50, 50, 20), healthString);

		string moneyString;
		moneyString = TDWorld.getWorld().getTDPlayer().money() + " $";
		GUI.Label(new Rect(130, 50, 50, 20), moneyString);

		GUI.Box(new Rect(800, 500, 500, 80), "Towers");

		if (GUI.Button(new Rect(850, 530, 80, 20), "Archer"))
		{
			m_mode = Mode.eArcher;
		}

		if (GUI.Button(new Rect(950, 530, 80, 20), "Canonier"))
		{
			m_mode = Mode.eCanon;	
		}
	}
	
	bool inBuildingMode()
	{
		switch (m_mode)
		{
			case Mode.eArcher:
			case Mode.eCanon:
				return true;
			default:
				return false;
		}
	}

	enum Mode
	{
		eNone = 0,
		eArcher = 1,
		eCanon = 2
	}

	Mode m_mode;
}
