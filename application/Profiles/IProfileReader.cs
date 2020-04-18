using System.Threading.Tasks;

namespace application.Profiles
{
    public interface IProfileReader
    {
         Task<Profile> ReadProfile(string username);
    }
}