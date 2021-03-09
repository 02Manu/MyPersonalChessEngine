using System;
using System.Collections.Generic;

namespace ChessEngine
{
    internal class Queen : IChessPiece, IComparable
    {
        public Queen(PieceColor pieceColor, Grid position = Grid.NULL)
        {
            PieceColor = pieceColor;
            Position = position;
        }
        public ChessPiece Piece { get { return PieceColor == PieceColor.White ? ChessPiece.WhiteQueen : ChessPiece.BlackQueen; } }
        public Grid Position { get; set; }
        public int Val { get => 9; }
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
            return PieceColor == PieceColor.White ? "\u2655" : "\u265B";
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

        public List<Grid> AllPossibleMoves(ChessBoard board, bool[] enemyBitMap = null)
        {
            List<Grid> possibleMoves = new List<Grid>();

            possibleMoves.AddRange(PossibleRightMoves(board));//>

            possibleMoves.AddRange(PossibleLeftMoves(board));//<

            possibleMoves.AddRange(PossibleUpperMoves(board));//^

            possibleMoves.AddRange(PossibleDownMoves(board));//v

            possibleMoves.AddRange(PossibleTopRightMoves(board));//>

            possibleMoves.AddRange(PossibleTopLeftMoves(board));//<

            possibleMoves.AddRange(PossibleBottomRightMoves(board));//^

            possibleMoves.AddRange(PossibleBottomLeftMoves(board));//v

            return possibleMoves;
        }

