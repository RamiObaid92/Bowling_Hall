using Bowling_Hall.src.Interfaces;
using Microsoft.Extensions.Logging;

namespace Bowling_Hall.src.App
{
    public class App
    {
        private readonly IMemberService _memberService;
        private readonly ILogger<App> _logger;

        public App(IMemberService memberService, ILogger<App> logger)
        {
            _memberService = memberService;
            _logger = logger;
        }
    }
}
