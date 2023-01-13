public class QuizDTO
{
    public int Id { get; set; }
    public string Pin { get; set; }
    public bool Started { get; set; }
    public bool Ended { get; set; }
    public IEnumerable<QuestionDTO> Questions { get; set; }
    public IEnumerable<PlayerDTO> Players { get; set; }
}