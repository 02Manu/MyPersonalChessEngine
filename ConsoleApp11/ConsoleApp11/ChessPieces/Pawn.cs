using System;
using System.Collections.Generic;

namespace ChessEngine
{
    internal class Pawn : IChessPiece, IComparable// TODO da testare
    {
        public Pawn(PieceColor pieceColor, Grid position = Grid.NULL)
        {
            PieceColor = pieceColor;
            Position = position;
        }
        public ChessPiece Piece { get { return PieceColor == PieceColor.White ? ChessPiece.WhitePawn : ChessPiece.BlackPawn; } }
        public Grid Position { get; set; }
        public int Val { get => 1; }
        public PieceColor PieceColor { get; private set; }

        public bool FirstMove { get => (CoordsConverter.GetRecord(Position) == Record.Row2 && PieceColor == PieceColor.Black) || (CoordsConverter.GetRecord(Position) == Record.Row7 && PieceColor == PieceColor.White); }

        public bool EnPassantable { get => throw new NotImplementedException(); }

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
            return PieceColor == PieceColor.White ? "\u2659" : "\u265F";
        }

        public List<Grid> AllPossibleMoves(ChessBoard board, bool[] enemyBitMap = null)//da testare
        {
            List<Grid> possibleMoves = new List<Grid>();

            if (PieceColor == PieceColor.White)
            {
                try 
                { 
                    if (board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetCellUpFrom(Position))] == ChessPiece.EMPTY)
                    {
                        possibleMoves.Add((Grid)CoordsConverter.GetCellUpFrom(Position));
                        if (FirstMove && board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetCellUpFrom(Position, 2))] == ChessPiece.EMPTY)
                            possibleMoves.Add((Grid)CoordsConverter.GetCellUpFrom(Position, 2));
                    }
                }
                catch (ArgumentOutOfRangeException) { }
            //quando prende
            try
                {
                    if (ChessBoard.AreInDifferentTeams(board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetTopLeftCellFrom(Position))], Piece))
                    {
                        if (board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetTopLeftCellFrom(Position))] != ChessPiece.EMPTY)
                        {
                            possibleMoves.Add((Grid)CoordsConverter.GetTopLeftCellFrom(Position));
                        }
                    }
                    if (ChessBoard.AreInDifferentTeams(board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetTopRightCellFrom(Position))], Piece))
                    {
                        if (board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetTopRightCellFrom(Position))] != ChessPiece.EMPTY)
                        {
                            possibleMoves.Add((Grid)CoordsConverter.GetTopRightCellFrom(Position));
                        }
                    }
                }
                catch (ArgumentOutOfRangeException) { }

            }
            else if (PieceColor == PieceColor.Black)
            {
                try
                {
                    if (board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetCellDownFrom(Position))] == ChessPiece.EMPTY)
                    {
                        possibleMoves.Add((Grid)CoordsConverter.GetCellDownFrom(Position));
                        if (FirstMove && board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetCellDownFrom(Position, 2))] == ChessPiece.EMPTY)
                            possibleMoves.Add((Grid)CoordsConverter.GetCellDownFrom(Position, 2));
                    }
                }
                catch (ArgumentOutOfRangeException) { }

                //quando prende
                try
                {
                    if (ChessBoard.AreInDifferentTeams(board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetBottomLeftCellFrom(Position))], Piece))
                    {
                        if (board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetBottomLeftCellFrom(Position))] != ChessPiece.EMPTY)
                        {
                            possibleMoves.Add((Grid)CoordsConverter.GetBottomLeftCellFrom(Position));
                        }
                    }
                    if (ChessBoard.AreInDifferentTeams(board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetBottomRightCellFrom(Position))], Piece))
                    {
                        if (board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetBottomRightCellFrom(Position))] != ChessPiece.EMPTY)
                        {
                            possibleMoves.Add((Grid)CoordsConverter.GetBottomRightCellFrom(Position));
                        }
                    }
                }
                catch (ArgumentOutOfRangeException) { }

            }
            //TODO: en passant

            return possibleMoves;
        }

        public List<Grid> AllPossibleWhiteMoves(ChessBoard board)
        {
            List<Grid> possibleMoves = new List<Grid>();

            if (board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetCellUpFrom(Position, 2))] == ChessPiece.EMPTY)
            {
                possibleMoves.Add((Grid)CoordsConverter.GetCellUpFrom(Position));//^

                if (FirstMove)//prima mossa
                {
                    possibleMoves.Add((Grid)CoordsConverter.GetCellUpFrom(Position, 2));
                }
            }
            if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetTopLeftCellFrom(Position))]))//se questo pawn è in un team diverso di una pedina che sta in alto a sinistra rispetto a lei
            {
                possibleMoves.Add((Grid)CoordsConverter.GetTopLeftCellFrom(Position));
            }
            if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetTopRightCellFrom(Position))]))//se questo pawn è in un team diverso di una pedina che sta in alto a destra rispetto a lei
            {
                possibleMoves.Add((Grid)CoordsConverter.GetTopRightCellFrom(Position));
            }
            //TODO: en passant

            return possibleMoves;
        }
        public List<Grid> AllPossibleBlackMoves(ChessBoard board)
        {
            List<Grid> possibleMoves = new List<Grid>();

            if (board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetCellDownFrom(Position, 2))] == ChessPiece.EMPTY)
            {
                possibleMoves.Add((Grid)CoordsConverter.GetCellDownFrom(Position));//^

                if (FirstMove)//prima mossa
                {
                    possibleMoves.Add((Grid)CoordsConverter.GetCellDownFrom(Position, 2));
                }
            }
            if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetBottomLeftCellFrom(Position))]))//se questo pawn è in un team diverso di una pedina che sta in basso a sinistra rispetto a lei
            {
                possibleMoves.Add((Grid)CoordsConverter.GetBottomLeftCellFrom(Position));
            }
            if (ChessBoard.AreInDifferentTeams(Piece, board[CoordsConverter.Board120ToBoard64(CoordsConverter.GetBottomRightCellFrom(Position))]))//se questo pawn è in un team diverso di una pedina che sta in basso a destra rispetto a lei
            {
                possibleMoves.Add((Grid)CoordsConverter.GetBottomRightCellFrom(Position));
            }
            //TODO: en passant

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
