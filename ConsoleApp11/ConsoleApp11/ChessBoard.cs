using System;
using System.Collections;
using System.Collections.Generic;
//Forsyth Edward's Notation
//
//neri in minuscolo
//numeri interi = numero di spazi
//slash = a capo
//w / b -> prossima mossa muove bianco o nero                                                                                               w
//KQkq -> castling -> k = castle dal re, q = castle dalla regina                                                                            KQkq
//possibili en passant (- se non ce ne sono, se ce ne sono -> casella dove è possibile farlo (dove finirà la pawn alla prossima mossa))     -
//numero di mosse senza che un pezzo sia stato catturato, patta quando arriva a 100                                                         0
//numero di turni fatti (aumenta di 1 ogni volta che nero finisce la mossa)                                                                 1

//controlla validità di una mossa e modifica il codice fen

using System.Diagnostics;

namespace ChessEngine
{
    /// <summary>
    ///     Rappresenta la tabella di una partita di scacchi
    /// </summary>
    internal class ChessBoard : IEnumerable<ChessPiece>
    {
         /// <summary>
         ///    Crea una nuova istanza di <see cref="ChessBoard"/>
         /// </summary>
         /// <param name="fen">La notazione di fen da usare in questa istanza di <see cref="ChessBoard"/></param>
        public ChessBoard(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            _fen = fen;
            InitializePieces();//creare un parser per fen
        }

        /// <summary>
        /// scrive o legge l'elemento all'indice specificato
        /// </summary>
        /// <param name="index">l'indice dell'elemento da restituire</param>
        /// <returns>l'elemento dell'indice specificato</returns>
        public ChessPiece this[int index]
        {
            get
            {
                return board[index];
            }
            set
            {
                board[index] = value;
            }
        }

        // i pezzi fisici nel gioco
        protected List<IChessPiece> whites = new List<IChessPiece>(16);
        public King whiteKing;
        protected List<IChessPiece> blacks = new List<IChessPiece>(16);
        public King blackKing;

        //la griglia con dentro i pezzi
        protected List<ChessPiece> board = new List<ChessPiece>();
        protected List<IChessPiece> chessPieces = new List<IChessPiece>();

        //le bitmap che mostrano le zone attaccate da ogni pedina
        protected bool[] whiteAttacks = new bool[64];
        protected bool[] blackAttacks = new bool[64];


        protected bool whiteQueenSideCastle;
        protected bool blackQueenSideCastle;
        protected bool whiteKingSideCastle;
        protected bool blackKingSideCastle;

        protected string enPassant;
        protected int mosseDaUltimaCattura;
        protected int turni;

        protected string _fen;
        protected bool whitesTurn = true;

        /// <summary>
        ///     Notazione che descrive una posizione precisa e specifica di una partita di scacchi
        /// </summary>
        public string Fen
        {
            get => _fen;
            set => _fen = value;
        }

        /// <summary>
        ///     Restituisce il numero di elementi in <see cref="ChessBoard"/>
        /// </summary>
        public int Count
        {
            get => board.Count;
        }

        /// <summary>
        /// La bitmap che mostra quali caselle sono sotto attacco
        /// </summary>
        public bool[] WhiteAttackedSquares
        {
            get => whiteAttacks;
        }
        public bool[] BlackAttackedSquares
        {
            get => blackAttacks;
        }

        /// <summary>
        ///     Estrae dalla notazione di Fen attuale la posizione di tutte le pedine
        /// </summary>
        /// <returns>La tabella di gioco contenente i pezzi in posizione</returns>
        public List<ChessPiece> FindRecords()//funziona
        {
            List<ChessPiece> toReturn = new List<ChessPiece>();

            int whiteSpaces = 0;
            int listIndex = 0;
            for (int i = 0; i < _fen.Length && toReturn.Count < 64; i++)
            {
                if (_fen[i] <= 56 && _fen[i] >= 49)
                {
                    whiteSpaces = int.Parse(_fen[i].ToString());
                    listIndex += whiteSpaces;
                    for (int j = listIndex - whiteSpaces; j < listIndex; j++)
                    {
                        toReturn.Add(ChessPiece.EMPTY);
                    }
                }
                else if (_fen[i] != '/')
                {
                    toReturn.Add((ChessPiece)_fen[i]);
                }
                listIndex++;
            }

            return toReturn;
        }

