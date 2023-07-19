using Shared.RequestModels;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IArtikalRepository
    {
        Task<int> Create(ArtikalRequestModel model);
    }
}
