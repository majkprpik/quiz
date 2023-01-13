using quiz.Entities;

public class Answer : AuditableBaseEntity
{
    public string AnswerText { get; set; }
    public bool IsCorrect { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
}