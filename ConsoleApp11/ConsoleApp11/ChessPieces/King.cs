using System;
using System.Collections.Generic;

namespace ChessEngine
{
    internal class King : IChessPiece, IComparable//per ora funziona, aggiungere checkmate
    {
        public King(PieceColor pieceColor, Grid position = Grid.NULL)
        {
            PieceColor = pieceColor;
            Position = position;
        }
        public ChessPiece Piece { get { return PieceColor == PieceColor.White ? ChessPiece.WhiteKing : ChessPiece.BlackKing; } }
        public Grid Position { get; set; }
        public int Val { get => 0; }
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
            return PieceColor == PieceColor.White ? "\u2654" : "\u265A";
        }

        public List<Grid> AllPossibleMoves(ChessBoard board, bool[] Enemybitmap)
        {
            List<Grid> possibleMoves = new List<Grid>();

            try
            {
                //se piece e la cella in alto a sinistra da piece (convertito in 64) sono in squadre differenti
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetTopLeftCellFrom(Position))]))
                    if (!Enemybitmap[CoordsConverter.Board120ToBoard64(CoordsConverter.GetTopLeftCellFrom(Position))])
                        possibleMoves.Add((Grid)CoordsConverter.GetTopLeftCellFrom(Position));
            }
            catch (ArgumentOutOfRangeException){ }

            try
            {
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetCellUpFrom(Position))]))
                    if (!Enemybitmap[CoordsConverter.Board120ToBoard64(CoordsConverter.GetCellUpFrom(Position))])
                        possibleMoves.Add((Grid)CoordsConverter.GetCellUpFrom(Position));
            }
            catch (ArgumentOutOfRangeException) { }

            try
            {
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetTopRightCellFrom(Position))]))
                    if (!Enemybitmap[CoordsConverter.Board120ToBoard64(CoordsConverter.GetTopRightCellFrom(Position))])
                        possibleMoves.Add((Grid)CoordsConverter.GetTopRightCellFrom(Position));
            }
            catch (ArgumentOutOfRangeException) { }

            try
            {
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64((int)Position + 1)]))
                    if (!Enemybitmap[CoordsConverter.Board120ToBoard64((int)Position + 1)])
                        possibleMoves.Add(Position + 1);
            }
            catch (ArgumentOutOfRangeException) { }

            try
            {
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetBottomRightCellFrom(Position))]))
                    if (!Enemybitmap[CoordsConverter.Board120ToBoard64(CoordsConverter.GetBottomRightCellFrom(Position))])
                        possibleMoves.Add((Grid)CoordsConverter.GetBottomRightCellFrom(Position));
            }
            catch (ArgumentOutOfRangeException) { }

            try
            {
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetCellDownFrom(Position))]))
                    if (!Enemybitmap[CoordsConverter.Board120ToBoard64(CoordsConverter.GetCellDownFrom(Position))])
                        possibleMoves.Add((Grid)CoordsConverter.GetCellDownFrom(Position));
            }
            catch (ArgumentOutOfRangeException) { }

            try
            {
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetBottomLeftCellFrom(Position))]))
                    if (!Enemybitmap[CoordsConverter.Board120ToBoard64(CoordsConverter.GetBottomLeftCellFrom(Position))])
                        possibleMoves.Add((Grid)CoordsConverter.GetBottomLeftCellFrom(Position));
            }
            catch (ArgumentOutOfRangeException) { }

            try
            {
                if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64((int)Position - 1)]))
                    if (!Enemybitmap[CoordsConverter.Board120ToBoard64((int)Position - 1)])
                        possibleMoves.Add(Position - 1);
            }
            catch (ArgumentOutOfRangeException) { }

            return possibleMoves;
        }

        public bool CanMoveTo(Grid des, ChessBoard board, bool[] enemyBitMap)
        {
            List<Grid> allPossibleMoves = AllPossibleMoves(board, enemyBitMap);

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
