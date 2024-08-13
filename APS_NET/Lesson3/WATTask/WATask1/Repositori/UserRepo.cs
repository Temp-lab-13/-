using AutoMapper;
using WATask1.DadaBase;
using WATask1.DTO;

namespace WATask1.Repositori
{
    public class UserRepo : IUserRepo
    {
        private IMapper _mapper;
        private AppDbContext _dbContext;
        public UserRepo(IMapper mapper, AppDbContext appDbContext) 
        {
            this._mapper = mapper;
            this._dbContext = appDbContext;
        }
        public void AddUser(UserDto user)
        {
            using (_dbContext)
            {
                User userDb = _mapper.Map<User>(user);
                _dbContext.Users.Add(userDb);
                _dbContext.SaveChanges();
            }
                
        }

        public bool Exist(string email)
        {
            using (_dbContext)
            {
                return _dbContext.Users.Any(x => x.Active && x.Email == email);
            }
        }
    }
}
