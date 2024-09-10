using KbAis.Intern.Library.Service.Web.Features.Covers.GettingCoverById.V1;
using KbAis.Intern.Library.Service.Web.Features.Covers.GettingAllCovers.V1;
using KbAis.Intern.Library.Service.Web.Features.Covers.CreatingCover.V1;
using KbAis.Intern.Library.Service.Web.Features.Covers.DeletingCover.V1;
using KbAis.Intern.Library.Service.Web.Features.Covers.UpdatingCoverInfo.V1;

namespace KbAis.Intern.Library.Service.Web.Features.Covers;

public static class CoverEndpoints
{
    public const string ResourceName = "covers";

    public static class V1
    {
        public static void Map(WebApplication app)
        {
            GetCoverByIdEndpoint.Map(app);

            GetAllCoversEndpoint.Map(app);

            CreateCoverEndpoint.Map(app);

            DeleteCoverEndpoint.Map(app);

            UpdateCoverEndpoint.Map(app);
        }
    }
}
