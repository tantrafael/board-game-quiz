using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Board
{
	public class Main : MonoBehaviour
	{
		public TextAsset boardLayoutFile;
		public float gridCellSize;
		public GameObject tile;

		void Start()
		{
			var boardLayout = GetBoardLayout(boardLayoutFile);

			ConstructBoard(boardLayout, gridCellSize, tile);
		}

		private BoardLayout GetBoardLayout(TextAsset boardLayoutFile)
		{
			Debug.Log(boardLayoutFile);

			var boardLayoutFileContents = boardLayoutFile.text;
			var boardLayout = JsonConvert.DeserializeObject<BoardLayout>(boardLayoutFileContents);

			Debug.Log(boardLayout.ID);

			return boardLayout;
		}

		private void ConstructBoard(BoardLayout boardLayout, float gridCellSize, GameObject tile)
		{
			var tiles = ConstructTiles(boardLayout, gridCellSize, tile);

			SpecializeTiles(boardLayout, tiles);
		}

		private List<GameObject> ConstructTiles(BoardLayout boardLayout, float gridCellSize, GameObject tile)
		{
			var tiles = new List<GameObject>();

			foreach (var gridPosition in boardLayout.TilePositions)
			{
				Debug.Log(gridPosition);

				//Vector3 worldPosition = (Vector3Int)gridPosition;
				var worldPosition = new Vector3(gridPosition.x, 0.0f, gridPosition.y) * gridCellSize;
				var tileInstance = Instantiate(tile, worldPosition, Quaternion.identity);

				//GameObject tileInstance = GameObject.CreatePrimitive(PrimitiveType.Cube);
				//tileInstance.transform.position = worldPosition;

				tiles.Add(tileInstance);
			}

			return tiles;
		}

		private void SpecializeTiles(BoardLayout boardLayout, List<GameObject> tileInstances)
		{
			var colorTable = new Dictionary<QuizType, Color>
			{
				{ QuizType.Question, Color.red },
				{ QuizType.Flag, Color.green }
			};

			foreach (var quiz in boardLayout.Quizzes)
			{
				Debug.Log(quiz.ID);
				Debug.Log(quiz.Type);

				var color = colorTable[quiz.Type];

				SpecializeQuizTiles(tileInstances, quiz, color);
			}
		}

		private void SpecializeQuizTiles(List<GameObject> tileInstances, Quiz quiz, Color color)
		{
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
}
