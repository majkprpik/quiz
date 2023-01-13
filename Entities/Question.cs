using quiz.Entities;

public class Question : AuditableBaseEntity
{
    public Question()
    {
        Answers = new List<Answer>();
        Quizzes = new List<Quiz>();
    }
    public string QuestionText { get; set; }
    public ICollection<Answer> Answers { get; set; }
    public virtual ICollection<Quiz> Quizzes { get; set; }

}