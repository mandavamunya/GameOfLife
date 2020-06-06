using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGameOfLife
    {
        Task Start();
        Task Stop();
		Task Notify();
	}
}