using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataServer.Models.Inputs
{
    public record AddSessionInput(
        string Title,
        string? Abstract,
        [ID(nameof(Speaker))]
        IReadOnlyList<int> SpeakerIds);

    public class SessionPayloadBase : Payload
    {
        protected SessionPayloadBase(Session session)
        {
            Session = session;
        }

        protected SessionPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Session? Session { get; }
    }

    public class AddSessionPayload : SessionPayloadBase
    {
        public AddSessionPayload(Session session)
            : base(session)
        {
        }

        public AddSessionPayload(UserError error)
            : base(new[] { error })
        {
        }
    }
}
