using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class Test : MonoBehaviour
{
	void Start()
	{
		var json = File.ReadAllText(Application.dataPath + "/Board/BoardLayout1.json");
		Debug.Log(json);

		var boardLayout = JsonConvert.DeserializeObject<BoardLayout>(json);
		Debug.Log(boardLayout.ID);
		Debug.Log(boardLayout.Positions.Count);

		foreach (var gridPosition in boardLayout.Positions)
		{
			Debug.Log(gridPosition);

			//Vector3 worldPosition = (Vector3Int)gridPosition;
			var worldPosition = new Vector3(gridPosition.x, 0, gridPosition.y) * tileSize;

			Instantiate(tile, worldPosition, Quaternion.identity);
		}
	}

	public float tileSize;
	public GameObject tile;
	public TextAsset file;
}
