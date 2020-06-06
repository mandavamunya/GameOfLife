using Core.Enum;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Cell
    {
        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Column { get; set; }

        public int Row { get; set; }

        public State CellState { get; set; } = State.Dead;

        public string CellStateName { get { return CellState.ToString().ToLower(); } }

        [JsonIgnore]
        public List<Cell> Neighbors { get; set; }
    }
}