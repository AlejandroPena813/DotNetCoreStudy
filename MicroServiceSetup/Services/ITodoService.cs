using System.Threading.Tasks;
using MicroServiceSetup.Models;

namespace MicroServiceSetup.Services
{
    public interface ITodoService
    {
        void GenMessage(Todo item);
    }
}