        /// <summary>
        ///     Inizializza gli oggetti <see cref="IChessPiece"/> 
        /// </summary>
        public void InitializePieces()
        {
            //throw new NotImplementedException("fare in modo di iterare in board e inizializzare i singoli oggetti nelle liste whites e blacks con posizione basata dall'indice di board trasformato da 64 a 120");

            //crea la board in base ai record
            //tramite la board si inizializzano gli oggetti
            //si creano le bitmap che identificano i pezzi che vengono attaccati

            //creare anche lista di IChessPiece per accesso facile
            whites = new List<IChessPiece>(16);
            blacks = new List<IChessPiece>(16);



            board = FindRecords();

            int whitesIndex = 0;
            int blacksIndex = 0;

            for (int i = 0; i < board.Count; i++)
            {
                switch (board[i])
                {
                    case ChessPiece.WhiteKing:
                        whites.Add(new King(PieceColor.White, (Grid)CoordsConverter.Board64ToBoard120(i)));
                        chessPieces.Add(whites[whitesIndex]);
                        whiteKing = new King(PieceColor.White, (Grid)CoordsConverter.Board64ToBoard120(i));
                        whitesIndex++;
                        break;
                    case ChessPiece.WhiteQueen:
                        whites.Add(new Queen(PieceColor.White, (Grid)CoordsConverter.Board64ToBoard120(i)));
                        chessPieces.Add(whites[whitesIndex]);
                        whitesIndex++;
                        break;
                    case ChessPiece.WhiteRook:
                        whites.Add(new Rook(PieceColor.White, (Grid)CoordsConverter.Board64ToBoard120(i)));
                        chessPieces.Add(whites[whitesIndex]);
                        whitesIndex++;
                        break;
                    case ChessPiece.WhiteBishop:
                        whites.Add(new Bishop(PieceColor.White, (Grid)CoordsConverter.Board64ToBoard120(i)));
                        chessPieces.Add(whites[whitesIndex]);
                        whitesIndex++;
                        break;
                    case ChessPiece.WhiteKnight:
                        whites.Add(new Knight(PieceColor.White, (Grid)CoordsConverter.Board64ToBoard120(i)));
                        chessPieces.Add(whites[whitesIndex]);
                        whitesIndex++;
                        break;
                    case ChessPiece.WhitePawn:
                        whites.Add(new Pawn(PieceColor.White, (Grid)CoordsConverter.Board64ToBoard120(i)));
                        chessPieces.Add(whites[whitesIndex]);
                        whitesIndex++;
                        break;
                    case ChessPiece.BlackKing:
                        blacks.Add(new King(PieceColor.Black, (Grid)CoordsConverter.Board64ToBoard120(i)));
                        blackKing = new King(PieceColor.Black, (Grid)CoordsConverter.Board64ToBoard120(i));
                        chessPieces.Add(blacks[blacksIndex]);
                        blacksIndex++;
                        break;
                    case ChessPiece.BlackQueen:
                        blacks.Add(new Queen(PieceColor.Black, (Grid)CoordsConverter.Board64ToBoard120(i)));
                        chessPieces.Add(blacks[blacksIndex]);
                        blacksIndex++;
                        break;
                    case ChessPiece.BlackRook:
                        blacks.Add(new Rook(PieceColor.Black, (Grid)CoordsConverter.Board64ToBoard120(i)));
                        chessPieces.Add(blacks[blacksIndex]);
                        blacksIndex++;
                        break;
                    case ChessPiece.BlackBishop:
                        blacks.Add(new Bishop(PieceColor.Black, (Grid)CoordsConverter.Board64ToBoard120(i)));
                        chessPieces.Add(blacks[blacksIndex]);
                        blacksIndex++;
                        break;
                    case ChessPiece.BlackKnight:
                        blacks.Add(new Knight(PieceColor.Black, (Grid)CoordsConverter.Board64ToBoard120(i)));
                        chessPieces.Add(blacks[blacksIndex]);
                        blacksIndex++;
                        break;
                    case ChessPiece.BlackPawn:
                        blacks.Add(new Pawn(PieceColor.Black, (Grid)CoordsConverter.Board64ToBoard120(i)));
                        chessPieces.Add(blacks[blacksIndex]);
                        blacksIndex++;
                        break;
                    case ChessPiece.EMPTY:
                        chessPieces.Add(null);
                        break;
                    default:
                        break;
                }
            }

            whites.TrimExcess();
            blacks.TrimExcess();

            //creato board e associato tutti i pezzi in whites e blacks

            //fare bitmap di zone attaccate

            blackAttacks = FillBlackAttackedCellBitMap(blacks);
            whiteAttacks = FillWhiteAttackedCellBitMap(whites);
        }

