using Bowling_Hall.src.Interfaces;
using Bowling_Hall.src.Models;
using Bowling_Hall.src.Data;
using Microsoft.Extensions.Logging;

namespace Bowling_Hall.src.Repositories
{
    public class MemberRepo : IRepository<Member>
    {
        AppDbContext _context;

        private readonly ILogger<MemberRepo> _logger;

        public MemberRepo(AppDbContext context, ILogger<MemberRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add(Member member)
        {
            if (_context.Members.Any(m => m.FirstName.ToLower() == member.FirstName.ToLower() && m.LastName.ToLower() == member.LastName.ToLower()))
            {
                throw new ArgumentException("Medlemmen finns redan i databasen");
            }
            _context.Members.Add(member);
            _context.SaveChanges();
        }
        public Member GetById(int id)
        {
            _logger.LogInformation($"Hämtar medlem med {id}");
            return _context.Members.Find(id);
        }

        public IEnumerable<Member> GetAll()
        {
            _logger.LogInformation("Hämtar alla medlemmar");
            return _context.Members.ToList();
        }

        public void Update(Member member)
        {
            _logger.LogInformation($"Uppdaterar medlem med {member.Id} - {member.FirstName} {member.LastName}");
            _context.Members.Update(member);
            _context.SaveChanges();
        }
        public void Delete(Member member)
        {
            var memberToDelete = _context.Members.Find(member.Id);
            if (memberToDelete != null)
            {
                _logger.LogInformation($"Tar bort medlem med {member.Id} - {member.FirstName} {member.LastName}");
                _context.Members.Remove(memberToDelete);
                _context.SaveChanges();
            }
        }
    }
}
