namespace ChessEngine
{
    /// <summary>
    /// Specifica il colore della pedina, Nero e Bianco specificano l'appartenenza a squadre diverse
    /// </summary>
    public enum PieceColor { Black, White }

    /// <summary>
    /// rappresenta la tabella di gioco tramite dei valori che vanno da 0 a 120, escludendo dei boundaries fatti apposta
    /// per aggevolare il modo in cui limitare le pedine nella tabella, valore 0 riservato per null (out of bounds)
    /// </summary>
    public enum Grid
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

    /// <summary>
    /// rappresenta la coordinata y della tabella (da 1 a 8, 0 per null) 
    /// </summary>
    public enum Record
    {
        Row1 = 1, Row2, Row3, Row4, Row5, Row6, Row7, Row8, RowNULL = 0
    }

    /// <summary>
    /// rappresenta la coordinata x della tabella (da 1 a 8, 0 per null) 
    /// </summary>
    public enum Column
    {
        Column1 = 1, Column2, Column3, Column4, Column5, Column6, Column7, Column8, ColumnNULL = 0
    }

    /// <summary>
    /// rappresenta una pedina, bianca o nera, l'enumerazione è svolta tenendo conto del codice ASCII ufficialmente riservato per la notazione di FEN:<para />
    /// re bianco = K
    /// re nero = k<para />
    /// regina bianca = Q
    /// regina nera = q<para />
    /// torre bianca = R
    /// torre nera = r<para />
    /// alfiere bianco = B
    /// alfiere nero = b<para />
    /// cavallo bianco = N
    /// cavallo nero = n<para />
    /// pedina bianca = P
    /// pedina nera = p<para />
    /// </summary>
    public enum ChessPiece
    {
        WhiteKing = 75, WhiteQueen = 81, WhiteRook, WhiteBishop = 66, WhiteKnight = 78, WhitePawn = 80, BlackKing = 107, BlackQueen = 113, BlackRook, BlackBishop = 98, BlackKnight = 110, BlackPawn = 112, EMPTY = 0
    }
}