        /// <summary>
        ///     controlla tutte le mosse possibili di ogni elemento di <see cref="whites"/> o di <see cref="blacks"/>
        /// </summary>
        /// <param name="team">la squadra di cui calcolare la bitmap</param>
        /// <param name="reset">specifica se va resettata la bitmap</param>
        public void FillAttackedCellBitMap(PieceColor team)
        {
            if (team == PieceColor.Black)
            {
                blackAttacks = FillBlackAttackedCellBitMap(blacks);
            }
            else if (team == PieceColor.White)
            {
                whiteAttacks = FillWhiteAttackedCellBitMap(whites);
            }
                
        }
        private bool[] FillWhiteAttackedCellBitMap(List<IChessPiece> whites)
        {
            bool[] toReturn = new bool[64];


            List<Grid> attackedCellsBlack = new List<Grid>();
            for (int i = 0; i < blacks.Count; i++)//prende tutti i bianchi e estrae tutte le mosse
            {
                try
                {
                    attackedCellsBlack.AddRange(whites[i].AllPossibleMoves(this, blackAttacks));
                }
                catch (ArgumentOutOfRangeException) { }
            }
            for (int i = 0; i < attackedCellsBlack.Count; i++)//mette tutte le mosse nella bitmap
            {
                try 
                { 
                toReturn[CoordsConverter.Board120ToBoard64((int)attackedCellsBlack[i])] = true;
                }
                catch (ArgumentOutOfRangeException) { }
            }

            return toReturn;
        }
        private bool[] FillBlackAttackedCellBitMap(List<IChessPiece> blacks)
        {
            bool[] toReturn = new bool[64];

            List<Grid> attackedCellsWhite = new List<Grid>();
            for (int i = 0; i < whites.Count; i++)//prende tutti i bianchi e estrae tutte le mosse
            {
                try
                {
                    attackedCellsWhite.AddRange(whites[i].AllPossibleMoves(this, whiteAttacks));
                }
                catch (ArgumentOutOfRangeException) { }
                
            }
            for (int i = 0; i < attackedCellsWhite.Count; i++)//mette tutte le mosse nella bitmap
            {
                try
                {
                    toReturn[CoordsConverter.Board120ToBoard64((int)attackedCellsWhite[i])] = true;
                }
                catch (ArgumentOutOfRangeException) { }
            }
            return toReturn;
        }

        [Obsolete("MakeAMove", true)]
        /// <summary>
        ///     Sposta la pedina da pos a des
        /// </summary>
        /// <param name="pos">la posizione della pedina da spostare</param>
        /// <param name="des">la destinazione della pedina da spostare</param>
        /// <param name="whiteTurn">il turno</param>
        public void MoveInBoard(Grid pos, Grid des, bool whiteTurn)//sicuramente non funziona
        {

            var tmp = board[CoordsConverter.Board120ToBoard64((int)pos)];
            board[CoordsConverter.Board120ToBoard64((int)pos)] = ChessPiece.EMPTY;
            board[CoordsConverter.Board120ToBoard64((int)des)] = tmp;

            if (whiteTurn)
                for (int i = 0; i < whites.Count; i++)
                {
                    if (pos == whites[i].Position)
                    {
                        whites[i].Position = des;
                    }
                }
            else
                for (int i = 0; i < blacks.Count; i++)
                {
                    if (pos == blacks[i].Position)
                    {
                        blacks[i].Position = des;
                    }
                }


        }

        /// <summary>
        ///     Specifica se p1 e p2 appartengono in squadre differenti
        /// </summary>
        /// <param name="p1">il primo oggetto <see cref="ChessPiece"/></param>
        /// <param name="p2">il primo oggetto <see cref="ChessPiece"/></param>
        /// <returns>restituisce un valore booleano che specifica se p1 e p2 appartengono in squadre differenti</returns>
        public static bool AreInDifferentTeams(ChessPiece p1, ChessPiece p2)
        {
            if (p1 == ChessPiece.EMPTY || p2 == ChessPiece.EMPTY) return true;
            return (((int)p1 >= 65 && (int)p1 <= 90) && ((int)p2 >= 97 && (int)p2 <= 122)) || (((int)p2 >= 65 && (int)p2 <= 90) && ((int)p1 >= 97 && (int)p1 <= 122));
            //          il primo pezzo è in maiuscolo ed il secondo è in minuscolo
        }

