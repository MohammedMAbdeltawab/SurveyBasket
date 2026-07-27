using SurveyBasket.Api.Contracts.Polls;
using SurveyBasket.Api.Entities;

namespace SurveyBasket.Api.Mapping;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PollRequest, Poll>()
            .Ignore(dest => dest.Id);
    }
}
