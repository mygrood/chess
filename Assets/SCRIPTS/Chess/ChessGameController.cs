using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Chess
{
    [RequireComponent(typeof(PieceCreator))]
    public class ChessGameController : MonoBehaviour
    {
        
        
        [SerializeField] private BoardLayout startingBoardLayout;
        [SerializeField] private Board board;

        private PieceCreator pieceCreator;
        private ChessPlayer activePlayer;
        private ChessPlayer whitePlayer;
        private ChessPlayer blackPlayer;

        private void Awake()
        {
            SetDependencies();
            CreatePlayers();
        }

        private void SetDependencies()
        {
            pieceCreator = GetComponent<PieceCreator>();
        }

        private void CreatePlayers()
        {
            whitePlayer = new ChessPlayer(TeamColor.White, board);
            blackPlayer = new ChessPlayer(TeamColor.Black, board);
        }

        private void Start()
        {
            StartNewGame();
        }

        [ContextMenu("New Game")]
        private void StartNewGame()
        {
            board.SetDependencies(this);
            CreatePiecesFromLayout(startingBoardLayout);
            activePlayer = whitePlayer;
            GenerateAllPossiblePlayerMoves(activePlayer);
        }

        private void CreatePiecesFromLayout(BoardLayout layout)
        {
            for (int i = 0; i < layout.GetPiecesCount(); i++)
            {
                Vector2Int squareCoords = layout.GetSquareCoordsAtIndex(i);
                TeamColor team = layout.GetSquareTeamColorAtIndex(i);
                string typeName = layout.GetSquarePieceNameAtIndex(i);

                string fullTypeName = $"Chess.{typeName}";
                Type pieceType = Type.GetType(fullTypeName);

                if (pieceType == null)
                {
                    Debug.LogError($"Piece type not found: {fullTypeName}");
                    return;
                }

                Debug.Log("CreatePiecesFromLayout type" + typeName + ", " + pieceType.ToString());
                CreatePieceAndInitialize(squareCoords, team, pieceType);
            }
        }

        private void CreatePieceAndInitialize(Vector2Int squareCoords, TeamColor team, Type pieceType)
        {
            Piece newPiece = pieceCreator.CreatePiece(pieceType).GetComponent<Piece>();
            newPiece.SetData(squareCoords, team, board);
            Material teamMaterial = pieceCreator.GetTeamMaterial(team);
            newPiece.SetMaterial(teamMaterial);
            
            board.SetPieceOnBoard(squareCoords, newPiece);
            ChessPlayer currentPlayer = team == TeamColor.White ? whitePlayer : blackPlayer;
            currentPlayer.AddPiece(newPiece);

        }

        private void GenerateAllPossiblePlayerMoves(ChessPlayer player)
        {
            player.GenerateAllPossibleMoves();
        }

        public bool IsTeamTurnActive(TeamColor team)
        {
            return activePlayer.team == team;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void EndTurn()
        {
            GenerateAllPossiblePlayerMoves(activePlayer);
            GenerateAllPossiblePlayerMoves(GetOpponentToPlayer(activePlayer));
            ChangeActiveTeam();
        }

        private void ChangeActiveTeam()
        {
            activePlayer = activePlayer == whitePlayer ? blackPlayer : whitePlayer;
        }

        private ChessPlayer GetOpponentToPlayer(ChessPlayer player)
        {
            return player == whitePlayer ? blackPlayer : whitePlayer;
        }
    }
}