        /// <summary>
        ///     Not implemented yet
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dest"></param>
        public void MakeAMove(Grid pos, Grid dest)
        {
           // throw new NotImplementedException();
            //quando si muove un pezzo:
            //ricalcolare le celle attaccate dal nemico e metterle in bitmap, se una attacca il re mossa invalida
            //sposta oggetto e ricalcolare celle attaccate se la mossa va a buon fine, ricreare bitmap e controllare per stalemate o checkmate

            //copiaggio di tutto (tabella di gioco con enum e con interfacce, le 2 bitmap)



            //sposta pedina nella copia
            //calcola bitmap nemica
            //controlla se il re è sotto attacco
            //se il re è sotto attacco: mossa invalida, lancia eccezione(?)
            //se il re non è sotto attacco: sposta la pedina in board, calcola bitmap amica, rimpiazza la bitmap nemica con la copia precedente

            //se il re, o una torre si è mossa, arrocco impossibile
            //controlla bitmap amica e posizione del re nemico, controlla per scacco o stalemate

            //il re non si può muovere in bitmap nemica, aggiungere parametro bitmap in allpossiblemoves in classe King



            //TODO: LA BITMAP NON SI AGGIORNA VEDERE PERCHE'


            //DUPLICAZIONE DEGLI ATTRIBUTI CHE SERVIRANNO PER INTERROGARE LA MOSSA
            List<ChessPiece> copyOfBoard = new List<ChessPiece>();
            List<IChessPiece> copyOfChessPieces = new List<IChessPiece>();
            King copyOfBlackKing = blackKing;
            King copyOfWhiteKing = whiteKing;

            bool[] copyOfBlackBitMap = new bool[64];
            bool[] copyOfWhiteBitMap = new bool[64];


            //GLI ATTRIBUTI VENGONO COPIATI
            copyOfBoard.AddRange(board);
            copyOfChessPieces.AddRange(chessPieces);

            Array.Copy(blackAttacks, copyOfBlackBitMap, blackAttacks.Length);
            Array.Copy(whiteAttacks, copyOfWhiteBitMap, whiteAttacks.Length);

            //IL COLORE DI CHI SI STA MUOVENDO
            PieceColor actualTeam;
            try
            {
                actualTeam = copyOfChessPieces[CoordsConverter.Board120ToBoard64((int)pos)].PieceColor;
            }
            catch (Exception) { return; }


            //SE LA MOSSA E' VALIDA (se il pezzo ad indice della posizione iniziale si può muovere a dest)
            if (actualTeam == (whitesTurn ? PieceColor.White : PieceColor.Black))
            {
                if (copyOfChessPieces[CoordsConverter.Board120ToBoard64((int)pos)].CanMoveTo(dest, this, actualTeam == PieceColor.Black ? copyOfBlackBitMap : copyOfWhiteBitMap))
                {

                    //passano quasi 2 secondi troppo lento
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    //VENGONO RIMPIAZZATI I PEZZI NELLA COPIA
                    copyOfBoard[CoordsConverter.Board120ToBoard64((int)dest)] = copyOfBoard[CoordsConverter.Board120ToBoard64((int)pos)];
                    copyOfBoard[CoordsConverter.Board120ToBoard64((int)pos)] = ChessPiece.EMPTY;

                    copyOfChessPieces[CoordsConverter.Board120ToBoard64((int)dest)] = copyOfChessPieces[CoordsConverter.Board120ToBoard64((int)pos)];
                    copyOfChessPieces[CoordsConverter.Board120ToBoard64((int)dest)].Position = dest;
                    copyOfChessPieces[CoordsConverter.Board120ToBoard64((int)pos)] = null;

                    //SE IL RE SI E' MOSSO MODIFICARE LA POSIZIONE DELLA COPIA DEL RE
                    if (copyOfChessPieces[CoordsConverter.Board120ToBoard64((int)dest)].Piece == ChessPiece.BlackKing)//se è un re
                    {
                        copyOfBlackKing.Position = dest;
                    }
                    else if (copyOfChessPieces[CoordsConverter.Board120ToBoard64((int)dest)].Piece == ChessPiece.BlackKing)
                    {
                        copyOfWhiteKing.Position = dest;
                    }

                    //TROVA TUTTE LE MOSSE POSSIBILI DI ENTRAMBI I GIOCATORI
                    List<Grid> allBlackPossibleMoves = new List<Grid>();
                    List<Grid> allWhitePossibleMoves = new List<Grid>();

                    for (int i = 0; i < copyOfChessPieces.Count; i++)
                    {
                        try
                        {
                            if (copyOfChessPieces[i].PieceColor == PieceColor.White)
                            {
                                allWhitePossibleMoves.AddRange(copyOfChessPieces[i].AllPossibleMoves(this, copyOfBlackBitMap));
                            }
                            else if (copyOfChessPieces[i].PieceColor == PieceColor.Black)
                            {
                                allBlackPossibleMoves.AddRange(copyOfChessPieces[i].AllPossibleMoves(this, copyOfWhiteBitMap));
                            }
                        }
                        catch (NullReferenceException) { }

                    }
                    sw.Stop();
                    Console.WriteLine($"tempo impiegato per vedere tutte le mosse possibili{sw.Elapsed}");

                    sw = new Stopwatch();
                    sw.Start();
                    //TROVA LE BITMAP
                    copyOfBlackBitMap = new bool[64];
                    copyOfWhiteBitMap = new bool[64];
                    foreach (Grid blackMove in allBlackPossibleMoves)
                    {
                        copyOfBlackBitMap[CoordsConverter.Board120ToBoard64((int)blackMove)] = true;
                    }
                    foreach (Grid whiteMove in allWhitePossibleMoves)
                    {
                        copyOfWhiteBitMap[CoordsConverter.Board120ToBoard64((int)whiteMove)] = true;
                    }
                    sw.Stop();
                    Console.WriteLine($"tempo impiegato per creare le 2 bitmap{sw.Elapsed}");

                    //CONTROLLA SE IL RE E' SOTTO ATTACCO DOPO LA MOSSA
                    if (actualTeam == PieceColor.White)
                    {
                        if (copyOfBlackBitMap[CoordsConverter.Board120ToBoard64((int)whiteKing.Position)])
                        {
                            throw new InvalidOperationException("Il re è sotto attacco");
                        }
                    }
                    else if (actualTeam == PieceColor.Black)
                    {
                        if (copyOfWhiteBitMap[CoordsConverter.Board120ToBoard64((int)blackKing.Position)])
                        {
                            throw new InvalidOperationException("Il re è sotto attacco");
                        }
                    }
                }
                whitesTurn = !whitesTurn;
            }
            whiteKing = copyOfWhiteKing;
            blackKing = copyOfBlackKing;
            board = copyOfBoard;
            chessPieces = copyOfChessPieces;
            blackAttacks = copyOfBlackBitMap;
            whiteAttacks = copyOfWhiteBitMap;

        }



