using quiz.Entities;

public class Player : AuditableBaseEntity
{
    public string Username { get; set; }
    public string Token { get; set; }
    public int Score { get; set; }
    public Quiz Quiz { get; set; }
}