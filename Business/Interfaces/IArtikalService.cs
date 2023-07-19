using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IArtikalService
    {
        Task<int> Create(ArtikalRequestModel model);
    }
}
