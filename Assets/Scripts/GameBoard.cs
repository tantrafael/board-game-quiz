using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Assertions;

namespace BoardGameQuiz
{
	public class GameBoard : MonoBehaviour
	{
		public float gridCellSize;
		public GameObject tileAsset;

		private GameBoardLayout gameBoardLayout;

		public void Initialize(GameBoardLayout gameBoardLayout)
		{
			this.gameBoardLayout = gameBoardLayout;

			ConstructGameBoard(gameBoardLayout, gridCellSize, tileAsset);
		}

		public Vector3 GetWorldPosition(int tileIndex)
		{
			Assert.IsNotNull(gameBoardLayout);
			Assert.IsNotNull(gameBoardLayout.TilePositions);
			Assert.IsTrue(tileIndex < gameBoardLayout.TilePositions.Count);

			var gridPosition = gameBoardLayout.TilePositions[tileIndex];
			var worldPosition = GetWorldPosition(gridPosition, gridCellSize);

			return worldPosition;
		}

		public int GetTotalTileCount()
		{
			var totalTileCount = gameBoardLayout.TilePositions.Count;

			return totalTileCount;
		}

		private Vector3 GetWorldPosition(Vector2Int gridPosition, float gridCellSize)
		{
			var worldPosition = new Vector3(gridPosition.x, 0.0f, gridPosition.y) * gridCellSize;

			return worldPosition;
		}

		private void ConstructGameBoard(GameBoardLayout gameBoardLayout, float gridCellSize, GameObject tile)
		{
			var tiles = ConstructTiles(gameBoardLayout, gridCellSize, tile);
			SpecializeTiles(gameBoardLayout, tiles);
		}

		private List<GameObject> ConstructTiles(GameBoardLayout gameBoardLayout, float gridCellSize, GameObject tile)
		{
			var tiles = new List<GameObject>();

			foreach (var gridPosition in gameBoardLayout.TilePositions)
			{
				var worldPosition = GetWorldPosition(gridPosition, gridCellSize);
				var tileInstance = Instantiate(tile, worldPosition, Quaternion.identity);

				tiles.Add(tileInstance);
			}

			return tiles;
		}

		private void SpecializeTiles(GameBoardLayout gameBoardLayout, List<GameObject> tileInstances)
		{
			// TODO: Get tile materials from settings.
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
			var totalTileCount = tileInstances.Count;

			foreach (var tileIndex in quizPlacement.TileIndexes)
			{
				Assert.IsTrue(tileIndex < totalTileCount);

				var tileInstance = tileInstances[tileIndex];

				// TODO: Clean up getting child and magic number.
				var tileModel = tileInstance.transform.GetChild(0).gameObject;
				var tileRenderer = tileModel.GetComponent<Renderer>();

				tileRenderer.material.color = color;
			}
		}
	}
}
