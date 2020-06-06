using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IZeroPlayer
    {
        Task Update(List<List<Cell>> board);
    }
}