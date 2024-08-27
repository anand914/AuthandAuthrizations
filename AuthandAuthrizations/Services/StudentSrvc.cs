using AuthandAuthrizations.Data;
using AuthandAuthrizations.Models.DTO;
using AuthandAuthrizations.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AuthandAuthrizations.Services
{
    public class StudentSrvc : IStuddent
    {
        private readonly EmployeeDbContext _context;

        public StudentSrvc(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> GetStudentbyId(int id)
        {
            try
            {
                var result = await _context.Student
                    .Where(x => x.StudentId == id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    return new ApiResponse
                    {
                        Status = true,
                        StatusCode = 200,
                        Message = result,
                        Errors = null
                    };
                }
                return new ApiResponse
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Student not found",
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ApiResponse> GetStudents(int pageNumber, int pageSize, string filter = "")
        {
            try
            {
                var query = _context.Student.AsQueryable();
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    query = query.Where(x => x.FirstName.Contains(filter));
                }
                //apply paginations//
                var totalRecords = await query.CountAsync();
                var students = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                return new ApiResponse
                {
                    Status = true,
                    StatusCode = 200,
                    Message = new
                    {
                        TotalRecords = totalRecords,
                        Students = students  
                    },
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
