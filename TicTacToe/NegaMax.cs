using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public class NegaMax
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

                int computedMove = -ComputeNegaMax(board, 10);
                
                board.UndoMove(move);

                if(computedMove > bestScore)
                {
                    BestMove = move;
                    bestScore = computedMove;
                }
            }
        }

        public int ComputeNegaMax(Board board, int depth)
        {
            CallAmount++;

            if (board.IsGameOver() || depth == 0)
                return board.Evaluate();

            int bestScore = int.MinValue;
            
            foreach (Move move in board.GetAvailableMoves())
            {
                board.MakeMove(move);

                bestScore = Math.Max(bestScore, -ComputeNegaMax(board, depth - 1));

                board.UndoMove(move);
            }

            return bestScore;
        }
    }
}
