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
        // Instansierar Observer pattern
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
                Console.WriteLine("\nVälkommen till Bowling hallen! Välj ett alternativ");
                Console.WriteLine("\n1. Registrera dig");
                Console.WriteLine("2. Starta en match");
                Console.WriteLine("3. Avsluta");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        RegisterMember();
                        _logger.LogInformation("Loopar tillbaka till huvudmenyn");
                        break;
                    case "2":
                        StartMatch();
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
            _logger.LogInformation("Registrering valdes");

            Console.Write("Ange förnamn: ");
            string firstName = Console.ReadLine();

            Console.Write("Ange efternamn: ");
            string lastName = Console.ReadLine();

            _logger.LogInformation($"input: {firstName} {lastName}");

            var newMember = new Member
            {
                FirstName = firstName,
                LastName = lastName
            };

            try
            {
                _memberService.AddMember(newMember);
                _logger.LogInformation("Registrerar medlem");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);
                return;
            }
        }

        private void StartMatch()
        {
            Console.Clear();
            _logger.LogInformation("Starta en match valdes");

            Console.WriteLine("\nDu valde att starta en match! Ange två spelare.");

            Console.Write("\nSpelare 1: ");
            string playerOne = Console.ReadLine();
            _logger.LogInformation($"Input spelare 1: {playerOne}");

            Console.Write("Spelare 2: ");
            string playerTwo = Console.ReadLine();
            _logger.LogInformation($"Input spelare 2: {playerTwo}");

            Console.WriteLine($"{playerOne} vs {playerTwo}, tryck på enter när du är redo!");

            Console.ReadKey();

            // Lägger till ScoreLogger som lyssnare
            _gameEventSystem.AddListener(_scoreLogger);

            var match = new MatchLogic(_gameEventSystem, playerOne, playerTwo);

            match.PlayMatch();

            _gameEventSystem.RemoveListener(_scoreLogger);

        }
    }
}
