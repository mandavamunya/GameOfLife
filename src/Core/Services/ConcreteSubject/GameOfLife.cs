using Core.Entities;
using Core.Enum;
using Core.Interfaces;
using Core.Extensions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services
{
    public class GameOfLife : IGameOfLife
    {
		private static GameOfLife _instance;
		public static GameOfLife Instance {
			get
			{
				if (_instance == null)
					_instance = new GameOfLife(100,100);
				return _instance;
			}
		}

        #region Private Properties

        private IZeroPlayer _zeroPlayer;
        private bool _gameStarted = false;

        #endregion

        #region Protected Properties

        protected int _numberOfRows;
        protected int _numberOfColumns;
        protected readonly List<List<Cell>> _board = new List<List<Cell>>();

        #endregion

        #region Constructor(s)
        public GameOfLife(int numberOfRows, int numberOfColumns)
        {
            _numberOfRows = numberOfRows;
            _numberOfColumns = numberOfColumns;
            Initialize();
        }
        #endregion

        #region Public Methods
        public async Task Start()
        {
            _gameStarted = true;
            await Notify();
        }

        public async Task Stop()
        {
            _gameStarted = false;
            await Task.CompletedTask;
        }

        public async Task Notify()
        {
            while (_gameStarted)
            {
                await _zeroPlayer.Update(_board);
                Game.Board = _board;
                Thread.Sleep(100);
            }
        }

        public void AddPlayer(IZeroPlayer zeroPlayer)
        {
            _zeroPlayer = zeroPlayer;
        }

        public List<List<Cell>>GetBoard()
        {
            return _board;
        }
        #endregion

        #region Private Methods

        private void Initialize()
        {
            Random random = new Random();
            for (int row = 0; row < _numberOfRows; row++)
            {
                var columns = new List<Cell>();
                for (int col = 0; col < _numberOfColumns; col++)
                {
                    int randomNumber = random.Next(0, 100);
                    var cell = new Cell(row, col);
                    var state = (State)(randomNumber % 2);
                    cell.CellState = state;
                    columns.Add(cell);
                }
                _board.Add(columns);
            }
        }

        #endregion

    }
}
