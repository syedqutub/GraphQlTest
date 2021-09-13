using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataServer.Models.Inputs
{
    public record RenameSessionInput(
        [ID(nameof(Session))] string SessionId,
        string Title);

    public class RenameSessionPayload : Payload
    {
        public RenameSessionPayload(Session session)
        {
            Session = session;
        }

        public RenameSessionPayload(UserError error)
            : base(new[] { error })
        {
        }

        public Session? Session { get; }
    }
}
