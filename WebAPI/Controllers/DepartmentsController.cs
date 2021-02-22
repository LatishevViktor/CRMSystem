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
    public class DepartmentsController : ControllerBase
    {
        private readonly WebAPIContext _dbContext;

        public DepartmentsController(WebAPIContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Получить список всех отделов
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IEnumerable<Departments>> GetAllDepartments()
        {
            return await _dbContext.Departments.ToListAsync();
        }
        /// <summary>
        /// Редактируем отдел
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditDepartment(Departments department)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Update(department);
                await _dbContext.SaveChangesAsync();
                return Ok(department);
            }
            return BadRequest();
        }

        /// <summary>
        /// Добавляем отдел
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Departments>> AddDepartment(Departments department)
        {
            _dbContext.Departments.Add(department);
            await _dbContext.SaveChangesAsync();

            return Ok(department);
        }

        /// <summary>
        /// Удаляем отдел
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Departments>> DeleteDepartment(int id)
        {
            var department = await _dbContext.Departments.FindAsync(id);
            if (department == null)
                return BadRequest();

            _dbContext.Departments.Remove(department);
            await _dbContext.SaveChangesAsync();

            return department;
        }
    }
}
