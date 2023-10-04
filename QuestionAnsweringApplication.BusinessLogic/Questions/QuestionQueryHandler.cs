using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestionAnsweringApplication.Domain;
using QuestionAnsweringApplication.Persistence;

namespace QuestionAnsweringApplication.BusinessLogic.Questions
{
    public class QuestionQueryHandler : IRequestHandler<GetQuestionsQuery, List<QuestionResponse>>,
        IRequestHandler<GetQuestionDetailsQuery, QuestionDetail>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public QuestionQueryHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<QuestionResponse>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            var questions = await _repository.Query<Question>(x => x.Tags.Any(a => request.Tags.Contains(a.Tag!.Name)))
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .ToListAsync();

            return _mapper.Map<List<QuestionResponse>>(questions);
        }

        public async Task<QuestionDetail> Handle(GetQuestionDetailsQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.Query<Question>(x => x.Id == request.QuestionId)
                .Include(x => x.Answers)
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag);
            var questions = await query.FirstAsync();

            return _mapper.Map<QuestionDetail>(questions);
        }
    }
}
