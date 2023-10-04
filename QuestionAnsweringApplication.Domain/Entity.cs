namespace QuestionAnsweringApplication.Domain
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}
