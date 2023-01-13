using AutoMapper;
using AutoMapper.QueryableExtensions;

public interface IQuizService
{
    Task<QuizDTO> NewQuiz();
    Task<bool> StartQuiz(int quizId);
    Task<string> AddNewPlayer(string username, string pin);
    Task<bool> SubmitAnswer(int quizId, string token, string answer, int questionId);
    Task<bool> EndQuiz(int quizId);
}

public class QuizService : IQuizService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;


    public QuizService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> AddNewPlayer(string username, string pin)
    {
        var player = new Player();

        player.Username = username;
        player.Token = Guid.NewGuid().ToString();

        var quiz = _context.Quizzes.FirstOrDefault(x => x.Pin == pin);
        
        if (quiz == null)
        {
            return null;
        }
        else {
            quiz.Players.Add(player);
        }

        await _context.SaveChangesAsync();


        // TODO send sse to front for new player

        return player.Token;
    }

    public async Task<bool> EndQuiz(int quizId)
    {
        var quiz = _context.Quizzes.Find(quizId);

        quiz.Ended = true;

        _context.Quizzes.Update(quiz);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<QuizDTO> NewQuiz()
    {
        var quiz = new Quiz();

        quiz.Started = true;
        quiz.Pin = GenerateRandomNo().ToString();

        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();

        return _mapper.Map<Quiz, QuizDTO>(quiz);
    }

    public async Task<bool> StartQuiz(int quizId)
    {
        var quiz = _context.Quizzes.Find(quizId);
        
        quiz.Started = true;

        _context.Quizzes.Update(quiz);
        
        await _context.SaveChangesAsync();

        return true;
    }

    public Task<bool> SubmitAnswer(int quizId, string token, string answer, int questionId)
    {
        var player = _context.Players.FirstOrDefault(x => x.Token == token);
        var quiz = _context.Quizzes.Find(quizId);
        var question = _context.Questions.Find(questionId);

        // TODO send to sse for frontend to update

       return Task.FromResult(true);
    }

    //Generate RandomNo
    public int GenerateRandomNo()
    {
        int _min = 100000000;
        int _max = 999999999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }
}