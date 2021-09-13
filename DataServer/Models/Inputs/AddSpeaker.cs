using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataServer.Models.Inputs
{
    public record AddSpeakerInput(
        string Name,
        string? Bio,
        string? WebSite);

    public class SpeakerPayloadBase : Payload
    {
        protected SpeakerPayloadBase(Speaker speaker)
        {
            Speaker = speaker;
        }

        protected SpeakerPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Speaker? Speaker { get; }
    }

    public class AddSpeakerPayload : SpeakerPayloadBase
    {
        public AddSpeakerPayload(Speaker speaker)
            : base(speaker)
        {
        }

        public AddSpeakerPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}
