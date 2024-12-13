using UnityEngine;
using UnityEngine.UIElements;

namespace Chess
{
    public class Board : MonoBehaviour
    {
        public const int BOARD_SIZE = 8;

        [SerializeField] private Transform bottomLeftSquareTransform;
        [SerializeField] private float squareSize;

        private Piece[,] grid;
        private Piece selectedPiece;
        private ChessGameController chessController;

        private void Awake()
        {
            CreateGrid();
        }

        public void SetDependencies(ChessGameController chessController)
        {
            this.chessController = chessController;
        }

        private void CreateGrid()
        {
            grid = new Piece[BOARD_SIZE, BOARD_SIZE];
        }

        internal Vector3 CalculatePositionFromCoords(Vector2Int coords)
        {
            return bottomLeftSquareTransform.position + new Vector3((squareSize+(float)0.4) * coords.x, 0, (squareSize+(float)0.4) * coords.y); //magic 0.4 - need to fix
        }

        public bool HasPiece(Piece piece)
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (grid[i, j] == piece)
                        return true;
                }
            }

            return false;
        }

        public void OnSquareSelected(Vector3 inputPosition)
        {
            Vector2Int coords = CalculateCoordsFromPosition(inputPosition);
            Piece piece = GetPieceOnSquare(coords);
            if (selectedPiece)
            {
                if (piece != null && selectedPiece == piece)
                    DeselectPiece();
                else if (piece != null && selectedPiece != piece && chessController.IsTeamTurnActive(piece.team))
                    SelectPiece(piece);
                else if (selectedPiece.CanMoveTo(coords))
                    OnSelectedPieceMoved(coords, selectedPiece);
            }
            else
            {
                if (piece != null&&chessController.IsTeamTurnActive(piece.team))
                    SelectPiece(piece);
            }
        }

        private void OnSelectedPieceMoved(Vector2Int coords, Piece piece)
        {
            UpdateBoardOnPieceMove(coords, piece.occupiedSquare, piece, null);
            selectedPiece.MovePiece(coords);
            DeselectPiece();
            EndTurn();
        }

        private void EndTurn()
        {
            chessController.EndTurn();
        }

        private void UpdateBoardOnPieceMove(Vector2Int newCoords, Vector2Int oldCoords, Piece newPiece, Piece oldPiece)
        {
            grid[oldCoords.x, oldCoords.y] = oldPiece;
            grid[newCoords.x, newCoords.y] = newPiece;
        }

        private void SelectPiece(Piece piece)
        {
            selectedPiece = piece;
        }


        private void DeselectPiece()
        {
            selectedPiece = null;
        }

        public Piece GetPieceOnSquare(Vector2Int coords)
        {
            if (CheckIfCoordinatesAreOnBoard(coords))
            {
                Debug.Log(coords.x + ", " + coords.y);
                Debug.Log(grid[coords.x, coords.y]);
                return grid[coords.x, coords.y];
            }

            return null;
        }

        public bool CheckIfCoordinatesAreOnBoard(Vector2Int coords)
        {
            if (coords.x < 0 || coords.y < 0 || coords.x >= BOARD_SIZE || coords.y >=BOARD_SIZE)
                return false;
            return true;
        }

        private Vector2Int CalculateCoordsFromPosition(Vector3 inputPosition)
        {
            int x = Mathf.RoundToInt(inputPosition.x/squareSize) + BOARD_SIZE / 2;
            int y = Mathf.RoundToInt(inputPosition.z /squareSize) + BOARD_SIZE / 2;
            Debug.Log("position "+ inputPosition+" coords "+  new Vector2Int(x, y));
            return new Vector2Int(x, y);
        }

        public void SetPieceOnBoard(Vector2Int squareCoords, Piece piece)
        {
            if (CheckIfCoordinatesAreOnBoard(squareCoords))
                grid[squareCoords.x, squareCoords.y] = piece;    
        }
    }
}