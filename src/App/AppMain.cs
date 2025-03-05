using Bowling_Hall.src.Interfaces;
using Bowling_Hall.src.Models;
using Microsoft.Extensions.Logging;
using System;

namespace Bowling_Hall.src.App
{
    public class AppMain
    {
        private readonly IMemberService _memberService;
        private readonly ILogger<AppMain> _logger;

        public AppMain(IMemberService memberService, ILogger<AppMain> logger)
        {
            _memberService = memberService;
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogInformation("App is running");
        }
    }
}