        public List<Grid> PossibleBottomLeftMoves(ChessBoard board)
        {
            Record recordOfBishop = CoordsConverter.GetRecord(Position);//la riga in cui è posizionato il rook
            Grid BottomRightBoundary = (Grid)99;//(Grid)(((int)recordOfBishop * 10) + 11 + 7);//trova la parte più a destra della riga su cui è posizionato il rook

            List<Grid> possibleMoves = new List<Grid>();
            try
            {
                //a destra r-->
                for (int i = (int)Position + 9; i < (int)BottomRightBoundary + 9; i += 9)//va dalla posizione del rook fino alla fine della riga e controlla se ci sono dei pezzi
                {
                    if (board[CoordsConverter.Board120ToBoard64(i)] == ChessPiece.EMPTY)//se non ci sta un cazzo
                    {
                        possibleMoves.Add((Grid)i);// è una mossa possibile
                    }
                    else
                    {
                        //controlla se l'ultima casella è dello stesso team, se non lo è aggiungi un'altra possibile mossa
                        if (ChessBoard.AreInDifferentTeams(board[CoordsConverter.Board120ToBoard64(i)], Piece))
                            possibleMoves.Add((Grid)i);

                        break;
                    }
                }
            }
            catch (ArgumentOutOfRangeException) { }

            return possibleMoves;
        }
        public List<Grid> PossibleTopLeftMoves(ChessBoard board)
        {
            Record recordOfBishop = CoordsConverter.GetRecord(Position);//la riga in cui è posizionato il rook

            List<Grid> possibleMoves = new List<Grid>();

            Grid TopLeftBoundary = (Grid)20;//(Grid)(((int)recordOfBishop * 10) + 11);//trova la parte più a sinistra del rook
            //a sinistra <--r
            try
            {
                for (int i = (int)Position - 11; i >= /*(int)Position - */(int)TopLeftBoundary; i -= 11)
                {
                    if (board[CoordsConverter.Board120ToBoard64(i)] == ChessPiece.EMPTY)//se non ci sta un cazzo
                    {
                        possibleMoves.Add((Grid)i);
                    }
                    else
                    {
                        //controlla se l'ultima casella è dello stesso team, se non lo è aggiungi un'altra possibile mossa
                        if (ChessBoard.AreInDifferentTeams(board[CoordsConverter.Board120ToBoard64(i)], Piece))
                            possibleMoves.Add((Grid)i);

                        break;
                    }
                }
            }
            catch (ArgumentOutOfRangeException) { }


            return possibleMoves;
        }
        public List<Grid> PossibleBottomRightMoves(ChessBoard board)
        {
            List<Grid> possibleMoves = new List<Grid>();

            Column colOfBishop = CoordsConverter.GetColumn(Position);//la colonna in cui  posizionato il rook

            Grid bottomRightBoundary = (Grid)109;//(Grid)(colOfBishop + 90);

            try
            {
                //in basso
                for (int i = CoordsConverter.GetBottomRightCellFrom(Position); i < (int)bottomRightBoundary + 11; i += 11)
                {
                    if (board[CoordsConverter.Board120ToBoard64(i)] == ChessPiece.EMPTY)//se non ci sta un cazzo
                    {
                        possibleMoves.Add((Grid)i);
                    }
                    else
                    {
                        //controlla se l'ultima casella è dello stesso team, se non lo è aggiungi un'altra possibile mossa
                        if (ChessBoard.AreInDifferentTeams(board[CoordsConverter.Board120ToBoard64(i)], Piece))
                            possibleMoves.Add((Grid)i);

                        break;
                    }//controlla se l'ultima casella è dello stesso team
                }
            }
            catch (ArgumentOutOfRangeException) { }
            return possibleMoves;
        }
        public List<Grid> PossibleTopRightMoves(ChessBoard board)
        {
            List<Grid> possibleMoves = new List<Grid>();

            //Column colOfBishop = CoordsConverter.GetColumn(Position);

            //Grid topRightBoundary = (Grid)29;//(Grid)(colOfBishop + 20);



            int len = (int)CoordsConverter.GetRecord(Position) - 1;
            try
            {
                for (int i = CoordsConverter.GetTopRightCellFrom(Position); i >= len; i -= 9)
                {
                    if (board[CoordsConverter.Board120ToBoard64(i)] == ChessPiece.EMPTY)//se non ci sta un cazzo
                    {
                        possibleMoves.Add((Grid)i);
                    }
                    else
                    {
                        //controlla se l'ultima casella è dello stesso team, se non lo è aggiungi un'altra possibile mossa
                        if (ChessBoard.AreInDifferentTeams(board[CoordsConverter.Board120ToBoard64(i)], Piece))
                            possibleMoves.Add((Grid)i);

                        break;
                    }//controlla se l'ultima casella è dello stesso team//controlla se l'ultima casella è dello stesso team
                }
            }
            catch (ArgumentOutOfRangeException) { }


            return possibleMoves;
        }
        public List<Grid> PossibleRightMoves(ChessBoard board)
        {
            Record recordOfRook = CoordsConverter.GetRecord(Position);//la riga in cui è posizionato il rook
            Grid rightBoundary = (Grid)(((int)recordOfRook * 10) + 11 + 7);//trova la parte più a destra della riga su cui è posizionato il rook

            List<Grid> possibleMoves = new List<Grid>();
            try
            {
                //a destra r-->
                for (int i = (int)Position + 1; i < (int)rightBoundary + 1; i++)//va dalla posizione del rook fino alla fine della riga e controlla se ci sono dei pezzi
                {
                    if (board[CoordsConverter.Board120ToBoard64(i)] == ChessPiece.EMPTY)//se non ci sta un cazzo
                    {
                        possibleMoves.Add((Grid)i);// è una mossa possibile
                    }
                    else
                    {
                        //controlla se l'ultima casella è dello stesso team, se non lo è aggiungi un'altra possibile mossa
                        if (ChessBoard.AreInDifferentTeams(board[CoordsConverter.Board120ToBoard64(i)], Piece))
                            possibleMoves.Add((Grid)i);

                        break;
                    }
                }
            }
            catch (ArgumentOutOfRangeException) { }

            return possibleMoves;
        }
        public List<Grid> PossibleLeftMoves(ChessBoard board)
        {
            Record recordOfRook = CoordsConverter.GetRecord(Position);//la riga in cui è posizionato il rook

            List<Grid> possibleMoves = new List<Grid>();

            Grid leftBoundary = (Grid)(((int)recordOfRook * 10) + 11);//trova la parte più a sinistra del rook
            //a sinistra <--r
            try
            {
                for (int i = (int)Position - 1; i >= (int)Position - (int)leftBoundary; i--)
                {
                    if (board[CoordsConverter.Board120ToBoard64(i)] == ChessPiece.EMPTY)//se non ci sta un cazzo
                    {
                        possibleMoves.Add((Grid)i);
                    }
                    else
                    {
                        //controlla se l'ultima casella è dello stesso team, se non lo è aggiungi un'altra possibile mossa
                        if (ChessBoard.AreInDifferentTeams(board[CoordsConverter.Board120ToBoard64(i)], Piece))
                            possibleMoves.Add((Grid)i);

                        break;
                    }
                }
            }
            catch (ArgumentOutOfRangeException) { }


            return possibleMoves;
        }
        public List<Grid> PossibleDownMoves(ChessBoard board)
        {
            List<Grid> possibleMoves = new List<Grid>();

            Column colOfRook = CoordsConverter.GetColumn(Position);//la colonna in cui  posizionato il rook

            Grid downBoundary = (Grid)(colOfRook + 90);

            try
            {
                //in basso
                for (int i = CoordsConverter.GetCellDownFrom(Position); i < (int)downBoundary + 10; i += 10)
                {
                    if (board[CoordsConverter.Board120ToBoard64(i)] == ChessPiece.EMPTY)//se non ci sta un cazzo
                    {
                        possibleMoves.Add((Grid)i);
                    }
                    else
                    {
                        //controlla se l'ultima casella è dello stesso team, se non lo è aggiungi un'altra possibile mossa
                        if (ChessBoard.AreInDifferentTeams(board[CoordsConverter.Board120ToBoard64(i)], Piece))
                            possibleMoves.Add((Grid)i);

                        break;
                    }//controlla se l'ultima casella è dello stesso team
                }
            }
            catch (ArgumentOutOfRangeException) { }
            return possibleMoves;
        }
        public List<Grid> PossibleUpperMoves(ChessBoard board)
        {
            List<Grid> possibleMoves = new List<Grid>();

            Column colOfRook = CoordsConverter.GetColumn(Position);//la colonna in cui  posizionato il rook

            Grid upperBoundary = (Grid)(colOfRook + 20);

            //in alto

            int len = (int)CoordsConverter.GetRecord(Position) - 1;
            try
            {
                for (int i = CoordsConverter.GetCellUpFrom(Position); i >= len; i -= 10)
                {
                    if (board[CoordsConverter.Board120ToBoard64(i)] == ChessPiece.EMPTY)//se non ci sta un cazzo
                    {
                        possibleMoves.Add((Grid)i);
                    }
                    else
                    {
                        //controlla se l'ultima casella è dello stesso team, se non lo è aggiungi un'altra possibile mossa
                        if (ChessBoard.AreInDifferentTeams(board[CoordsConverter.Board120ToBoard64(i)], Piece))
                            possibleMoves.Add((Grid)i);

                        break;
                    }//controlla se l'ultima casella è dello stesso team//controlla se l'ultima casella è dello stesso team
                }
            }
            catch (ArgumentOutOfRangeException) { }


            return possibleMoves;
        }






    }
}
