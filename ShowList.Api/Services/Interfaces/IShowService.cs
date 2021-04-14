using System.Collections.Generic;
using System.Threading.Tasks;
using ShowList.Api.Model;

namespace ShowList.Api.Services.Interfaces
{
    public interface IShowService
    {
        Task<IEnumerable<ShowResponse>> GetShowList();
        Task AddShow(ShowRequest showRequest);
        Task EditShow(ShowRequest showRequest);
        Task DeleteShow(int id);
    }
}