using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class MapGenerator : MonoBehaviour {
	//Declarations
	//public int width;
	//public int height;

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
	
	//int[,] map;

	//Francisco
	public int M = 5;
	public int N = 10;

	int[,] gut;
	





	void Start() {
		GenerateMap();
	}
	
	void Update() {
		//if (Input.GetMouseButtonDown(0)) {
		//	GenerateMap();
		//}



	}
	
	void GenerateMap() {
		//map = new int[width,height];
		gut = new int[M, N];
		StartCoroutine(FranSetup());
		//RandomFillMap();
		
		//for (int i = 0; i < 100; i ++) {
		//	SmoothMap();
		//}
	}


	IEnumerator FranSetup () {

		Debug.Log ("Starting");
		// Populations
		int pop1 = 0; // Actinobacteria
		int pop2 = 0; // Fusobacteria
		int pop3 = 0; // Proteobacteria
		int pop4 = 0; // Firimicutes
		int pop5 = 0; // Bacteroidetes
		int pop6 = 0; // Other
		// sum of populations
		int popTotal;
		
		double noChange = 1.0; // the variable on which the change is predicated
		double selfConstant = 1.0; // factor that contributes to stability
		double otherConstant = 1.0; // factor that contributes to change
		double tippingPoint = 1.0; // determines result beyond which cells change or not change
		int self; // identity of species at a particular location
		List<int> surroundings = new List<int>(8); // vector represents the 8 surrounding elements, set to zero
		//vector<double> changePotentials(6,0.0); // each of these six numbers represents a likelihood of an element changing into another
		double changeInto = 1.0; // the constant by which the likelihood of an element changing int another is multiplied for every time such an element is encountered in the surroundings
		double inulin = 0.0;
		double inulinDegradationConstant = 1.0;
		
		//Prokao contents
		double inulinAdditionConstant = 0.0;
		int additionConstant1 = 0;
		int additionConstant2 = 0;
		int additionConstant3 = 0;
		int additionConstant4 = 0;
		int additionConstant5 = 0;
		int additionConstant6 = 0;

		// fitness values: strength is the ability to appear, durability is the ability to not disappear. Also, influence of inulin
		double s1 = 1; // Actinobacteria
		double s2 = 1; // Fusobacteria
		double s3 = 1; // Proteobacteria
		double s4 = 1; // Firimicutes
		double s5 = 1; // Bacteroidetes
		double s6 = 1; // Other
		
		double d1 = 1; // Actinobacteria
		double d2 = 1; // Fusobacteria
		double d3 = 1; // Proteobacteria
		double d4 = 1; // Firimicutes
		double d5 = 1; // Bacteroidetes
		double d6 = 1; // Other
		
		double i1 = (1 + inulin); // Actinobacteria
		double i2 = 1; // Fusobacteria
		double i3 = 1; // Proteobacteria
		double i4 = (1 + inulin); // Firimicutes
		double i5 = 1; // Bacteroidetes
		double i6 = 1; // Other

		
		
		// cooperation constants (can later be mafe into functions) - these define whether there is a cooperative or competitive effect between members of the same species inhabiting the gut
		
		double c1; //= 1 + (pop1/popTotal);
		double c2; //= 1 + (pop2/popTotal);
		double c3; //= 1 + (pop3/popTotal);
		double c4; //= 1 +  (pop4/popTotal);
		double c5; //= 1 + (pop5/popTotal);
		double c6; //= 1 +  (pop6/popTotal);
		// These are defined within the loop in order for prokao to account for the changing pop values



		if (useRandomSeed) {
			seed = Time.time.ToString();
		}
		System.Random RandomNumbers = new System.Random(seed.GetHashCode());

		for (int r = 0; r < M; r++)
		{
			for (int c = 0; c < N; c++)
			{
				int i = (RandomNumbers.Next() % 500);
				
				if (i == 0)
				{
					gut[r, c] = 1;
				}
				
				else if ((i > 0) && (i <= 9))
				{
					gut[r, c] = 2;
				}
				
				else if ((i > 9) && (i <= 24))
				{
					gut[r, c] = 3;
				}
				
				else if ((i > 24) && (i <= 149))
				{
					gut[r, c] = 4;
				}
				
				else if ((i > 149) && (i <= 359))
				{
					gut[r, c] = 5;
				}
				
				else if ((i > 359) && (i <= 499))
				{
					gut[r, c] = 6;
				}
			}
			
		}
		
		Debug.Log("t = 0");
		Debug.Log("\n");
		
		// print gut
		for (int r = 0; r < M; r++)
		{
			for (int c = 0; c < N; c++)
			{
				Debug.Log(gut[r, c]);
			}
			Debug.Log("\n");
		}
		Debug.Log(" ");
		Debug.Log("\n");

		
		// obtain the number of bacteria in each of the initial populations
		
		for (int r = 0; r < M; r++)
		{
			for (int c = 0; c < N; c++)
			{
				switch (gut[r, c])
				{
				case 1 :
					pop1++;
					break;
				case 2 :
					pop2++;
					break;
				case 3 :
					pop3++;
					break;
				case 4 :
					pop4++;
					break;
				case 5 :
					pop5++;
					break;
				case 6 :
					pop6++;
					break;
				}
			}
		}
		
		popTotal = pop1 + pop2 + pop3 + pop4 + pop5 + pop6;
		
		
		Debug.Log("Actinobacteria: ");
		Debug.Log(pop1);
		Debug.Log("\n");
		Debug.Log("Fusobacteria: ");
		Debug.Log(pop2);
		Debug.Log("\n");
		Debug.Log("Proteobacteria: ");
		Debug.Log(pop3);
		Debug.Log("\n");
		Debug.Log("Firmicutes: ");
		Debug.Log(pop4);
		Debug.Log("\n");
		Debug.Log("Bacteroidetes: ");
		Debug.Log(pop5);
		Debug.Log("\n");
		Debug.Log("Other: ");
		Debug.Log(pop6);
		Debug.Log("\n");
		Debug.Log("Total Population: ");
		Debug.Log(popTotal);
		Debug.Log("\n");
		
		//calculating and printing percentage populations
		
		double percentagePop1 = ((double)pop1 / popTotal * 100);
		double percentagePop2 = ((double)pop2 / popTotal * 100);
		double percentagePop3 = ((double)pop3 / popTotal * 100);
		double percentagePop4 = ((double)pop4 / popTotal * 100);
		double percentagePop5 = ((double)pop5 / popTotal * 100);
		double percentagePop6 = ((double)pop6 / popTotal * 100);
		
		Debug.Log("% Actinobacteria: ");
		Debug.Log(percentagePop1);
		Debug.Log("\n");
		Debug.Log("% Fusobacteria: ");
		Debug.Log(percentagePop2);
		Debug.Log("\n");
		Debug.Log("% Proteobacteria: ");
		Debug.Log(percentagePop3);
		Debug.Log("\n");
		Debug.Log("% Firmicutes: ");
		Debug.Log(percentagePop4);
		Debug.Log("\n");
		Debug.Log("% Bacteroidetes: ");
		Debug.Log(percentagePop5);
		Debug.Log("\n");
		Debug.Log("% Other: ");
		Debug.Log(percentagePop6);
		Debug.Log("\n");
		Debug.Log("  ");
		Debug.Log("\n");
		
		
		
		
		
		var tempGut = new int[M,N]; //introduce a provisional gut inside the loop so that all change is simultaneous within the gut (following a time series)
		
		for (int i = 1; i < 100; i++)
		{
			yield return new WaitForSeconds(5);

			tempGut = gut;
			
			
			inulin *= inulinDegradationConstant; // first-order degradation of inulin with respect to time
			
			if (i % 10 == 3)
			{ //daily dosing of prokao
				inulin += inulinAdditionConstant;
				pop1 += additionConstant1;
				pop2 += additionConstant2;
				pop3 += additionConstant3;
				pop4 += additionConstant4;
				pop5 += additionConstant5;
				pop6 += additionConstant6;
			}
			popTotal = pop1 + pop2 + pop3 + pop4 + pop5 + pop6;
			
			
			c1 = 1; //+ (double(pop1)/popTotal);
			c2 = 1; //+ (double(pop2)/popTotal);
			c3 = 1; //+ (double(pop3)/popTotal);
			c4 = 1; //+  (double(pop4)/popTotal);
			c5 = 1; //+ (double(pop5)/popTotal);
			c6 = 1; //+  (double(pop6)/popTotal);
			
			
			for (int r = 0; r < M; r++)
			{
				for (int c = 0; c < N; c++)
				{
					
					self = tempGut[r,c];
					surroundings[0] = tempGut[r - 1,c - 1];
					surroundings[1] = tempGut[r - 1,c];
					surroundings[2] = tempGut[r - 1,c + 1];
					surroundings[3] = tempGut[r,c - 1];
					surroundings[4] = tempGut[r,c + 1];
					surroundings[5] = tempGut[r + 1,c - 1];
					surroundings[6] = tempGut[r + 1,c];
					surroundings[7] = tempGut[r + 1,c + 1];
					
					noChange = 1.0;
					
					
					if (surroundings[0] == self)
					{
						noChange *= selfConstant;
					}
					else
					{
						noChange *= otherConstant;
					}
					
					if (surroundings[1] == self)
					{
						noChange *= selfConstant;
					}
					else
					{
						noChange *= otherConstant;
					}
					
					if (surroundings[2] == self)
					{
						noChange *= selfConstant;
					}
					else
					{
						noChange *= otherConstant;
					}
					
					if (surroundings[3] == self)
					{
						noChange *= selfConstant;
					}
					else
					{
						noChange *= otherConstant;
					}
					
					if (surroundings[4] == self)
					{
						noChange *= selfConstant;
					}
					else
					{
						noChange *= otherConstant;
					}
					
					if (surroundings[5] == self)
					{
						noChange *= selfConstant;
					}
					else
					{
						noChange *= otherConstant;
					}
					
					if (surroundings[6] == self)
					{
						noChange *= selfConstant;
					}
					else
					{
						noChange *= otherConstant;
					}
					
					if (surroundings[7] == self)
					{
						noChange *= selfConstant;
					}
					else
					{
						noChange *= otherConstant;
					}
					
					switch (tempGut[r,c])
					{
					case 1:
						noChange *= d1;
						break;
					case 2:
						noChange *= d2;
						break;
					case 3:
						noChange *= d3;
						break;
					case 4:
						noChange *= d4;
						break;
					case 5:
						noChange *= d5;
						break;
					case 6:
						noChange *= d6;
						break;
					}
					
					if (noChange > tippingPoint)
					{
						gut[r, c] = tempGut[r,c];
					}
					else
					{
						List<double> changePotentials = new List<double>(6);
						changePotentials[0]=1.0;
						changePotentials[1]=1.0;
						changePotentials[2]=1.0;
						changePotentials[3]=1.0;
						changePotentials[4]=1.0;
						changePotentials[4]=1.0;
						
						for (int s = 0; s < 8; s++)
						{
							switch (surroundings[s])
							{
							case 1:
								changePotentials[0] *= (changeInto * s1 * c1 * i1);
								break;
							case 2:
								changePotentials[1] *= (changeInto * s2 * c2 * i2);
								break;
							case 3:
								changePotentials[2] *= (changeInto * s3 * c3 * i3);
								break;
							case 4:
								changePotentials[3] *= (changeInto * s4 * c4 * i4);
								break;
							case 5:
								changePotentials[4] *= (changeInto * s5 * c5 * i5);
								break;
							case 6:
								changePotentials[5] *= (changeInto * s6 * c6 * i6);
								break;
							}
						}
						
						for (int l = 0; l < 6; l++)
						{
							changePotentials[l] *= 100000.0;

						}

						int sumPotentials;
						sumPotentials = Convert.ToInt16(changePotentials[0] + changePotentials[1] + changePotentials[2] + changePotentials[3] + changePotentials[4] + changePotentials[5]);
						int x;
						x = RandomNumbers.Next() % sumPotentials;
						if (x <= changePotentials[0])
						{
							gut[r, c] = 1;
						}
						else if (x <= (changePotentials[0] + changePotentials[1]))
						{
							gut[r, c] = 2;
						}
						else if (x <= (changePotentials[0] + changePotentials[1] + changePotentials[2]))
						{
							gut[r, c] = 3;
						}
						else if (x <= (changePotentials[0] + changePotentials[1] + changePotentials[2] + changePotentials[3]))
						{
							gut[r, c] = 4;
						}
						else if (x <= (changePotentials[0] + changePotentials[1] + changePotentials[2] + changePotentials[3] + changePotentials[4]))
						{
							gut[r, c] = 5;
						}
						else if (x <= (changePotentials[0] + changePotentials[1] + changePotentials[2] + changePotentials[3] + changePotentials[4] + changePotentials[5]))
						{
							gut[r, c] = 6;
						}
						
						
					}
					
				}
				
			}
			
			
			Debug.Log("t = ");
			Debug.Log(i);
			Debug.Log("\n");
			if (i % 3 == 0)
			{
				Debug.Log("prokao is given");
				Debug.Log("\n");
			}
			
			for (int r = 0; r < M; r++)
			{
				for (int c = 0; c < N; c++)
				{
					Debug.Log(gut[r, c]);
				}
				Debug.Log("\n");
			}
			
			Debug.Log("  ");
			Debug.Log("\n");
			
			
			// Resetting populations
			pop1 = 0; // Actinobacteria
			pop2 = 0; // Fusobacteria
			pop3 = 0; // Proteobacteria
			pop4 = 0; // Firimicutes
			pop5 = 0; // Bacteroidetes
			pop6 = 0; // Other
			
			
			
			// obtain the number of bacteria in each of the populations at t=i
			
			for (int r = 0; r < M; r++)
			{
				for (int c = 0; c < N; c++)
				{
					switch (gut[r, c])
					{
					case 1 :
						pop1++;
						break;
					case 2 :
						pop2++;
						break;
					case 3 :
						pop3++;
						break;
					case 4 :
						pop4++;
						break;
					case 5 :
						pop5++;
						break;
					case 6 :
						pop6++;
						break;
					}
				}
			}
			// sum of initial populations
			popTotal = pop1 + pop2 + pop3 + pop4 + pop5 + pop6;
			
			Debug.Log("Inulin: ");
			Debug.Log(inulin);
			Debug.Log("\n");
			Debug.Log("Actinobacteria: ");
			Debug.Log(pop1);
			Debug.Log("\n");
			Debug.Log("Fusobacteria: ");
			Debug.Log(pop2);
			Debug.Log("\n");
			Debug.Log("Proteobacteria: ");
			Debug.Log(pop3);
			Debug.Log("\n");
			Debug.Log("Firmicutes: ");
			Debug.Log(pop4);
			Debug.Log("\n");
			Debug.Log("Bacteroidetes: ");
			Debug.Log(pop5);
			Debug.Log("\n");
			Debug.Log("Other: ");
			Debug.Log(pop6);
			Debug.Log("\n");
			Debug.Log("Total Population: ");
			Debug.Log(popTotal);
			Debug.Log("\n");
			
			percentagePop1 = ((double)pop1 / popTotal * 100);
			percentagePop2 = ((double)pop2 / popTotal * 100);
			percentagePop3 = ((double)pop3 / popTotal * 100);
			percentagePop4 = ((double)pop4 / popTotal * 100);
			percentagePop5 = ((double)pop5 / popTotal * 100);
			percentagePop6 = ((double)pop6 / popTotal * 100);
			
			Debug.Log("% Actinobacteria: ");
			Debug.Log(percentagePop1);
			Debug.Log("\n");
			Debug.Log("% Fusobacteria: ");
			Debug.Log(percentagePop2);
			Debug.Log("\n");
			Debug.Log("% Proteobacteria: ");
			Debug.Log(percentagePop3);
			Debug.Log("\n");
			Debug.Log("% Firmicutes: ");
			Debug.Log(percentagePop4);
			Debug.Log("\n");
			Debug.Log("% Bacteroidetes: ");
			Debug.Log(percentagePop5);
			Debug.Log("\n");
			Debug.Log("% Other: ");
			Debug.Log(percentagePop6);
			Debug.Log("\n");
			Debug.Log("  ");
			Debug.Log("\n");
			
		}
	}




	/*void RandomFillMap() {
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
	}*/
	/*
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
	}*/


	int GetSurroundingWallCount(int gridX, int gridY) {
		int wallCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
				if (neighbourX >= 0 && neighbourX < M && neighbourY >= 0 && neighbourY < N) {
					int other = gut[neighbourX, neighbourY];
					int value = gut[gridX, gridY];
					if (value == other){wallCount++;}
					else {continue;}	
				}
			}
		}
		//Debug.Log (wallCount);
		return wallCount;
	}

	void OnDrawGizmos() {
		if (gut != null) {
			for (int x = 0; x < M; x ++) {
				for (int y = 0; y < N; y ++) {
					if (gut[x,y]==0) {Gizmos.color = Color.white;}
					if (gut[x,y]==1) {Gizmos.color = Color.green;}
					if (gut[x,y]==2) {Gizmos.color = Color.blue;}
					if (gut[x,y]==3) {Gizmos.color = Color.yellow;}
					if (gut[x,y]==4) {Gizmos.color = Color.red;}
					if (gut[x,y]==5) {Gizmos.color = Color.cyan;}
					Vector3 pos = new Vector3(-M/2 + x + .5f,0, -N/2 + y+.5f);
					int neighbourWallTiles = GetSurroundingWallCount(x,y);
					Vector3 size = new Vector3(1,neighbourWallTiles*2,1);
					Gizmos.DrawCube(pos,size);
				}
			}
		}
	}
	
}