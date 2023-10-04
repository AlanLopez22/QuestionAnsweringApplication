using AutoMapper;
using QuestionAnsweringApplication.Domain;

namespace QuestionAnsweringApplication.BusinessLogic.Answers
{
    public sealed class AnswerMappings : Profile
    {
        public AnswerMappings()
        {
            CreateMap<Answer, AnswerResponse>()
                .ForMember(x => x.Id, e => e.MapFrom(x => x.Id))
                .ForMember(x => x.Answer, e => e.MapFrom(x => x.AnswerText))
                .ForMember(x => x.Question, e => e.MapFrom(x => x.Question!.QuestionText))
                .ForMember(x => x.Tags, e => e.MapFrom(x => x.Tags.Select(s => s.Tag!.Name)));
        }
    }
}
