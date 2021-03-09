using System;
using System.Collections.Generic;
using ChessEngine;
using System.Diagnostics;
namespace ConsoleApp11
{
    class Program
    {
        static void Main(string[] args)
        {
            // da testare: king, knight, pawn

            //TODO: 
            //  pawn: en passant, evoluzione arrivati a bordo della griglia
            //  king: check, checkmate, non può muoversi in una checked cell, arrocco
            //  rook: arrocco

            //  creare due bitmap dove mostri quali caselle sono sotto attacco
            //  ad ogni mossa aggiornare la bitmap e controllare se il re è sotto attacco
            //  dalle mosse del re controllare se la casella è sotto attacco e aggiungerle agli if di allpossiblemoves

            Console.OutputEncoding = System.Text.Encoding.Unicode;
            ChessBoard a = new ChessBoard();

            //King r = new King(PieceColor.White, Grid.A1);

            while (true)
            {
                string str = "";
                int i = 0;
                int k = 0;
                Console.WriteLine("abcdefgh");
                foreach (var p in a)
                {
                    switch (p)
                    {
                        case ChessPiece.WhiteKing:
                            str += new King(PieceColor.White, (Grid)CoordsConverter.Board64ToBoard120(k)).ToString();
                            break;
                        case ChessPiece.WhiteQueen:
                            str += new Queen(PieceColor.White, (Grid)CoordsConverter.Board64ToBoard120(k)).ToString();
                            break;
                        case ChessPiece.WhiteRook:
                            str += new Rook(PieceColor.White, (Grid)CoordsConverter.Board64ToBoard120(k)).ToString();
                            break;
                        case ChessPiece.WhiteBishop:
                            str += new Bishop(PieceColor.White, (Grid)CoordsConverter.Board64ToBoard120(k)).ToString();
                            break;
                        case ChessPiece.WhiteKnight:
                            str += new Knight(PieceColor.White, (Grid)CoordsConverter.Board64ToBoard120(k)).ToString();
                            break;
                        case ChessPiece.WhitePawn:
                            str += new Pawn(PieceColor.White, (Grid)CoordsConverter.Board64ToBoard120(k)).ToString();
                            break;
                        case ChessPiece.BlackKing:
                            str += new King(PieceColor.Black, (Grid)CoordsConverter.Board64ToBoard120(k)).ToString();
                            break;
                        case ChessPiece.BlackQueen:
                            str += new Queen(PieceColor.Black, (Grid)CoordsConverter.Board64ToBoard120(k)).ToString();
                            break;
                        case ChessPiece.BlackRook:
                            str += new Rook(PieceColor.Black, (Grid)CoordsConverter.Board64ToBoard120(k)).ToString();
                            break;
                        case ChessPiece.BlackBishop:
                            str += new Bishop(PieceColor.Black, (Grid)CoordsConverter.Board64ToBoard120(k)).ToString();
                            break;
                        case ChessPiece.BlackKnight:
                            str += new Knight(PieceColor.Black, (Grid)CoordsConverter.Board64ToBoard120(k)).ToString();
                            break;
                        case ChessPiece.BlackPawn:
                            str += new Pawn(PieceColor.Black, (Grid)CoordsConverter.Board64ToBoard120(k)).ToString();
                            break;
                        case ChessPiece.EMPTY:
                            str += "-";
                            break;
                        default:
                            break;
                    }
                    if (i == 7)
                    {
                        str += (k / (i + 1) + 1) + "\r\n";
                        i = -1;
                    }

                    ++k;
                    ++i;
                }
                Console.WriteLine(str);

                string str1 = Console.ReadLine();
                string str2 = Console.ReadLine();

                a.MakeAMove(CoordsConverter.ParseToGrid(str1), CoordsConverter.ParseToGrid(str2));

            }

            Console.ReadLine();
        }



    }

}





















  /*  class ChessBoard
    {


    List<ChessPiece> whites = new List<ChessPiece>(16);
        List<ChessPiece> blacks = new List<ChessPiece>(16);

        int[,] map = new int[8, 8]
        {
            {0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0 },
        };

        public ChessBoard()
        {
            _fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        }
        private string _fen;

        public string Fen
        {
            get => _fen;
            set => _fen = value;
        }

        public void MapPieces()
        {
            //dividi fen in 8 parti per pezzi
        }

        public List<ChessPiece> FindRecord()
        {
            List<ChessPiece> toReturn = new List<ChessPiece>();
            char currentChar = ' ';

            int whiteSpaces = 0;
            int listIndex = 0;
            for (int i = 0; i < _fen.Length; i++)
            {
                if (_fen[i] <= 56 && _fen[i] >= 1)
                {
                    whiteSpaces = int.Parse(_fen[i].ToString());
                    listIndex += whiteSpaces;
                    for (int j = listIndex - whiteSpaces; j < listIndex; j++)
                    {
                        toReturn[j] = ChessPiece.EMPTY;
                    }
                }
                else
                {
                    toReturn[listIndex] = (ChessPiece)_fen[i];
                }
                listIndex++;
            }

            return null;
        }



        public int Board120ToBoard64(int n)
        {
            if (n >= (int)Grid.A1 && n <= (int)Grid.H1)
                return n - 21;
            else if (n >= (int)Grid.A2 && n <= (int)Grid.H2)
                return n - 23;
            else if (n >= (int)Grid.A3 && n <= (int)Grid.H3)
                return n - 25;
            else if (n >= (int)Grid.A4 && n <= (int)Grid.H4)
                return n - 27;
            else if (n >= (int)Grid.A5 && n <= (int)Grid.H5)
                return n - 29;
            else if (n >= (int)Grid.A6 && n <= (int)Grid.H6)
                return n - 31;
            else if (n >= (int)Grid.A7 && n <= (int)Grid.H7)
                return n - 33;
            else if (n >= (int)Grid.A8 && n <= (int)Grid.H8)
                return n - 35;
            else return -1;
        }
        public int Board64ToBoard120(int n)
        {
        if (n >= 0 && n <= 7)
            return n + 21;
        else if (n >= 8 && n <= 15)
            return n + 23;
        else if (n >= 16 && n <= 23)
            return n + 25;
        else if (n >= 24 && n <= 31)
            return n + 27;
        else if (n >= 32 && n <= 39)
            return n + 29;
        else if (n >= 40 && n <= 47)
            return n + 31;
        else if (n >= 48 && n <= 55)
            return n + 33;
        else if (n >= 56 && n <= 63)
            return n + 35;
        else return -1;


        return n;
        }



}

/*
 0  1   2   3   4   5   6   7   8    9
10  11  12  13  14  15  16  17  18   19
   -a---b----c---d--e---f---g---h--
20 |21  22  23  24  25  26  27  28|  29
30 |31  32  33  34  35  36  37  38|  39
40 |41  42  43  44  45  46  47  48|  49
50 |51  52  53  54  55  56  57  58|  59
60 |61  62  63  64  65  66  67  68|  69
70 |71  72  73  74  75  76  77  78|  79
80 |81  82  83  84  85  86  87  88|  89
90 |91  92  93  94  95  96  97  98|  99
   --------------------------------
100 101 102 103 104 105 106 107 108 109
110 111 112 113 114 115 116 117 118 119
 *//*
enum Grid
{
    A1 = 21, B1, C1, D1, E1, F1, G1, H1,
    A2 = 31, B2, C2, D2, E2, F2, G2, H2,
    A3 = 41, B3, C3, D3, E3, F3, G3, H3,
    A4 = 51, B4, C4, D4, E4, F4, G4, H4,
    A5 = 61, B5, C5, D5, E5, F5, G5, H5,
    A6 = 71, B6, C6, D6, E6, F6, G6, H6,
    A7 = 81, B7, C7, D7, E7, F7, G7, H7,
    A8 = 91, B8, C8, D8, E8, F8, G8, H8, NULL = 0
}

enum Record
{
    Row1 = 1, Row2, Row3, Row4, Row5, Row6, Row7, Row8, RowNULL = 0
}
enum Colomn
{
    Colomn1 = 1, Colomn2, Colomn3, Colomn4, Colomn5, Colomn6, Colomn7, Colomn8, ColomnNULL = 0
}
enum ChessPiece
{
    WhiteKing = 75, WhiteQueen = 81, WhiteRook, WhiteBishop = 66, WhiteKnigh = 78, WhitePawn = 80, BlackKing = 107, BlackQueen = 113, BlackRook, BlackBishop = 98, BlackKnight = 110, BlackPawn = 112, EMPTY = 0
}

*/