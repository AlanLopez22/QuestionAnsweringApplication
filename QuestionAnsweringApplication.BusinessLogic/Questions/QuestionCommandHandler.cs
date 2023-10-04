using AutoMapper;
using MediatR;
using QuestionAnsweringApplication.Domain;
using QuestionAnsweringApplication.Persistence;

namespace QuestionAnsweringApplication.BusinessLogic.Questions
{
    public sealed class QuestionCommandHandler : IRequestHandler<CreateQuestionCommand, QuestionResponse>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public QuestionCommandHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<QuestionResponse> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _repository.FirstAsync<Question>(x => x.QuestionText == request.Question);

            if (question == null)
            {
                question = new Question
                {
                    CreatedAt = DateTime.UtcNow,
                    QuestionText = request.Question,
                    UserId = request.UserId
                };
                _repository.Add(question);
            }

            question.UpdatedAt = DateTime.UtcNow;

            if (request.Tags?.Any() == true)
            {
                foreach (var tag in request.Tags)
                {
                    var entityTag = await _repository.FirstAsync<Tag>(x => x.Name == tag);

                    if (entityTag == null)
                    {
                        question.Tags.Add(new QuestionTag
                        {
                            Tag = new Tag
                            {
                                Name = tag
                            }
                        });
                        continue;
                    }

                    if (!question.Tags.Any(x => x.TagId == entityTag.Id))
                    {
                        question.Tags.Add(new QuestionTag
                        {
                            TagId = entityTag.Id
                        });
                    }
                }
            }

            await _repository.SaveChangesAsync(cancellationToken);
            return _mapper.Map<QuestionResponse>(question);
        }
    }
}
