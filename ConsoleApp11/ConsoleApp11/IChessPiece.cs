using System.Collections.Generic;

namespace ChessEngine
{
    /// <summary>
    /// contiene tutti i comportamenti che una classe 
    /// rappresentante una pedina deve rispettare,
    /// bisogna specificare:<para />
    /// -Enumerazione che specifichi la squadra della pedina<para />
    /// -Il valore intero della pedina<para />
    /// -Enumerazione che specifichi la posizione della pedina nella griglia di gioco<para />
    /// -Enumerazione che specifici il tipo di pedina<para />
    /// -Metodo che specifica la validità di una mossa data in input, considerando la partita su cui è istanziato (data in input)<para />
    /// -Metodo ToString() che stampi il carattere ASCII della pedina in questione
    /// </summary>
    interface IChessPiece
    {
        /// <summary>
        /// il colore della pedina, specifica l'appartenenza ad una squadra
        /// </summary>
        PieceColor PieceColor { get; }

        /// <summary>
        /// il valore attribuito alla pedina
        /// </summary>
        int Val { get; }

        /// <summary>
        /// la posizione attuale di questa istanza di Oggetto
        /// </summary>
        Grid Position { get; set; }

        /// <summary>
        /// il tipo di pedina
        /// </summary>
        ChessPiece Piece { get; }

        /// <summary>
        /// specifica la validità di una mossa
        /// </summary>
        /// <param name="des">la destinazione della mossa da eseguire</param>
        /// <param name="board">la tabella di gioco aggiornata</param>
        /// <returns></returns>
        bool CanMoveTo(Grid des, ChessBoard board, bool[] Enemybitmap);

        /// <summary>
        /// rappresenta il valore numerico dell'istanza nella rappresentazione di stringa equivalente
        /// </summary>
        /// <returns>carattere ASCII rappresentante la pedina</returns>
        string ToString();

        /// <summary>
        ///     trova il numero di mosse possibili per questa istanza di <see cref="IChessPiece"/>
        /// </summary>
        /// <param name="board">la partita attuale</param>
        /// <returns>una lista contenente le possibili mosse per questa istanza di <see cref="IChessPiece"/></returns>
        public List<Grid> AllPossibleMoves(ChessBoard board, bool[] Enemybitmap);
    }
}
