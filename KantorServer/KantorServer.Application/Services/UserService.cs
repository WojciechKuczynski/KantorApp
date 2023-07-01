using KantorServer.Application.Responses;
using KantorServer.Application.Services.Interfaces;
using KantorServer.DAL;
using KantorServer.Model;
using KantorServer.Model.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KantorServer.Application.Services
{
    public class UserService : IUserService
    {
        public DataContext DataContext { get; }

        public UserService(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task<UserDto> AddEditUser(UserDto user)
        {
            try
            {
                var userInDb = await DataContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
                if (userInDb == null)
                {
                    var userEntity = user.ConvertToEntity();
                    await DataContext.Users.AddAsync(userEntity);
                    await DataContext.SaveChangesAsync();
                    return new UserDto(userEntity);
                }
                else
                {
                    userInDb.Login = user.Login;
                    userInDb.Password = user.Password;
                    userInDb.Name = user.Name;
                    userInDb.Valid = user.Valid;
                }
                await DataContext.SaveChangesAsync();
                return new UserDto(userInDb);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<LoginResponse> UserLogin(UserDto user, KantorDto kantor)
        {
            var userInDb = await CheckUserLogin(user);
            var kantorInDb = await DataContext.Kantors.FirstOrDefaultAsync(x => x.IdentificationKey == kantor.IdentificationKey);

            if (userInDb == null || kantorInDb == null)
            {
                return new LoginResponse(false, "", "Podano niepoprawny kantor lub użytkownika");
            }

            var synchronizationKey = Guid.NewGuid().ToString();
            var userSession = new UserSession(kantorInDb, userInDb, DateTime.Now, synchronizationKey);

            await DataContext.UserSessions.AddAsync(userSession);
            await DataContext.SaveChangesAsync();
            return new LoginResponse(true, "Poprawnie zalogowano!", "") { SynchronizationKey = synchronizationKey, UserId = userSession.User.Id, Name = userSession.User.Name, Permission = userSession.User.Permission, Kantor = new KantorDto(kantorInDb) };
        }

        private async Task<User?> CheckUserLogin(UserDto user)
        {
            if (user == null)
            {
                return null;
            }
            var password = CreateMD5(user.Password);
            return await DataContext.Users.FirstOrDefaultAsync(x => x.Login == user.Login && x.Password == password && x.Valid);
        }

        private string CreateMD5(string input)
        {

            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes); 
            }
        }

        public async Task<List<UserDto>> GetUsers()
        {
            try
            {
                var users = await DataContext.Users.ToListAsync();
                return UserDto.Map(users);
            }
            catch (Exception ex) { return null; }
        }
    }
}
