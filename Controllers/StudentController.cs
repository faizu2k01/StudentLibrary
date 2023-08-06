using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentLibrary.DataInfrastructure;

namespace StudentLibrary.Controllers
{
    public class StudentController : Controller
    {
        private readonly DataContext _dataContext;
        public StudentController(DataContext dataContext)
        {
            _dataContext = dataContext;
            
        }
        public IActionResult Index()
        {
            var studentsList = _dataContext.Students.FromSqlRaw("GetAllRecord").ToList();
            return View(studentsList);
        }


        public IActionResult GetDetail(int id)
        {
            var student = _dataContext.Students.FromSqlRaw($"GetOneRecord {id}").AsEnumerable().FirstOrDefault();
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var student =  await _dataContext.Database.ExecuteSqlRawAsync($"Exce deleteRecord {id}");
            if(student == 1)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("GetDetail");
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id,string mobile,string email)
        {

            var param = new SqlParameter[]
            {
                new SqlParameter
                {
                    Value = id,
                    SqlDbType = System.Data.SqlDbType.Int,
                    ParameterName="@id"
                },
                new SqlParameter
                {
                    Value = email,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    ParameterName="@email"
                },
                new SqlParameter
                {
                    Value = mobile,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    ParameterName="@mobile"
                },
            };
            var update = await _dataContext.Database.ExecuteSqlRawAsync($"Exec updateRecord @id, @email, @mobile", param);

            if(update == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("GetDetail");
            }
        }
    }
}
