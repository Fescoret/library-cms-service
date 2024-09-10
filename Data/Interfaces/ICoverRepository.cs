using KbAis.Intern.Library.Service.Web.Data.Models;

namespace KbAis.Intern.Library.Service.Web.Data.Interfaces;

public interface ICoverRepository
{
    void AddCover(Cover cover);

    void DeleteCover(int id);

    Cover GetCoverById(int id);
}
