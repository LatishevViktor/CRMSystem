using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly WebAPIContext _dbContext;

        public UsersController(WebAPIContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Список всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IQueryable<Users> GetAllUser()
        {
            return _dbContext.Users.Include(b => b.Dep);
        }

        /// <summary>
        /// Получаем конкретного пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            
            if (user == null)
                return BadRequest();

            return user;
        }

        /// <summary>
        /// Изменяем пользователя по Id
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Departments>> EditUser(int id, Users user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(user).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Users.Any(dep => dep.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Добавляем пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Users>> AddUser(Users user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return Ok(user);
        }

        /// <summary>
        /// Удаляем пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
                return BadRequest();

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}
