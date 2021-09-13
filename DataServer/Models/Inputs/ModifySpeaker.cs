using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataServer.Models.Inputs
{
    public record ModifySpeakerInput(
        [ID(nameof(Speaker))]
        int Id,
        string? Name,
        string? Bio,
        string? WebSite);

    public class ModifySpeakerPayload : SpeakerPayloadBase
    {
        public ModifySpeakerPayload(Speaker speaker)
            : base(speaker)
        {
        }

        public ModifySpeakerPayload(UserError error)
            : base(new[] { error })
        {
        }
    }
}
