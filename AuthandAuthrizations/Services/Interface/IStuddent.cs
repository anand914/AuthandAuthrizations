using AuthandAuthrizations.Models.DTO;

namespace AuthandAuthrizations.Services.Interface
{
    public interface IStuddent
    {
        Task<ApiResponse> GetStudents(int pageNumber, int pageSize, string filter = "");
        Task<ApiResponse> GetStudentbyId(int id);

    }
}
