public class QuestionDTO
{
    public int Id { get; set; }
    public string QuestionText { get; set; }
    public IEnumerable<AnswerDTO> Answers { get; set; }
}