using quiz.Entities;

public class Quiz : AuditableBaseEntity
{
    public string Pin { get; set; }
    public virtual IEnumerable<Question> Questions { get; set; }
    public virtual IEnumerable<Player> Players { get; set; }
    public bool Started { get; set; }
    public bool Ended { get; set; }
}   