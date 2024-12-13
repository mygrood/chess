using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class Pawn : Piece
    {
       /* public override List<Vector2Int> SelectAvailableSquares()
        {
            availableMoves.Clear();
            Vector2Int direction = team == TeamColor.White ? Vector2Int.up : Vector2Int.down;
            float range = hasMoved ? 1 : 2;
            for (int i = 0; i <= range; i++)
            {
                Vector2Int nextCoords = occupiedSquare + direction * i;
                Piece piece=board.GetPieceOnSquare(nextCoords);
                if (!board.CheckIfCoordinatesAreOnBoard(nextCoords))
                    break;
                if (piece == null)
                    TryToAddMove(nextCoords);
                else if (piece.IsFromSameTeam(this))
                    break;
            }
            Vector2Int[] takeDirections = new Vector2Int[] { new Vector2Int(1,direction.y),new Vector2Int(-1,direction.y) };
            for (int i = 0; i <takeDirections.Length; i++)
            {
                Vector2Int nextCoords = occupiedSquare + takeDirections[i];
                Piece piece=board.GetPieceOnSquare(nextCoords);
                if (!board.CheckIfCoordinatesAreOnBoard(nextCoords))
                    break;
                if (piece != null && !piece.IsFromSameTeam(this))
                    TryToAddMove(nextCoords);
            }
            return availableMoves;
        }*/
       
       public override List<Vector2Int> SelectAvailableSquares()
{
    availableMoves.Clear();
    Vector2Int direction = team == TeamColor.White ? Vector2Int.up : Vector2Int.down;
    float range = hasMoved ? 1 : 2;

    Debug.Log($"Проверяем доступные клетки для фигуры: {this}, команда: {team}, занятая клетка: {occupiedSquare}, диапазон: {range}");

    // Прямое движение
    for (int i = 1; i <= range; i++) // Начинаем с 1, чтобы не проверять текущую клетку
    {
        Vector2Int nextCoords = occupiedSquare + direction * i;
        if (!board.CheckIfCoordinatesAreOnBoard(nextCoords))
        {
            Debug.Log($"Координаты {nextCoords} вне доски, прерываем цикл прямого движения.");
            break;
        }

        Piece piece = board.GetPieceOnSquare(nextCoords);
        if (piece == null)
        {
            Debug.Log($"Клетка {nextCoords} свободна, добавляем в доступные ходы.");
            TryToAddMove(nextCoords);
        }
        else
        {
            Debug.Log($"Клетка {nextCoords} занята фигурой: {piece}. Проверяем команду...");
            if (piece.IsFromSameTeam(this))
            {
                Debug.Log($"Фигура на {nextCoords} из той же команды. Прерываем цикл прямого движения.");
                break;
            }
        }
    }

    // Атака по диагоналям
    Vector2Int[] takeDirections = new Vector2Int[] 
    { 
        new Vector2Int(1, direction.y), 
        new Vector2Int(-1, direction.y) 
    };

    for (int i = 0; i < takeDirections.Length; i++)
    {
        Vector2Int nextCoords = occupiedSquare + takeDirections[i];
        if (!board.CheckIfCoordinatesAreOnBoard(nextCoords))
        {
            Debug.Log($"Координаты {nextCoords} вне доски, пропускаем проверку атаки.");
            continue;
        }

        Piece piece = board.GetPieceOnSquare(nextCoords);
        if (piece != null && !piece.IsFromSameTeam(this))
        {
            Debug.Log($"Фигура на {nextCoords} из вражеской команды: {piece}. Добавляем в доступные ходы.");
            TryToAddMove(nextCoords);
        }
        else if (piece == null)
        {
            Debug.Log($"Клетка {nextCoords} пуста, сюда нельзя атаковать.");
        }
        else
        {
            Debug.Log($"Фигура на {nextCoords} из той же команды. Пропускаем.");
        }
    }

    Debug.Log($"Доступные ходы: {string.Join(", ", availableMoves)}");
    return availableMoves;
}

    }
}