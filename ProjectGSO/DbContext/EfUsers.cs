using ProjectGSO.Models.DTO;

namespace ProjectGSO.DbContext
{
    public class EfUsers
    {
        public List<UsersDTO> GetUsers()
        {
            using (BlogDbContext context = new BlogDbContext())
            {
                var result = from u in context.Users
                             join r in context.Roles on u.RoleId equals r.Id
                             select new UsersDTO
                             {
                                 Id = u.Id,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Role = r.RoleName,
                                 Email = u.Email,
                                 Password = u.Password,
                                 Username = u.Username,
                                 AvatarUrl = u.AvatarUrl
                             };
                return result.ToList();
            }
        }

        public List<UsersDTO> ByIdGetUsers(int id)
        {
            using (BlogDbContext context = new BlogDbContext())
            {
                var result = from u in context.Users
                             join r in context.Roles on u.RoleId equals r.Id
                             where u.Id == id
                             select new UsersDTO
                             {
                                 Id = u.Id,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Role = r.RoleName,
                                 Email = u.Email,
                                 Password = u.Password,
                                 Username = u.Username,
                                 AvatarUrl = u.AvatarUrl
                             };
                return result.ToList();
            }
        }
    }
}
