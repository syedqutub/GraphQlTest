using DataServer.Data.DataLoaders;
using DataServer.Data.Extensions;
using DataServer.Models;
using DataServer.Models.Types;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataServer.Data.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class AttendeeQueries
    {
        [UseApplicationDbContext]
        [UsePaging(typeof(NonNullType<AttendeeType>))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Attendee> GetAttendees(
            [ScopedService] ApplicationDbContext context) =>
                context.Attendees;

        public Task<Attendee> GetAttendeeByIdAsync(
            [ID(nameof(Attendee))] int id,
            AttendeeByIdDataLoader attendeeById,
            CancellationToken cancellationToken) =>
                attendeeById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Attendee>> GetAttendeesByIdAsync(
            [ID(nameof(Attendee))] int[] ids,
            AttendeeByIdDataLoader attendeeById,
            CancellationToken cancellationToken) =>
                await attendeeById.LoadAsync(ids, cancellationToken);
    }
}
