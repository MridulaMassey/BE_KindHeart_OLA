using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;
using Online_Learning_APP.Application.DTO;

namespace Online_Learning_APP.Application.Interfaces
{
    public interface IClassGroupSubjectService
    {
        Task<ClassGroupSubjectDto> AddClassGroupSubjectAsync(ClassGroupSubjectDto classGroupSubject);
        Task<ClassGroupSubject> GetClassGroupSubjectByIdAsync(Guid id);
        Task<IEnumerable<ClassGroupSubjectDto>> GetAllClassGroupSubjectsAsync();
        Task<IEnumerable<ClassGroupSubject>> GetByClassGroupIdAsync(Guid classGroupId);
        Task<IEnumerable<ClassGroupSubject>> GetBySubjectIdAsync(Guid subjectId);
        Task UpdateClassGroupSubjectAsync(ClassGroupSubjectDto classGroupSubject);
        Task DeleteClassGroupSubjectAsync(Guid id);
    }
}
