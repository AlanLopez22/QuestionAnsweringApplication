using AutoMapper;
using QuestionAnsweringApplication.Domain;

namespace QuestionAnsweringApplication.BusinessLogic.Questions
{
    public sealed class QuestionMappings : Profile
    {
        public QuestionMappings()
        {
            CreateMap<Question, QuestionResponse>()
                .ForMember(x => x.Id, e => e.MapFrom(x => x.Id))
                .ForMember(x => x.Question, e => e.MapFrom(x => x.QuestionText))
                .ForMember(x => x.Tags, e => e.MapFrom(x => x.Tags.Select(s => s.Tag!.Name)));

            CreateMap<Question, QuestionDetail>()
                .ForMember(x => x.Id, e => e.MapFrom(x => x.Id))
                .ForMember(x => x.Question, e => e.MapFrom(x => x.QuestionText))
                .ForMember(x => x.Answers, e => e.MapFrom(x => x.Answers.Select(s => s.AnswerText)))
                .ForMember(x => x.Tags, e => e.MapFrom(x => x.Tags.Select(s => s.Tag!.Name)));
        }
    }
}
