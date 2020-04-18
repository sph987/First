using System.Linq;
using System.Net;
using System.Threading.Tasks;
using application.Errors;
using application.Interfaces;
using Microsoft.EntityFrameworkCore;
using persistence;

namespace application.Profiles
{
    public class ProfileReader : IProfileReader
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _useraccessor;
        public ProfileReader(DataContext context, IUserAccessor useraccessor)
        {
            _useraccessor = useraccessor;
            _context = context;
        }

        public async Task<Profile> ReadProfile(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == username);

            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, new { User = "not found" });

            var currentUser = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _useraccessor.GetCurrentUsername());

            var profile = new Profile
            {
                DisplayName = user.DisplayName,
                Username = user.UserName,
                Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                Photos = user.Photos,
                Bio = user.Bio,
                FollowersCount = user.Followers.Count(),
                FollowingCount = user.Followings.Count()
            };

            if (currentUser.Followings.Any(x => x.TargetId == user.Id))
                profile.IsFollowed = true;

            return profile;
        }
    }
}