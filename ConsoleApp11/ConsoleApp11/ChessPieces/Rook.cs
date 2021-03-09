using System;
using System.Collections.Generic;

namespace ChessEngine
{
    /// <summary>
    ///     Rappresenta una torre
    /// </summary>
    internal class Rook : IChessPiece, IComparable//COMPLETATO! PER ORA FUNZIONA TUTTO, TODO: ARROCCO
    {
        /// <summary>
        ///     Crea una nuova istanza di <see cref="Rook"/>
        /// </summary>
        /// <param name="pieceColor">Il team a cui appartiene questa istanza di <see cref="Rook"/></param>
        /// <param name="position">La posizione nel gioco di questa istanza di <see cref="Rook"/></param>
        public Rook(PieceColor pieceColor, Grid position = Grid.NULL)
        {
            PieceColor = pieceColor;
            Position = position;
        }

        public ChessPiece Piece { get { return PieceColor == PieceColor.White ? ChessPiece.WhiteRook : ChessPiece.BlackRook; } }
        public Grid Position { get; set; }
        public int Val { get => 5; }
        public PieceColor PieceColor { get; private set; }

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
            return PieceColor == PieceColor.White ? "\u2656" : "\u265C";
        }
    }
}
