using quiz.Entities;

public class Quiz : AuditableBaseEntity
{
    public Quiz ()
    {
        Questions = new List<Question>();
        Players = new List<Player>();
    }
    public string Pin { get; set; }
    public virtual ICollection<Question> Questions { get; set; }
    public virtual ICollection<Player> Players { get; set; }
    public bool Started { get; set; }
    public bool Ended { get; set; }
}   