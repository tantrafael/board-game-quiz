using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Board
{
	public class Test : MonoBehaviour
	{
		void Start()
		{
			// Get board layout.
			Debug.Log(boardLayoutFile);

			var boardLayoutFileContents = boardLayoutFile.text;
			var boardLayout = JsonConvert.DeserializeObject<BoardLayout>(boardLayoutFileContents);

			Debug.Log(boardLayout.ID);

			// Spawn tiles.
			var tileInstances = new List<GameObject>();

			foreach (var gridPosition in boardLayout.TilePositions)
			{
				Debug.Log(gridPosition);

				//Vector3 worldPosition = (Vector3Int)gridPosition;
				var worldPosition = new Vector3(gridPosition.x, 0.0f, gridPosition.y) * gridCellSize;
				var tileInstance = Instantiate(tile, worldPosition, Quaternion.identity);

				//GameObject tileInstance = GameObject.CreatePrimitive(PrimitiveType.Cube);
				//tileInstance.transform.position = worldPosition;

				tileInstances.Add(tileInstance);
			}

			var colorTable = new Dictionary<QuizType, Color>
			{
				{ QuizType.Question, Color.red },
				{ QuizType.Flag, Color.green }
			};

			// Specialize tiles.
			foreach (var quiz in boardLayout.Quizzes)
			{
				Debug.Log(quiz.ID);
				Debug.Log(quiz.Type);

				var color = colorTable[quiz.Type];

				foreach (var tileIndex in quiz.TileIndexes)
				{
					Debug.Log(tileIndex);

					var tileInstance = tileInstances[tileIndex];
					//var tileRenderer = tileInstance.GetComponent<Renderer>();
					//var tileRenderer = tileInstance.transform.GetChild(0).gameObject.GetComponent<Renderer>();
					var tileModel = tileInstance.transform.GetChild(0).gameObject;
					var tileRenderer = tileModel.GetComponent<Renderer>();

					tileRenderer.material.color = color;
				}
			}
		}

		public TextAsset boardLayoutFile;
		public float gridCellSize;
		public GameObject tile;
	}
}
