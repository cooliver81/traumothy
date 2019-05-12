using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour {

    // Game variables
    int mapsize = 10;
    int numberTurns = 100;
    int startingPos;

    // List of prefabs maptiles
    [SerializeField] GameObject[] rooms;
    [SerializeField] GameObject[] roomsR;

    // Prefabs by type
    [SerializeField] GameObject[] corridorsT;
    [SerializeField] GameObject[] corridorsT2;
    [SerializeField] GameObject[] corridorsB;
    [SerializeField] GameObject[] corridorsL;
    [SerializeField] GameObject[] corridorsR;

    // Unique maptiles
    [SerializeField] GameObject cap1;
    [SerializeField] GameObject cap2;
    [SerializeField] GameObject cap3;
    [SerializeField] GameObject cap4;
    GameObject entrance;

    [SerializeField] GameObject longCorridor;

    GameObject[,] map;
    Transform origin;

    struct MapPosition {
        public int row; // the z position
        public int column; // the x position

        public MapPosition(int p1, int p2){
            row = p1;
            column = p2;
        }
    }

	// Use this for initialization
	void Start () {
        // Entrance (first room)
        entrance = GameObject.Find("Entrance");
        origin = entrance.transform;
        startingPos = (mapsize - 1)/2;

        // Initialize map
        map = new GameObject[mapsize, mapsize];
        map[startingPos, 0] = entrance;

        // Start procedural algorithm 
        GameObject toInstantiate = findMapTileForDirection(entrance.transform.GetChild(0).GetComponent<JointPoints>().openingNeeded, false);
        proceduralAlgo(new MapPosition(startingPos, 1), toInstantiate, numberTurns);
    }

    void proceduralAlgo(MapPosition mapPosition, GameObject toInstantiate, int turns) {

        // Instantiate maptile
        GameObject maptile = instantiatePrefab(mapPosition, toInstantiate);

        // Check if we still want to instantiate corridors
        Debug.Log("Number of turns: " + turns);
        if (turns != 0)
        {
            // Decide random number of corridors to instantiate
            int corridorsToInstantiate = Random.Range(1, maptile.transform.GetChild(0).transform.childCount - 2);
            //Debug.Log("Corridors to instantiate: " + corridorsToInstantiate);
            bool instantiatingDoors = false;
            // Check for each jointpoint if it can open a new corridor
            foreach (Transform jointpoints in maptile.transform.GetChild(0).transform)
            {
                // Check if all corridors were spawned, if yes, spawn rooms instead
                if (corridorsToInstantiate == 0)
                {
                    instantiatingDoors = true;
                }
                // Check if tile next to it is empty
                if (checkIfAdjacentIsEmpty(mapPosition.row, mapPosition.column, jointpoints.gameObject.GetComponent<JointPoints>().openingDirection))
                {
                    //Debug.Log("ADJACENT FOR: row: " + mapPosition.row + " col: " + mapPosition.column + " direction: " + jointpoints.gameObject.GetComponent<JointPoints>().openingDirection);
                    //Debug.Log("Found an adjacent grid cell. Jointpoint: " + jointpoints.gameObject.name);

                    // find a prefab to instantiate
                    GameObject newMaptile = findMapTileForDirection(openingNeeded(jointpoints.gameObject.GetComponent<JointPoints>().openingDirection), instantiatingDoors);

                    // Get the position of this new corridor in the map
                    MapPosition spawnPosition = getSpawnPosition(mapPosition.row, mapPosition.column, jointpoints.gameObject.GetComponent<JointPoints>().openingDirection);
                    Debug.Log("Next position in map: row: " + spawnPosition.row + " column: " + spawnPosition.column);
                    // Recursive call to procedural algorithm
                    proceduralAlgo(spawnPosition, newMaptile, turns - 1);
                    corridorsToInstantiate--;
                }
                else if (checkIfAdjacentOutside(mapPosition.row, mapPosition.column, jointpoints.gameObject.GetComponent<JointPoints>().openingDirection))
                {
                    instantiateWall(jointpoints.gameObject.GetComponent<JointPoints>().openingDirection, jointpoints);
                }
            }
        } else {
            // Add rooms 
            // Check for each jointpoint if it can open a new corridor
            foreach (Transform jointpoints in maptile.transform.GetChild(0).transform)
            {
                // Check if tile next to it is empty 
                if (checkIfAdjacentIsEmpty(mapPosition.row, mapPosition.column, jointpoints.gameObject.GetComponent<JointPoints>().openingDirection))
                {
                    // INSTANTIATE A ROOM
                    Debug.Log("ADJACENT FOR: row: " + mapPosition.row + " col: " + mapPosition.column + " direction: " + jointpoints.gameObject.GetComponent<JointPoints>().openingDirection);
                    Debug.Log("Found an adjacent grid cell. Jointpoint: " + jointpoints.gameObject.name);
                    // find a prefab to instantiate
                    GameObject newMaptile = findMapTileForDirection(openingNeeded(jointpoints.gameObject.GetComponent<JointPoints>().openingDirection), true);
                    // Get the position of this new corridor in the map
                    MapPosition spawnPosition = getSpawnPosition(mapPosition.row, mapPosition.column, jointpoints.gameObject.GetComponent<JointPoints>().openingDirection);
                    Debug.Log("Next position in map: row: " + spawnPosition.row + " column: " + spawnPosition.column);
                    // Instantiate maptile
                    instantiatePrefab(spawnPosition, newMaptile);
                }
                
            }
            return;
        }   
    }

    GameObject findMapTileForDirection(int direction, bool searchingRooms) {

        if (searchingRooms){
            foreach (GameObject room in rooms) { // rooms have only 1 jointpoint (door)
                if(direction == 2)
                    return corridorsB[Random.Range(0, 2)];
                if (room.transform.GetChild(0).GetComponent<JointPoints>().openingDirection == direction) {
                    return room;
                }
            }
        } else { // Searching corridors
            if (direction == 1) {
                return corridorsB[Random.Range(0, corridorsB.Length)];
            } else if (direction == 2) {
                return corridorsT[Random.Range(0, corridorsT.Length)];
            } else if (direction == 3) {
                return corridorsL[Random.Range(0, corridorsL.Length)];
            } else {
                return corridorsR[Random.Range(0, corridorsR.Length)];
            }      
        }
        return null;
    }

    bool checkIfAdjacentIsEmpty(int row, int col, int direction) {

        // Check if outside edge of map, instantiate a door
        if (direction == 1) {
            if (row == mapsize - 1) {
                return false;
            }else if (map[row + 1,col] != null) {
                return false;
            }else{
                return true;
            }
        } else if (direction == 2) {
            if (row == 0) {
                return false;
            }else if (map[row - 1, col] != null) {
                return false;
            }else{
                return true;
            }
        } else if (direction == 3) {
            if (col == 0){
                return false;
            }else if (map[row, col - 1] != null){
                return false;
            } else{
                return true;
            }
        } else {
            if (col == mapsize - 1) {
                return false;
            } else if (map[row, col + 1] != null){
                return false;
            } else{
                return true;
            }
        } 
    }

    bool checkIfAdjacentOutside(int row, int col, int direction)
    {
        // Check if outside edge of map, instantiate a door
        if (direction == 1) {
            if (row == mapsize - 1){
                return true;
            }
        } else if (direction == 2){
            if (row == 0){
                return true;
            }
        }else if (direction == 3) {
            if (col == 0){
                return true;
            }
        }else{
            if (col == mapsize - 1){
                return true;
            }       
        }
        return false;
    }

    MapPosition getSpawnPosition(int row, int col, int direction) {
        // Check if outside edge of map, instantiate a door
        if (direction == 1) {
            return new MapPosition(row + 1, col);
        }
        else if (direction == 2) {
            return new MapPosition(row - 1,  col);
        }
        else if (direction == 3) {
            return new MapPosition(row, col - 1);
        }
        else {
            return new MapPosition(row, col + 1);
        } 
    }

    int openingNeeded(int openingDirection ) {
        switch (openingDirection) {
            case 1:
                return 2;
            case 2:
                return 1;
            case 3:
                return 4;
            case 4:
                return 3;
            default:
                return 0;
        }
    }

    GameObject instantiatePrefab(MapPosition position, GameObject prefab) {

        GameObject newObject;
        if (prefab == longCorridor)
        {
            Vector3 offset = new Vector3(position.column * 2, 0, (((startingPos) - position.row) * 2)+2);
            newObject = Instantiate(prefab, origin.position + offset, prefab.transform.rotation);
            map[position.row, position.column] = newObject;
            map[position.row+1, position.column] = newObject;
            map[position.row+2, position.column] = newObject;
        }
        else {
            Vector3 offset = new Vector3(position.column * 2, 0, ((startingPos) - position.row) * 2);
            newObject = Instantiate(prefab, origin.position + offset, prefab.transform.rotation);
            map[position.row, position.column] = newObject;
        }

        // Debug.Log("Offset: " + offset);
        return newObject;
    }

    void instantiateWall(int direction, Transform jp) {
        // Check if outside edge of map, instantiate a door
        if (direction == 1) {
            Instantiate(cap1, jp.position, cap1.transform.rotation);
        } else if (direction == 2){
            Instantiate(cap2, jp.position, cap2.transform.rotation);
        }
        else if (direction == 3) {
            Instantiate(cap3, jp.position, cap3.transform.rotation);
        }
        else{
            Instantiate(cap4, jp.position, cap4.transform.rotation);
        }
    }

}
