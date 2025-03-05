using Bowling_Hall.src.Interfaces;
using Bowling_Hall.src.Models;
using Bowling_Hall.src.Data;

namespace Bowling_Hall.src.Repositories
{
    public class MemberRepo : IRepository<Member>
    {
        AppDbContext _context;

        public MemberRepo(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Member member)
        {
            _context.Members.Add(member);
            _context.SaveChanges();
        }
        public Member GetById(int id)
        {
            return _context.Members.Find(id);
        }

        public IEnumerable<Member> GetAll()
        {
            return _context.Members.ToList();
        }

        public void Update(Member member)
        {
            _context.Members.Update(member);
            _context.SaveChanges();
        }
        public void Delete(Member member)
        {
            var memberToDelete = _context.Members.Find(member.Id);
            if (memberToDelete != null)
            {
                _context.Members.Remove(memberToDelete);
                _context.SaveChanges();
            }
        }
    }
}
