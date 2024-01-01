using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class MinMax
    {
        public uint CallAmount;
        public Move BestMove = new Move();

        public void GetNextMove(Board board)
        {
            CallAmount = 0U;
            int bestScore = int.MinValue;
            foreach (Move move in board.GetAvailableMoves())
            {
                board.MakeMove(move);

                int computedMove = ComputeMinMax(board, 10, false);
                
                board.UndoMove(move);

                if(computedMove > bestScore)
                {
                    bestScore = computedMove;
                    BestMove = move;
                }
            }
        }

        public int ComputeMinMax(Board board, int depth, bool isMaximizingPlayer)
        {
            CallAmount++;

            if (board.IsGameOver() || depth == 0)
                return board.Evaluate(Player.Circle);

            int bestScore = isMaximizingPlayer ? int.MinValue : int.MaxValue;
            
            foreach (Move move in board.GetAvailableMoves())
            {
                board.MakeMove(move);

                int value = ComputeMinMax(board, depth - 1, !isMaximizingPlayer);

                bestScore = isMaximizingPlayer ? Math.Max(bestScore, value) : Math.Min(bestScore, value);

                board.UndoMove(move);
            }

            return bestScore;
        }
    }
}
