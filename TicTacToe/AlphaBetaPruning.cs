using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class AlphaBetaPruning
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

                int computedMove = ComputeAlphaBetaPruning(board, 10, false, int.MinValue, int.MaxValue);

                board.UndoMove(move);

                if (computedMove > bestScore)
                {
                    bestScore = computedMove;
                    BestMove = move;
                }
            }
        }

        public int ComputeAlphaBetaPruning(Board board, int depth, bool isMaximizingPlayer, int alpha, int beta)
        {
            CallAmount++;

            if (board.IsGameOver() || depth == 0)
                return board.Evaluate(Player.Circle);

            int bestScore = isMaximizingPlayer ? int.MinValue : int.MaxValue;

            foreach (Move move in board.GetAvailableMoves())
            {
                board.MakeMove(move);

                int value = ComputeAlphaBetaPruning(board, depth - 1, !isMaximizingPlayer, alpha, beta);

                bestScore = isMaximizingPlayer ? Math.Max(bestScore, value) : Math.Min(bestScore, value);

                if(isMaximizingPlayer)
                    alpha = Math.Max(alpha, value);
                else
                    beta = Math.Min(beta, value);

                board.UndoMove(move);

                if (beta <= alpha)
                    break;
            }

            return bestScore;
        }
    }
}
