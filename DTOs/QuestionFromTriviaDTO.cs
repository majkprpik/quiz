public class QuestionFromTriviaDTO
{
    public string question { get; set; }
    public string correctAnswer { get; set; }
    public IEnumerable<string> incorrectAnswers { get; set; }    
}