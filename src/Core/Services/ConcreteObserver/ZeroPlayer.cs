using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ZeroPlayer : IZeroPlayer
    {

        #region Private Properties

        private readonly List<List<bool>> _states = new List<List<bool>>();

        #endregion

        #region Private Methods

        private async Task RuleCheck(Cell cell)
        {
            var neighbors = cell.Neighbors;
            if (neighbors.Count() > 0)
            {
                var liveCells = neighbors.Where(o => o.CellState == State.Live);
                var deadCells = neighbors.Where(o => o.CellState == State.Dead);

                if (cell.CellState == State.Dead && liveCells.Count() == 3)
                    _states[cell.Row][cell.Column] = true; 

                if (cell.CellState == State.Live && liveCells.Count() >= 2 && liveCells.Count() <= 3)
                    _states[cell.Row][cell.Column] = true; 

                if (liveCells.Count() <= 1 || liveCells.Count() > 3)
                    _states[cell.Row][cell.Column] = false;
            }
            await Task.CompletedTask;
        }

        #endregion

        #region Public Methods

        public async Task Update(List<List<Cell>> board)
        {
            InitializeStates(board.Count, board[0].Count);
            for (int row = 0; row < board.Count; row++)
            {
                for (int col = 0; col < board[0].Count; col++)
                {
                    var cell = board[row][col];
                    cell.Neighbors = GetNeighbors(board, cell);
                    await RuleCheck(cell);
                }
            }
            ReviseStates(board);
        }

        #endregion

        private List<Cell> GetNeighbors(List<List<Cell>> board, Cell cell)
        {
            var row = cell.Row;
            var col = cell.Column;

            var neighBors = new List<Cell>();

            if (row > 0 && col > 0)
                neighBors.Add(board[row - 1][col - 1]);

            if (row > 0)
                neighBors.Add(board[row - 1][col]);

            if (col < board[row].Count - 1 && row > 0)
                neighBors.Add(board[row - 1][col + 1]);

            if (col > 0)
                neighBors.Add(board[row][col - 1]);

            if (col < board[row].Count - 1)
                neighBors.Add(board[row][col + 1]);

            if (row < board.Count - 1 && col > 0)
                neighBors.Add(board[row + 1][col - 1]);

            if (row < board.Count - 1)
                neighBors.Add(board[row + 1][col]);

            if (row < board.Count - 1 && col < board[row].Count - 1)
                neighBors.Add(board[row + 1][col + 1]);

            return neighBors;
        }

        private void ReviseStates(List<List<Cell>> board)
        {
            for (int row = 0; row < board.Count; row++)
            {
                for (int col = 0; col < board[0].Count; col++)
                {
                    var cell = board[row][col];
                    cell.CellState = (_states[row][col]) ? State.Live : State.Dead;
                    board[row][col] = cell;
                }
            }
        }

        private void InitializeStates(int rows, int cols)
        {
            for (int row = 0; row < rows; row++)
            {
                var columns = new List<bool>();
                for (int col = 0; col < cols; col++)
                {
                    columns.Add(false);
                }
                _states.Add(columns);
            }
        }

    }
}
