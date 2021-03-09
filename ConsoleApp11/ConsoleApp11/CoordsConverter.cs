using System;

namespace ChessEngine
{
    /// <summary>
    /// utility tools che permettono di eseguire conversioni riguardanti le coordinate
    /// </summary>
    static class CoordsConverter
    {
        /// <summary>
        /// data una posizione ritorna la colonna
        /// </summary>
        /// <param name="pos">posizione della quale si vuole effettuare la conversione</param>
        /// <returns>colonna della posizione</returns>
        public static Column GetColumn(Grid pos)
        {
            if (pos == Grid.NULL || !Enum.IsDefined(typeof(Grid), (int)pos)) return Column.ColumnNULL;//se non esiste il valore
            return (Column)int.Parse(((int)pos).ToString()[1].ToString());
        }

        /// <summary>
        /// data una posizione ritorna la riga
        /// </summary>
        /// <param name="pos">posizione della quale si vuole effettuare la conversione</param>
        /// <returns>riga della posizione</returns>
        public static Record GetRecord(Grid pos)
        {
            if (pos == Grid.NULL || !Enum.IsDefined(typeof(Grid), (int)pos)) return Record.RowNULL;
            return (Record)int.Parse(((int)pos - 10).ToString()[0].ToString());
        }

        /// <summary>
        /// date le due coordinate x e y ritorna un valore unico
        /// </summary>
        /// <param name="r">riga</param>
        /// <param name="c">colonna</param>
        /// <returns>coordinata</returns>
        public static Grid GetCoordinates(Record r, Column c)
        {
            return (Grid)((((int)r + 1) * 10) + c);
        }

        /// <summary>
        ///     converte la coordinata espressa in indice in una coordinata di una tabella che non considera le boundaries (da scala 120 a scala 64)
        /// </summary>
        /// <param name="n">la coordinata in formato intero</param>
        /// <returns>la coordinata in formato intero in base 64</returns>
        public static int Board120ToBoard64(int n)
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

        /// <summary>
        ///     converte la coordinata espressa in indice in una coordinata di una tabella che considera le boundaries (da scala 64 a scala 120)
        /// </summary>
        /// <param name="n">la coordinata in formato intero</param>
        /// <returns>la coordinata in formato intero in base 120</returns>
        public static int Board64ToBoard120(int n)
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
        }
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

        /// <summary>
        ///     Data in input una posizione Grid, trova la coordinata sottostante a pos di distance caselle
        /// </summary>
        /// <param name="pos">posizione attuale</param>
        /// <param name="distance">posizione distanziata</param>
        /// <returns>La coordinata sottostante di distance caselle</returns>
        public static int GetCellDownFrom(Grid pos, int distance=1)
        {
            return (int)pos + 10 * distance;
        }

        /// <summary>
        ///     Data in input una posizione Grid, trova la coordinata sottostante a pos di distance caselle
        /// </summary>
        /// <param name="pos">posizione attuale</param>
        /// <param name="distance">posizione distanziata</param>
        /// <returns>La coordinata sottostante di distance caselle</returns>
        public static int GetCellUpFrom(Grid pos, int distance = 1)
        {
            return (int)pos - 10 * distance;
        }


        public static int GetTopRightCellFrom(Grid pos, int distance = 1)
        {
            return (int)pos - 9 * distance;
        }
        public static int GetBottomRightCellFrom(Grid pos, int distance = 1)
        {
            return (int)pos + 11 * distance;
        }
        public static int GetTopLeftCellFrom(Grid pos, int distance = 1)
        {
            return (int)pos - 11 * distance;
        }
        public static int GetBottomLeftCellFrom(Grid pos, int distance = 1)
        {
            return (int)pos + 9 * distance;
        }

        public static Grid ParsedMove()
        {
            throw new NotImplementedException();
        }

        public static Grid ParseToGrid(string cell)
        {
            cell = cell.ToUpper();

            int gridBuilder = (int)cell[0] - 64;
            gridBuilder += (int.Parse(cell[1].ToString()) + 1) * 10;

            return (Grid)gridBuilder;
        }
    }
}
