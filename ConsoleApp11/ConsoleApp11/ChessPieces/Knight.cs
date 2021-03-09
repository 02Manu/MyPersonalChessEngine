using System;
using System.Collections.Generic;

namespace ChessEngine
{
    internal class Knight : IChessPiece, IComparable
    {
        public Knight(PieceColor pieceColor, Grid position = Grid.NULL)
        {
            PieceColor = pieceColor;
            Position = position;
        }

        public ChessPiece Piece { get { return PieceColor == PieceColor.White ? ChessPiece.WhiteKnight : ChessPiece.BlackKnight; } }
        public Grid Position { get; set; }
        public int Val { get => 3; }
        public PieceColor PieceColor { get; private set; }

        public int CompareTo(object obj)
        {
            if (Val > (obj as IChessPiece).Val)
                return 1;
            if (Val < (obj as IChessPiece).Val)
                return -1;
            return 0;
        }
        public override string ToString()
        {
            return PieceColor == PieceColor.White ? "\u2658" : "\u265E";
        }

        public List<Grid> AllPossibleMoves(ChessBoard board, bool[] enemyBitMap = null)
        {
            List<Grid> possibleMoves = new List<Grid>();

            //destra
            try
            {
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64((int)Position + 12)]))
                    possibleMoves.Add(Position + 12);
            }
            catch (ArgumentOutOfRangeException) { }
            try
            {
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64((int)Position - 8)]))
                    possibleMoves.Add(Position - 8);
            }
            catch (ArgumentOutOfRangeException) { }
            //sinistra
            try
            {
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64((int)Position - 12)]))
                    possibleMoves.Add(Position - 12);
            }
            catch (ArgumentOutOfRangeException) { }
            try
            {
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64((int)Position + 8)]))
                    possibleMoves.Add(Position + 8);
            }
            catch (ArgumentOutOfRangeException) { }
            //alto
            try
            {
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64((int)Position - 19)]))
                    possibleMoves.Add(Position - 19);
            }
            catch (ArgumentOutOfRangeException) { }
            try
            {
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64((int)Position - 21)]))
                    possibleMoves.Add(Position - 21);
            }
            catch (ArgumentOutOfRangeException) { }
            //basso
            try
            {
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64((int)Position + 19)]))
                    possibleMoves.Add(Position + 19);
            }
            catch (ArgumentOutOfRangeException) { }
            try
            {
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64((int)Position + 21)]))
                    possibleMoves.Add(Position + 21);
            }
            catch (ArgumentOutOfRangeException) { }

            return possibleMoves;
        }

        public bool CanMoveTo(Grid des, ChessBoard board, bool[] enemyBitMap = null)
        {
            List<Grid> allPossibleMoves = AllPossibleMoves(board);

            foreach (Grid cell in allPossibleMoves)
            {
                if (des == cell)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
