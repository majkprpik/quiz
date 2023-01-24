using Microsoft.AspNetCore.Mvc;
using quiz.Helpers;

namespace quiz.Controllers;

public class QuizController : ApiBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    // [HttpGet]
    // public async Task<IActionResult> NewQuiz()
    // {
    //     var quiz = await _quizService.NewQuiz();

    //     return Ok(quiz);       
    // }

    // [HttpGet]
    // public async Task<IActionResult> StartQuiz(int quizId)
    // {
    //     var success = await _quizService.StartQuiz(quizId);

    //     return Ok(success);
    // }

    // [HttpGet]
    // public async Task<IActionResult> EndQuiz(int quizId)
    // {
    //     var success = await _quizService.EndQuiz(quizId);

    //     return Ok(success);
    // }

    // [HttpPost]
    // public async Task<IActionResult> NextQuestion(int quizId)
    // {
    //     var success = await _quizService.NextQuestion(quizId);

    //     return Ok(success);
    // }

    //     [HttpPost]
    // public async Task<IActionResult> SubmitAnswer(string token, string answer)
    // {
    //     var success = await _quizService.SubmitAnswer(token, answer);

    //     return Ok(success);
    // }

    // [HttpPost]
    // public async Task<IActionResult> AddNewPlayer(string username, string pin)
    // {
    //     var token = await _quizService.AddNewPlayer(username, pin);

    //     return Ok(token);
    // }
}
