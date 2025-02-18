using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace BoardGameQuiz
{
	public class GameBoard : MonoBehaviour
	{
		public TextAsset gameBoardLayoutFile;
		public float gridCellSize;
		public GameObject tile;

		private GameBoardLayout gameBoardLayout;

		public void Initialize()
		{
			gameBoardLayout = ConstructGameBoard(gameBoardLayoutFile, gridCellSize, tile);
		}

		/*
		public Vector3 GetStartPosition()
		{
			// TODO: Assert existing tile positions.
			var startTilePosition = gameBoardLayout.TilePositions[0];
			var startWorldPosition = GetWorldPosition(startTilePosition, gridCellSize);

			return startWorldPosition;
		}
		*/

		public Vector3 GetWorldPosition(int tileIndex)
		{
			var gridPosition = gameBoardLayout.TilePositions[tileIndex];
			var worldPosition = GetWorldPosition(gridPosition, gridCellSize);

			return worldPosition;
		}

		private Vector3 GetWorldPosition(Vector2Int gridPosition, float gridCellSize)
		{
			var worldPosition = new Vector3(gridPosition.x, 0.0f, gridPosition.y) * gridCellSize;

			return worldPosition;
		}

		private GameBoardLayout ConstructGameBoard(TextAsset gameBoardLayoutFile, float gridCellSize, GameObject tile)
		{
			var gameBoardLayout = GetGameBoardLayout(gameBoardLayoutFile);
			var tiles = ConstructTiles(gameBoardLayout, gridCellSize, tile);
			SpecializeTiles(gameBoardLayout, tiles);

			return gameBoardLayout;
		}

		private GameBoardLayout GetGameBoardLayout(TextAsset gameBoardLayoutFile)
		{
			var gameBoardLayoutFileContents = gameBoardLayoutFile.text;
			var gameBoardLayout = JsonConvert.DeserializeObject<GameBoardLayout>(gameBoardLayoutFileContents);

			return gameBoardLayout;
		}

		private List<GameObject> ConstructTiles(GameBoardLayout gameBoardLayout, float gridCellSize, GameObject tile)
		{
			var tiles = new List<GameObject>();

			foreach (var gridPosition in gameBoardLayout.TilePositions)
			{
				//Vector3 worldPosition = (Vector3Int)gridPosition;
				//var worldPosition = new Vector3(gridPosition.x, 0.0f, gridPosition.y) * gridCellSize;
				var worldPosition = GetWorldPosition(gridPosition, gridCellSize);
				var tileInstance = Instantiate(tile, worldPosition, Quaternion.identity);
				tiles.Add(tileInstance);
			}

			return tiles;
		}

		private void SpecializeTiles(GameBoardLayout gameBoardLayout, List<GameObject> tileInstances)
		{
			var colorTable = new Dictionary<QuizType, Color>
			{
				{ QuizType.Text, Color.red },
				{ QuizType.Flag, Color.green }
			};

			foreach (var quizPlacement in gameBoardLayout.QuizLayout)
			{
				var quizType = GetQuizType(quizPlacement.ID);
				var color = colorTable[quizType];

				SpecializeQuizTiles(tileInstances, quizPlacement, color);
			}
		}

		private QuizType GetQuizType(string quizID)
		{
			var quizDataFilePath = $"QuizData/{quizID}";
			var quizDataFile = Resources.Load<TextAsset>(quizDataFilePath);

			// TODO: Handle missing file.
			var quizDataFileContents = quizDataFile.text;
			var quiz = JsonConvert.DeserializeObject<Quiz>(quizDataFileContents);
			var quizType = (QuizType)int.Parse(quiz.QuestionType);

			return quizType;
		}

		void SpecializeQuizTiles(List<GameObject> tileInstances, QuizPlacement quizPlacement, Color color)
		{
			foreach (var tileIndex in quizPlacement.TileIndexes)
			{
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
