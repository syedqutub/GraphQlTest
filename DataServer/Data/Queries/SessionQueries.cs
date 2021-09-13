using DataServer.Data.DataLoaders;
using DataServer.Data.Extensions;
using DataServer.Models;
using DataServer.Models.Types;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataServer.Data.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class SessionQueries
    {
        [UseApplicationDbContext]
        //[UseFirstOrDefault]
        [UsePaging(typeof(NonNullType<SessionType>))]
        [UseProjection]
        [UseFiltering(typeof(SessionFilterInputType))]
        [UseSorting]
        public IQueryable<Session> GetSessions(
            [ScopedService] ApplicationDbContext context) =>
                context.Sessions.AsNoTracking();

        public Task<Session> GetSessionByIdAsync(
            [ID(nameof(Session))] int id,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
                sessionById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Session>> GetSessionsByIdAsync(
            [ID(nameof(Session))] int[] ids,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
                await sessionById.LoadAsync(ids, cancellationToken);
    }
}
