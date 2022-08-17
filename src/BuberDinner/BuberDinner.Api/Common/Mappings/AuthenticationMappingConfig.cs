using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using Mapster;

namespace BuberDinner.Api.Common.Mappings;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            //.Map(dest => dest.Token, src => src.Token)      // No need. Mapster auto map properties with the same name
            .Map(dest => dest, src => src.User);
    }
}