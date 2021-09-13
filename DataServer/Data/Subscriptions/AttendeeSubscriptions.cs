using DataServer.Data.DataLoaders;
using DataServer.Models;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using System.Threading;
using System.Threading.Tasks;

namespace DataServer.Data.Subscriptions
{
    [ExtendObjectType(OperationTypeNames.Subscription)]
    public class AttendeeSubscriptions
    {
        [Subscribe(With = nameof(SubscribeToOnAttendeeCheckedInAsync))]
        public SessionAttendeeCheckIn OnAttendeeCheckedIn(
            [ID(nameof(Session))] int sessionId,
            [EventMessage] int attendeeId/*,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken*/) =>
                new SessionAttendeeCheckIn(attendeeId, sessionId);

        public async ValueTask<ISourceStream<int>> SubscribeToOnAttendeeCheckedInAsync(
            int sessionId,
            [Service] ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken) =>
                await eventReceiver.SubscribeAsync<string, int>(
                    $"{nameof(OnAttendeeCheckedIn)}_{sessionId}", cancellationToken);
    }
}
