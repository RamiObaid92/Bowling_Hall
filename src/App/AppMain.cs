using Bowling_Hall.src.Events;
using Bowling_Hall.src.Interfaces;
using Bowling_Hall.src.Models;
using Bowling_Hall.src.Services;
using Microsoft.Extensions.Logging;

namespace Bowling_Hall.src.App
{
    public class AppMain
    {
        private readonly IMemberService _memberService;
        private readonly ILogger<AppMain> _logger;
        private readonly GameEventSystem _gameEventSystem;
        private readonly ScoreLogger _scoreLogger;

        public AppMain(IMemberService memberService, ILogger<AppMain> logger, GameEventSystem gameEventSystem, ScoreLogger scoreLogger)
        {
            _memberService = memberService;
            _logger = logger;
            _gameEventSystem = gameEventSystem;
            _scoreLogger = scoreLogger;
        }

        public void Run()
        {
            _logger.LogInformation("Appen är igång");

            while (true)
            {
                _logger.LogInformation("Loopar huvudmenyn");

                Console.WriteLine("\nVälkommen till Bowling hallen! Välj ett alternativ");
                Console.WriteLine("\n1. Registrera dig");
                Console.WriteLine("2. Starta en match");
                Console.WriteLine("3. Avsluta");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        RegisterMember();
                        _logger.LogInformation("Registrering valdes");
                        break;
                    case "2":
                        StartMatch();
                        _logger.LogInformation("Starta en match valdes");
                        break;
                    case "3":
                        _logger.LogInformation("Appen avslutades");
                        return;
                    default:
                        _logger.LogInformation("Felaktig input");
                        break;
                }
            }
        }

        private void RegisterMember()
        {
            Console.Clear();

            Console.Write("Ange förnamn: ");
            string firstName = Console.ReadLine();

            Console.Write("Ange efternamn: ");
            string lastName = Console.ReadLine();

            var newMember = new Member
            {
                FirstName = firstName,
                LastName = lastName
            };

            try
            {
                _memberService.AddMember(newMember);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private void StartMatch()
        {
            Console.WriteLine("\nDu valde att starta en match! Ange två spelare.");

            Console.Write("\nSpelare 1: ");
            string playerOne = Console.ReadLine();
            _logger.LogInformation($"input: {playerOne}");

            Console.Write("Spelare 2: ");
            string playerTwo = Console.ReadLine();
            _logger.LogInformation($"input: {playerTwo}");

            _gameEventSystem.AddListener(_scoreLogger);

            var match = new MatchLogic(_gameEventSystem, playerOne, playerTwo);

            match.PlayMatch();

            _gameEventSystem.RemoveListener(_scoreLogger);

        }
    }
}
