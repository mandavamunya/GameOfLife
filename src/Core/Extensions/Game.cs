using Core.Entities;
using System.Collections.Generic;

namespace Core.Extensions
{
    public static class Game
    {
        public static List<List<Cell>> Board { get; set; } = new List<List<Cell>>();
    }
}
