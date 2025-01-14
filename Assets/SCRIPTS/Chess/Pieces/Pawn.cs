using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class Pawn : Piece
    {
        public override List<Vector2Int> SelectAvailableSquares()
        {
            availableMoves.Clear();
            Vector2Int direction = team == TeamColor.White ? Vector2Int.up : Vector2Int.down;
            float range = hasMoved ? 1 : 2;

            // Прямое движение
            for (int i = 1; i <= range; i++) // Начинаем с 1, чтобы не проверять текущую клетку
            {
                Vector2Int nextCoords = occupiedSquare + direction * i;
                if (!board.CheckIfCoordinatesAreOnBoard(nextCoords))
                {
                    break;
                }

                Piece piece = board.GetPieceOnSquare(nextCoords);
                if (piece == null)
                {
                    TryToAddMove(nextCoords);
                }
                else
                {
                    if (piece.IsFromSameTeam(this))
                    {
                        break;
                    }
                }
            }

            // Атака по диагоналям
            Vector2Int[] takeDirections = new Vector2Int[]
            {
                new Vector2Int(-1, direction.y),
                new Vector2Int(1, direction.y)
            };
            for (int i = 0; i < takeDirections.Length; i++)
            {
                Vector2Int nextCoords = occupiedSquare + takeDirections[i];
                if (!board.CheckIfCoordinatesAreOnBoard(nextCoords))
                {
                    continue;
                }

                Piece piece = board.GetPieceOnSquare(nextCoords);
                if (piece != null && !piece.IsFromSameTeam(this))
                {
                    TryToAddMove(nextCoords);
                }
            }

            return availableMoves;
        }

        public override void MovePiece(Vector2Int coords)
        {
            base.MovePiece(coords);
            CheckPromotion();
        }

        private void CheckPromotion()
        {
            int endOfBoardYCoord = team == TeamColor.White ? Board.BOARD_SIZE - 1 : 0;
            if (occupiedSquare.y == endOfBoardYCoord)
                board.PromotePiece(this);
        }
    }
}