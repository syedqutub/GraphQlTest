using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataServer.Models.Inputs
{
    public record RenameTrackInput([ID(nameof(Track))] int Id, string Name);

    public class RenameTrackPayload : TrackPayloadBase
    {
        public RenameTrackPayload(Track track)
            : base(track)
        {
        }

        public RenameTrackPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}
