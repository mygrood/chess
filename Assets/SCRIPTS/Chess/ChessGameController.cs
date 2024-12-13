using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    [RequireComponent(typeof(PieceCreator))]
    public class ChessGameController : MonoBehaviour
    {
        [SerializeField] private BoardLayout startingBoardLayout;
        [SerializeField] private Board board;
        
        private PieceCreator pieceCreator;
        
        private void Awake()
        {
            SetDependencies();
        }

        private void SetDependencies()
        {
            pieceCreator = GetComponent<PieceCreator>();
        }

        private void Start()
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            CreatePiecesFromLayout(startingBoardLayout);
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
                Debug.Log("CreatePiecesFromLayout type"+ typeName + ", " + pieceType.ToString());
                CreatePieceAndInitialize(squareCoords, team, pieceType);
            }
        }

        private void CreatePieceAndInitialize(Vector2Int squareCoords, TeamColor team, Type pieceType)
        {
            Piece newPiece = pieceCreator.CreatePiece(pieceType).GetComponent<Piece>();
            newPiece.SetData(squareCoords, team,board);
            Material teamMaterial = pieceCreator.GetTeamMaterial(team); 
            newPiece.SetMaterial(teamMaterial);
            
        }
    }
}