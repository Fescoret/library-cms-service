using KbAis.Intern.Library.Service.Web.Data.Interfaces;
using KbAis.Intern.Library.Service.Web.Data.Models;
using System.Collections.Concurrent;

namespace KbAis.Intern.Library.Service.Web.Data.Repositories;

public class InMemoryCoverRepository : ICoverRepository
{
    static int concurrencyLevel = Environment.ProcessorCount * 2;
    ConcurrentDictionary<int, Cover> CoverDictionary = new ConcurrentDictionary<int, Cover>(concurrencyLevel, 128);
    int coverMaxId = -1;

    public void AddCover(Cover cover)
    {
        coverMaxId++;
        CoverDictionary[coverMaxId] = cover;
    }

    public void DeleteCover(int id)
    {
        _ = CoverDictionary.TryRemove(id, out _);
    }

    public Cover GetCoverById(int id)
    {
        return CoverDictionary[id];
    }
}
