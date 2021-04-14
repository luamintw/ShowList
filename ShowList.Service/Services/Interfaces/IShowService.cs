using System.Collections.Generic;
using System.Threading.Tasks;
using ShowList.Service.Data;
using ShowList.Service.Model;

namespace ShowList.Service.Services.Interfaces
{
    public interface IShowService
    {
        Task<IEnumerable<ShowResponse>> GetShowList();
        Task AddShow(ShowRequest showRequest);
        Task EditShow(ShowRequest showRequest);
        Task DeleteShow(int id);
        Task<ShowResponse> FindShowById(int id);
        //Task<IEnumerable<Show>> FindShowByIds(int[] ids);
    }
}