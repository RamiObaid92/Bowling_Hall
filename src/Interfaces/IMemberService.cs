using Bowling_Hall.src.Models;

namespace Bowling_Hall.src.Interfaces
{
    public interface IMemberService
    {
        void AddMember(Member member);
        Member GetMemberById(int id);
        IEnumerable<Member> GetAllMembers();
        void UpdateMember(Member member);
        void DeleteMember(int id);
    }
}