        IEnumerator<ChessPiece> IEnumerable<ChessPiece>.GetEnumerator()
            => board.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => board.GetEnumerator();

    }
}
/*
//if (Regex.IsMatch(move, "="))
//{
//    //pawn promotion
//}
//if (move == "0-0" || move == "0-0-0")
//{
//    //castle
//}
//string piece = Regex.Match(move, "[KQNBR]").Value;
//if (piece == "")
//{
//    piece = "P";
//}
//bool capture = move.IndexOf("x") > -1;
//bool checkmate = move.IndexOf("#") > -1;
//bool check = move.IndexOf("+") > -1;

//if (piece == "P")
//{

//}
//else if (piece == "K")
//{

//}
//else if (piece == "Q")
//{

//}
//else if (piece == "N")
//{

//}
//else if (piece == "B")
//{

//}
//else if (piece == "R")
//{

//}
*/


/*  LISTA BOARD INDICI
 0  1  2  3  4  5  6  7  
 8  9  10 11 12 13 14 15 
 16 17 18 19 20 21 22 23 
 24 25 26 27 28 29 30 31
 32 33 34 35 36 37 38 39
 40 41 42 43 44 45 46 47 
 48 49 50 51 52 53 54 55 
 56 57 58 59 60 61 62 63
*/
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
*/




