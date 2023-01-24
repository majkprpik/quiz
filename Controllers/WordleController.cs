using Microsoft.AspNetCore.Mvc;
using quiz.Helpers;

namespace quiz.Controllers;

public class WordleController : ApiBase
{
    private readonly IWordleService _wordleService;

    public WordleController(IWordleService wordleService)
    {
        _wordleService = wordleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var words = await _wordleService.GetWordles();

        return Ok(words);
    }

    [HttpGet]
    public async Task<IActionResult> GetRandom()
    {
        var word = await _wordleService.GetRandomWordle();

        return Ok(word);
    }
}
