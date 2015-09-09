using UnityEngine;
using System.Collections;
using System;

public class MapGenerator : MonoBehaviour {
	
	public int width;
	public int height;

	public int ABStart = 1;
	public int FBStart = 2;
	public int PBStart = 3;
	public int FiStart = 25;
	public int BBStart = 42;
	public int OBStart = 28;

	public string seed;
	public bool useRandomSeed;
	
	[Range(0,100)]
	public int randomFillPercent;
	
	int[,] map;
	
	void Start() {
		GenerateMap();
	}
	
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			GenerateMap();
		}



	}
	
	void GenerateMap() {
		map = new int[width,height];
		RandomFillMap();
		
		for (int i = 0; i < 100; i ++) {
			SmoothMap();
		}
	}
	
	
	void RandomFillMap() {
		if (useRandomSeed) {
			seed = Time.time.ToString();
		}

		FBStart += ABStart;
		PBStart += FBStart;
		FiStart += PBStart;
		BBStart += FiStart;
		OBStart += BBStart;

		System.Random pseudoRandom = new System.Random(seed.GetHashCode());
		
		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				int auto = pseudoRandom.Next(0,100);
				if (auto <= ABStart) {map[x,y]=1;}
				if (auto <= FBStart && auto > ABStart) {map[x,y]=2;}
				if (auto <= PBStart && auto > FBStart) {map[x,y]=3;}
				if (auto <= FiStart && auto > PBStart) {map[x,y]=4;}
				if (auto <= BBStart && auto > FiStart) {map[x,y]=5;}
				if (auto <= OBStart && auto > BBStart) {map[x,y]=0;}
			}
		}
	}
	
	void SmoothMap() {
		System.Random random = new System.Random();
		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				int neighbourWallTiles = GetSurroundingWallCount(x,y);
				if (neighbourWallTiles >= 2){
					continue;}
				else{
					int val = random.Next(-1,1);
					int val2 = random.Next(-1,1);
					if ((x+val > 0) && (val2+y > 0)) {
						map[x,y]= map[x+val, y+val2];

					}
				}
			}
		}
	}
	
	int GetSurroundingWallCount(int gridX, int gridY) {
		int wallCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
				if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height) {
					int other = map[neighbourX, neighbourY];
					int value = map[gridX, gridY];
					if (value == other){wallCount++;}
					else {continue;}	
				}
			}
		}
		//Debug.Log (wallCount);
		return wallCount;
	}
	
	
	void OnDrawGizmos() {
		if (map != null) {
			for (int x = 0; x < width; x ++) {
				for (int y = 0; y < height; y ++) {
					if (map[x,y]==0) {Gizmos.color = Color.white;}
					if (map[x,y]==1) {Gizmos.color = Color.red;}
					if (map[x,y]==2) {Gizmos.color = Color.blue;}
					if (map[x,y]==3) {Gizmos.color = Color.yellow;}
					if (map[x,y]==4) {Gizmos.color = Color.green;}
					if (map[x,y]==5) {Gizmos.color = Color.grey;}
					Vector3 pos = new Vector3(-width/2 + x + .5f,0, -height/2 + y+.5f);
					int neighbourWallTiles = GetSurroundingWallCount(x,y);
					Vector3 size = new Vector3(1,neighbourWallTiles*2,1);
					Gizmos.DrawCube(pos,size);
				}
			}
		}
	}
	
}