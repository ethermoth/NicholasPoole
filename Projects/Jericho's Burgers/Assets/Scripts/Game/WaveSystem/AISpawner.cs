using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour {

	[SerializeField] List<GameObject> patronSpawnPoints;
	[SerializeField] List<GameObject> raiderSpawnPoints;

	public static List<GameObject> PatronSpawns { get; private set; }
	public static List<GameObject> RaiderSpawns { get; private set; }

	public enum SpawnType {
		Patron,
		Raider
	}

	void Start () {
		PatronSpawns = patronSpawnPoints;
		RaiderSpawns = raiderSpawnPoints;
	}

	public static GameObject Spawn (GameObject _AICharacter, SpawnType _SpawnType) {
		switch (_SpawnType) {
			case SpawnType.Patron:
				return Instantiate (_AICharacter, PatronSpawns[Random.Range (0, PatronSpawns.Count - 1)].transform.position, Quaternion.identity);
			case SpawnType.Raider:
				return Instantiate (_AICharacter, RaiderSpawns[Random.Range (0, RaiderSpawns.Count - 1)].transform.position, Quaternion.identity);
			default:
				return null;
		}
	}
}