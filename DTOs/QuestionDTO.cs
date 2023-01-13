public class QuestionDTO
{
    public int Id { get; set; }
    public string QuestionText { get; set; }
    public ICollection<AnswerDTO> Answers { get; set; }
}