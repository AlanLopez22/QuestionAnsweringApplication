using AutoMapper;
using MediatR;
using QuestionAnsweringApplication.Domain;
using QuestionAnsweringApplication.Persistence;

namespace QuestionAnsweringApplication.BusinessLogic.Answers
{
    public sealed class AnswerCommandHandler : IRequestHandler<CreateAnswerCommand, AnswerResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AnswerCommandHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AnswerResponse> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var question = _repository.Get<Question, Guid>(request.QuestionId) ?? throw new Exception("Question not found");
            var answer = await _repository.FirstAsync<Answer>(x => x.AnswerText == request.Answer);

            if (answer == null)
            {
                answer = new Answer
                {
                    CreatedAt = DateTime.UtcNow,
                    AnswerText = request.Answer,
                    QuestionId = request.QuestionId,
                    UserId = request.UserId
                };
                _repository.Add(answer);
            }

            answer.UpdatedAt = DateTime.UtcNow;

            if (request.Tags?.Any() == true)
            {
                foreach (var tag in request.Tags)
                {
                    var entityTag = await _repository.FirstAsync<Tag>(x => x.Name == tag);

                    if (entityTag == null)
                    {
                        answer.Tags.Add(new AnswerTag
                        {
                            Tag = new Tag
                            {
                                Name = tag
                            }
                        });
                        continue;
                    }

                    if (!answer.Tags.Any(x => x.TagId == entityTag.Id))
                    {
                        answer.Tags.Add(new AnswerTag
                        {
                            TagId = entityTag.Id
                        });
                    }
                }
            }

            await _repository.SaveChangesAsync(cancellationToken);
            return _mapper.Map<AnswerResponse>(answer);
        }
    }
}
