using AutoMapper;
using Core.Models;

namespace Server.Data;
internal sealed class DTOMapping : Profile {
    public DTOMapping() {
        CreateMap<SurveyModel, SurveyDTO>().ReverseMap();
    }
}

