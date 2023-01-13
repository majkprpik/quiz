using AutoMapper;

namespace quiz.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Quiz, QuizDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Pin, opt => opt.MapFrom(src => src.Pin))
                .ForMember(dest => dest.Started, opt => opt.MapFrom(src => src.Started))
                .ForMember(dest => dest.Ended, opt => opt.MapFrom(src => src.Ended))
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions.Select(
                        q => new QuestionDTO
                        {
                            Id = q.Id,
                            QuestionText = q.QuestionText,
                            Answers = q.Answers.Select(
                                a => new AnswerDTO
                                {
                                    Id = a.Id,
                                    AnswerText = a.AnswerText,
                                    IsCorrect = a.IsCorrect
                                }
                            ).ToList()
                        }
                    )
                ))
                .ForMember(dest => dest.Players, opt => opt.MapFrom(src => src.Players.Select(
                    p => new PlayerDTO
                    {
                        Id = p.Id,
                        Username = p.Username,
                        Score = p.Score
                    }
                )));

            CreateMap<Player, PlayerDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score));

            CreateMap<Question, QuestionDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.QuestionText))
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers.Select(
                    a => new AnswerDTO
                    {
                        Id = a.Id,
                        AnswerText = a.AnswerText,
                        IsCorrect = a.IsCorrect
                    }
                )));

            CreateMap<Answer, AnswerDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AnswerText, opt => opt.MapFrom(src => src.AnswerText))
                .ForMember(dest => dest.IsCorrect, opt => opt.MapFrom(src => src.IsCorrect));
        }
    }
}