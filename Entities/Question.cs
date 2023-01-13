using quiz.Entities;

public class Question : AuditableBaseEntity
{
    public string QuestionText { get; set; }
    public IEnumerable<Answer> Answers { get; set; }
    public virtual IEnumerable<Quiz> Quizzes { get; set; }

}