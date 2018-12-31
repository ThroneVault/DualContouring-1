﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CircleTest : MonoBehaviour {
	
	public int i = 0;

	MeshFilter meshFilter;

	List<Edge> edges;
	Point[,,] points;
	Cell[,,] cells;

	public float cellSize = 1f;
	public int size = 10;
	public bool debug;

	IIsoSurface surface;

	// Use this for initialization
	public void start () {

		//surface = new CircleSurface(4.25f, new Vector3(5,5,5), 0.0001f);
		surface = new CircleSurface(2, new Vector3d(0.5,0.5,0.5), 0.0001f);
		
		edges = new List<Edge>();
		points = new Point[size + 1,size + 1,size + 1];
		cells = new Cell[size,size,size];

		for(int x = 0; x < size; x++)
		{
			for(int y = 0; y < size; y++)
			{
				for(int z = 0; z < size; z++)
				{
					cells[x,y,z] = new Cell(x,y,z,cellSize, points, edges, surface);
				}	
			}
		}

		meshFilter = GetComponent<MeshFilter>();

		Mesh mesh = new Mesh();
		
		List<Vector3> vertices = new List<Vector3>();
		List<Vector3> normals = new List<Vector3>();
		List<int> triangles = new List<int>();

		foreach(Edge edge in edges)
		{
			int i = edge.Draw(vertices, normals, triangles, surface);
			//Debug.Log("I: " + i);
		}

		mesh.vertices = vertices.ToArray();
		mesh.normals = normals.ToArray();
		mesh.triangles = triangles.ToArray();

		meshFilter.mesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
		if(i == 5) start();

		i++;

		if(debug)
		{
			foreach(Point point in points)
			{
				if(point != null) point.DebugDraw();
			}
			foreach(Cell cell in cells)
			{
				if(cell != null) cell.DebugDraw(surface);
			}
			foreach(Edge edge in edges)
			{
				if(edge != null) edge.DebugDraw(cellSize);
			}
		}
	}
}