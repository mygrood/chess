using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class King: Piece
    {
        private Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, 1),
            new Vector2Int(-1, -0),
            Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down
        };

        public override List<Vector2Int> SelectAvailableSquares()
        {
            availableMoves.Clear();
            float range = 1;
            foreach (var direction in directions)
            {
                for (int i = 0; i <= range; i++)
                {
                    Vector2Int nextCoords = occupiedSquare + direction * i;
                    Piece piece = board.GetPieceOnSquare(nextCoords);
                    if (!board.CheckIfCoordinatesAreOnBoard(nextCoords))
                        break;
                    if (piece == null)
                        TryToAddMove(nextCoords);
                    else if (!piece.IsFromSameTeam(this))
                    {
                        TryToAddMove(nextCoords);
                        break;
                    }
                    else if (piece.IsFromSameTeam(this))
                        break;
                }
            }

            return availableMoves;
        }
    }
}