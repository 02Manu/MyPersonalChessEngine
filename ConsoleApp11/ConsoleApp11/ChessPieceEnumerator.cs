using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessEngine
{
    //esempio di implementazione di IEnumerator, inutile in questo caso
    internal class ChessPieceEnumerator : IEnumerator<ChessPiece>
    {
        private ChessBoard _chessBoard;
        private int _i;

        public ChessPieceEnumerator(ChessBoard chessBoard)
        {
            _chessBoard = chessBoard;
            _i = -1;
        }

        ChessPiece IEnumerator<ChessPiece>.Current => _chessBoard[_i];

        object IEnumerator.Current => _chessBoard[_i];

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        bool IEnumerator.MoveNext()
            => ++_i < _chessBoard.Count;

        void IEnumerator.Reset()
             => _i = -1;
        
    }
}
