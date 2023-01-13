using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BCrypt.Net;
using Lib.AspNetCore.ServerSentEvents;
using Microsoft.IdentityModel.Tokens;
using WebApi.HostedServices;

public interface IQuizService
{
    Task<QuizDTO> NewQuiz();
    Task<bool> StartQuiz(int quizId);
    Task<string> AddNewPlayer(string username, string pin);
    Task<bool> SubmitAnswer(string token, string answer);
    Task<bool> NextQuestion(int quizId);
    Task<bool> EndQuiz(int quizId);
}

public class QuizService : IQuizService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IServerSentEventsService _client;
    protected readonly IConfiguration Configuration;

    public QuizService(DataContext context, IMapper mapper, IServerSentEventsService client, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _client = client;
        Configuration = configuration;
    }

    public async Task<string> AddNewPlayer(string username, string pin)
    {
        var player = new Player();

        player.Username = username;

        var quiz = _context.Quizzes.FirstOrDefault(x => x.Pin == pin);

        var claimsData = new[]
            {
                new Claim("id", player.Id.ToString()),
                new Claim("userName", player.Username),
                new Claim("quizId", quiz.Id.ToString()),
            };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("test"));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(
            claims: claimsData,
            expires: DateTime.Now.AddMinutes(60)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        player.Token = token;


        if (quiz == null)
        {
            return null;
        }
        else
        {
            quiz.Players.Add(player);
        }

        await _context.SaveChangesAsync();

        await _client.SendEventAsync("new-player:" + quiz.Id, ":username:" + player.Username);

        return player.Token;
    }

    public async Task<bool> EndQuiz(int quizId)
    {
        var quiz = _context.Quizzes.Find(quizId);

        quiz.Ended = true;

        _context.Quizzes.Update(quiz);
        await _context.SaveChangesAsync();

        await _client.SendEventAsync("end-quiz:" + quiz.Id);

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

        await _client.SendEventAsync("start-quiz:" + quiz.Id);

        return true;
    }

    public async Task<bool> NextQuestion(int quizId)
    {
        await _client.SendEventAsync("next-question:" + quizId);

        return true;
    }

    public async Task<bool> SubmitAnswer(string token, string answer)
    {
        var player = _context.Players.FirstOrDefault(x => x.Token == token);

        await _client.SendEventAsync("answer:" + "quizId:" + player.Quiz.Id, ":username:" + player.Username + ":answer:" + answer);

        return true;
    }

    public int GenerateRandomNo()
    {
        int _min = 100000000;
        int _max = 999999999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }
}