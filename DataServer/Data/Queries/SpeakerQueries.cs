using DataServer.Data.DataLoaders;
using DataServer.Data.Extensions;
using DataServer.Models;
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
    public class SpeakerQueries
    {
        [UseApplicationDbContext]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Speaker> GetSpeakers(
            [ScopedService] ApplicationDbContext context) =>
                context.Speakers.AsNoTracking();

        public Task<Speaker> GetSpeakerByIdAsync(
            [ID(nameof(Speaker))] int id,
            SpeakerByIdDataLoader speakerById,
            CancellationToken cancellationToken) =>
                speakerById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Speaker>> GetSpeakersByIdAsync(
            [ID(nameof(Speaker))] int[] ids,
            SpeakerByIdDataLoader speakerById,
            CancellationToken cancellationToken) =>
                await speakerById.LoadAsync(ids, cancellationToken);
    }
}
