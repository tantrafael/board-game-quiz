using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace BoardGameQuiz
{
	public class GameBoard : MonoBehaviour
	{
		public TextAsset boardLayoutFile;
		public float gridCellSize;
		public GameObject tile;

		void Start()
		{
			ConstructBoard(boardLayoutFile, gridCellSize, tile);
		}

		void ConstructBoard(TextAsset boardLayoutFile, float gridCellSize, GameObject tile)
		{
			var boardLayout = GetBoardLayout(boardLayoutFile);
			var tiles = ConstructTiles(boardLayout, gridCellSize, tile);
			SpecializeTiles(boardLayout, tiles);
		}

		GameBoardLayout GetBoardLayout(TextAsset boardLayoutFile)
		{
			var boardLayoutFileContents = boardLayoutFile.text;
			var boardLayout = JsonConvert.DeserializeObject<GameBoardLayout>(boardLayoutFileContents);

			return boardLayout;
		}

		List<GameObject> ConstructTiles(GameBoardLayout gameBoardLayout, float gridCellSize, GameObject tile)
		{
			var tiles = new List<GameObject>();

			foreach (var gridPosition in gameBoardLayout.TilePositions)
			{
				//Vector3 worldPosition = (Vector3Int)gridPosition;
				var worldPosition = new Vector3(gridPosition.x, 0.0f, gridPosition.y) * gridCellSize;
				var tileInstance = Instantiate(tile, worldPosition, Quaternion.identity);
				tiles.Add(tileInstance);
			}

			return tiles;
		}

		void SpecializeTiles(GameBoardLayout gameBoardLayout, List<GameObject> tileInstances)
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

		QuizType GetQuizType(string quizID)
		{
			var quizDataFilePath = $"QuizData/{quizID}";
			var quizDataFile = Resources.Load<TextAsset>(quizDataFilePath);

			// TODO: Handle missing file.
			var quizDataFileContents = quizDataFile.text;
			var quiz = JsonConvert.DeserializeObject<Quiz>(quizDataFileContents);
			var quizType = (QuizType)int.Parse(quiz.QuestionType);

			return quizType;
		}
	}
}
