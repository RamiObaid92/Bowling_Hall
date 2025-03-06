using Bowling_Hall.src.Interfaces;
using Bowling_Hall.src.Models;
using Microsoft.Extensions.Logging;

namespace Bowling_Hall.src.Services
{
    public class MemberService : IMemberService
    {
        private readonly IRepository<Member> _memberRepo;
        private readonly ILogger<MemberService> _logger;

        public MemberService(IRepository<Member> memberRepo, ILogger<MemberService> logger)
        {
            _memberRepo = memberRepo;
            _logger = logger;
        }

        public void AddMember(Member member)
        {
            if (string.IsNullOrWhiteSpace(member.FirstName) && string.IsNullOrWhiteSpace(member.LastName))
            {
                _logger.LogWarning("Användaren matade in en tom sträng");
                throw new ArgumentException("Förnamn eller Efternamn får inte vara tomt");
            }
            _logger.LogInformation($"Bearbetar {member.FirstName} {member.LastName}");
            try
            {
                _memberRepo.Add(member);
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public Member GetMemberById(int id)
        {
            var member = _memberRepo.GetById(id);
            if (member == null)
            {
                _logger.LogWarning($"Medlem med id {id} hittades inte");
                throw new ArgumentException($"Medlem med id {id} hittades inte");
            }
            return member;
        }

        public IEnumerable<Member> GetAllMembers()
        {
            _logger.LogInformation("Hämtar medlemmar");
            return _memberRepo.GetAll();
        }

        public void UpdateMember(Member member)
        {
            if (string.IsNullOrWhiteSpace(member.FirstName) || string.IsNullOrWhiteSpace(member.LastName))
            {
                _logger.LogWarning("Förnamn eller Efternamn var tomt");
                throw new ArgumentException("Förnamn eller Efternamn får inte vara tomt");
            }
            _memberRepo.Update(member);
            _logger.LogInformation($"Medlem {member.FirstName} {member.LastName} uppdaterad i databas");
        }

        public void DeleteMember(int id)
        {
            var member = _memberRepo.GetById(id);
            if (member == null)
            {
                _logger.LogWarning($"Medlem med id {id} hittades inte");
                throw new ArgumentException($"Medlem med id {id} hittades inte");
            }
            _memberRepo.Delete(member);
            _logger.LogInformation($"Medlem {member.FirstName} {member.LastName} raderad från databas");
        }
    }
}
