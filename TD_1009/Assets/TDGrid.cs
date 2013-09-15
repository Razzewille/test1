using UnityEngine;
using System.Collections.Generic;

public class TDGrid
{
	public TDGrid()
	{
		m_ncx = m_ncy = 1;
		reallocate();
		m_length = m_width = 1f;
		m_startX = m_startY = 0;
	}

	public bool initialize(uint nbCellsX, uint nbCellsY,
		float startX, float startY, float length, float width)
	{
		m_ncx = nbCellsX;
		m_ncy = nbCellsY;
		m_startX = startX;
		m_startY = startY;
		m_length = length;
		m_width  = width;
		reallocate();
		recalcGrid();
		return true;
	}

	public uint nbCellsX
	{
		get {return m_ncx;}
		set {m_ncx = value; reallocate(); recalcGrid();}
	}
	public uint nbCellsY
	{
		get {return m_ncy;}
		set {m_ncy = value; reallocate(); recalcGrid();}
	}

	public float length
	{
		get {return m_length;}
		set {m_length = value; recalcGrid();}
	}

	public float width
	{
		get {return m_width;}
		set {m_width = value; recalcGrid();}
	}

	public enum CellState {eFree = 0, eBusy, ePlayer, eEnemyRespawn};
	public struct Cell
	{
		Cell(uint i, uint j) {m_i = i; m_j = j; pos = new Vector2();}
		public Vector2 pos;
		public uint m_i, m_j;
	}

	public CellState cellState(Cell cell)
	{
		if (cell.m_i < 0 || cell.m_i >= m_ncx)
			return CellState.eBusy;
		if (cell.m_j < 0 || cell.m_j >= m_ncy)
			return CellState.eBusy;
		return m_aCells[cell.m_i, cell.m_j];
	}

	public void setCellState(Cell cell, CellState state)
	{
		m_aCells[cell.m_i, cell.m_j] = state;
	}

	public Vector2 getCenter(Cell cell)
	{
		float cx = m_gridX*((float)(cell.m_i) + 0.5f) + m_startX;
		float cy = m_gridY*((float)(cell.m_j) + 0.5f) + m_startY;
		return new Vector2(cx, cy);
	}

	public Cell getCell(Vector2 pos)
	{
		Cell cell = new Cell();
		pos.x -= m_startX;
		if (pos.x < 0f)
			cell.m_i = 0;
		else			
			cell.m_i = (uint)((pos.x/m_gridX));
		pos.y -= m_startY;
		if (pos.y < 0f)
			cell.m_j = 0;
		else			
			cell.m_j = (uint)((pos.y/m_gridX));
		return cell;
	}

	class PathNode
	{
		public Cell m_cell;
		public Cell? m_parentCell;

		public int m_stepsFromStart; 
		public int m_heuristicRating;
		public int m_priority // less is better
		{
			get
			{
				return m_stepsFromStart + m_heuristicRating;
			}
		}
	}

	//Gets neighborhoods if exists
	List<Cell> getSuccessors(Cell sourceCell)
	{
		sbyte[,] direction = new sbyte[4, 2] { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };
		List<Cell> successors = new List<Cell>();

		for (int i = 0; i < 4; ++i)
		{
			int posI = (int)sourceCell.m_i + direction[i, 0];
			int posJ = (int)sourceCell.m_j + direction[i, 1];

			if ( (0 <= posI) && (0 <= posJ) && (m_ncx > posI) && (m_ncy > posJ))
			{
				if (CellState.eFree != m_aCells[posI, posJ])
					continue;

				Cell cell = new Cell();
				cell.m_i = (uint)posI;
				cell.m_j = (uint)posJ;
				successors.Add(cell);
			}
		}

		return successors;
	}

	// EuclideanNoSQR distance to end cell
	int heuristicRating(Cell curCell, Cell endCell)
	{
		int di = (int)(curCell.m_i - endCell.m_i);
		int dj = (int)(curCell.m_j - endCell.m_j);
		int rating = di*di + dj*dj;
		return rating;
	}

	bool buildPath(Cell startCell, Cell endCell, out Cell[] path)
	{
		List<PathNode> closedCells = new List<PathNode>();
		SortedDictionary<int, PathNode> openCells = new SortedDictionary<int, PathNode>(); // int - priority
		
		PathNode startPath = new PathNode();
		startPath.m_cell = startCell;
		startPath.m_parentCell = null;
		startPath.m_stepsFromStart = 0;
		startPath.m_heuristicRating = heuristicRating(startCell, endCell);
		openCells.Add(startPath.m_priority, startPath);

		bool pathFound = false;
		PathNode endPath = null;

		while (0 < openCells.Count)
		{
			KeyValuePair<int, PathNode> priorityPath = openCells.GetEnumerator().Current;
			PathNode parentNode = priorityPath.Value;

			closedCells.Add(parentNode);
			openCells.Remove(priorityPath.Key);
			if (endCell.Equals(parentNode.m_cell))
			{
				pathFound = true;
				endPath = parentNode;
				break;
			}

			List<Cell> successors = getSuccessors(parentNode.m_cell);
			foreach (Cell nextCell in successors)
			{
				PathNode nextPath = new PathNode();
				nextPath.m_cell = nextCell;
				nextPath.m_parentCell = parentNode.m_cell;
				nextPath.m_stepsFromStart = parentNode.m_stepsFromStart + 1;

				bool cellProcessed = false;
				foreach (KeyValuePair<int, PathNode> iterator in openCells)
				{
					PathNode openedNode = iterator.Value;
					if (openedNode.m_cell.Equals(nextPath.m_cell))
					{
						if (nextPath.m_stepsFromStart < openedNode.m_stepsFromStart)
						{
							openCells.Remove(iterator.Key);
						}
						else
						{
							cellProcessed = true;
						}
						break;
					}                   
				}

				if (cellProcessed)
					continue;

				foreach (PathNode closedNode in closedCells)
				{
					if (closedNode.m_cell.Equals(nextPath.m_cell))
					{
						cellProcessed = true;
						break;
					}
				}

				if (cellProcessed)
					continue;

				nextPath.m_heuristicRating = heuristicRating(nextCell, endCell);
				openCells.Add(nextPath.m_priority, nextPath);
			}
		}

		if (pathFound)
		{
			path = new Cell[closedCells.Count];
			PathNode curPath = endPath;
			for (int i = closedCells.Count - 1; i >= 0; --i)
			{
				path[i] = curPath.m_cell;

				if (!curPath.m_parentCell.HasValue)
				{
					//error in buildPath
					path = null;
					return false;
				}
				curPath.m_cell = curPath.m_parentCell.Value;
			}
			return true;
		}
		else
		{
			path = null;
			return false;
		}
	}

	void reallocate()
	{
		m_aCells = new CellState[m_ncx, m_ncy];
		for (uint i=0; i<m_ncx; i++)
			for (uint j=0; j<m_ncy; j++)
				m_aCells[i, j] = CellState.eFree;
	}

	void recalcGrid()
	{
		m_gridX = m_length/(float)m_ncy;
		m_gridY = m_length/(float)m_ncy;
	}

	CellState[,] m_aCells;

	uint m_ncx;
	uint m_ncy;

	float m_length;
	float m_width;

	public float m_startX;
	public float m_startY;
	
	public float m_gridX;
	public float m_gridY;
}
