
using SurveyBasket.Api.Contracts.Requests;

namespace SurveyBasket.Api.Mapping;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Poll, PollResponse>()
            .Map(dest => dest.Notes, src => src.Description);

        config.NewConfig<CreatePollRequest, Poll>()
            .Ignore(dest => dest.Id);
    }
}
