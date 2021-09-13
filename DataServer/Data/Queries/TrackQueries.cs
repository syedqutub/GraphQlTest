using DataServer.Data.DataLoaders;
using DataServer.Data.Extensions;
using DataServer.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataServer.Data.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class TrackQueries
    {
        [UseApplicationDbContext]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Track> GetTracks(
            [ScopedService] ApplicationDbContext context) =>
                context.Tracks.AsNoTracking();

        [UseApplicationDbContext]
        public Task<Track> GetTrackByNameAsync(
            string name,
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken) =>
                context.Tracks.FirstAsync(t => t.Name == name, cancellationToken);

        [UseApplicationDbContext]
        public async Task<IEnumerable<Track>> GetTrackByNamesAsync(
            string[] names,
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken) =>
                await context.Tracks.Where(t => names.Contains(t.Name)).ToListAsync(cancellationToken);

        public Task<Track> GetTrackByIdAsync(
            [ID(nameof(Track))] int id,
            TrackByIdDataLoader trackById,
            CancellationToken cancellationToken) =>
                trackById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Track>> GetTracksByIdAsync(
            [ID(nameof(Track))] int[] ids,
            TrackByIdDataLoader trackById,
            CancellationToken cancellationToken) =>
                await trackById.LoadAsync(ids, cancellationToken);
    }
}
