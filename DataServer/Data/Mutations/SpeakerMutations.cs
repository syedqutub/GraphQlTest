using DataServer.Data.Extensions;
using DataServer.Models;
using DataServer.Models.Inputs;
using HotChocolate;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;

namespace DataServer.Data.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class SpeakerMutations
    {
        [UseApplicationDbContext]
        public async Task<AddSpeakerPayload> AddSpeakerAsync(
            AddSpeakerInput input,
            [ScopedService] ApplicationDbContext context)
        {
            var speaker = new Speaker
            {
                Name = input.Name,
                Bio = input.Bio,
                WebSite = input.WebSite
            };

            context.Speakers.Add(speaker);
            await context.SaveChangesAsync();

            return new AddSpeakerPayload(speaker);
        }

        [UseApplicationDbContext]
        public async Task<ModifySpeakerPayload> ModifySpeakerAsync(
            ModifySpeakerInput input,
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken)
        {
            if (input.Name is null)
            {
                return new ModifySpeakerPayload(
                    new UserError("Name cannot be null", "NAME_NULL"));
            }

            Speaker? speaker = await context.Speakers.FindAsync(input.Id);

            if (speaker is null)
            {
                return new ModifySpeakerPayload(
                    new UserError("Speaker with id not found.", "SPEAKER_NOT_FOUND"));
            }

            if (input.Name is not null)
            {
                speaker.Name = input.Name;
            }

            if (input.Bio is not null)
            {
                speaker.Bio = input.Bio;
            }

            if (input.WebSite is not null)
            {
                speaker.WebSite = input.WebSite;
            }

            await context.SaveChangesAsync(cancellationToken);

            return new ModifySpeakerPayload(speaker);
        }
    }
}
