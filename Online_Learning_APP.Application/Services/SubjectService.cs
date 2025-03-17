using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_APP.Application.Services
{
    //public class SubjectService : ISubjectService
    //{
    //    private readonly ISubjectRepository _subjectRepository;

    //    public SubjectService(ISubjectRepository subjectRepository)
    //    {
    //        _subjectRepository = subjectRepository;
    //    }

    //    public async Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync()
    //    {
    //        var subjects = await _subjectRepository.GetAllAsync();
    //        return subjects.Select(s => new SubjectDto
    //        {
    //            SubjectId = s.SubjectId,
    //            SubjectName = s.SubjectName
    //        }).ToList();
    //    }

    //    public async Task<SubjectDto> GetSubjectByIdAsync(Guid subjectId)
    //    {
    //        var subject = await _subjectRepository.GetByIdAsync(subjectId);
    //        if (subject == null)
    //            return null;

    //        return new SubjectDto
    //        {
    //            SubjectId = subject.SubjectId,
    //            SubjectName = subject.SubjectName
    //        };
    //    }

    //    public async Task<Guid> CreateSubjectAsync(SubjectDto subjectDto)
    //    {
    //        var subject = new Subject
    //        {
    //            SubjectId = Guid.NewGuid(),
    //            SubjectName = subjectDto.SubjectName
    //        };

    //        await _subjectRepository.AddAsync(subject);
    //        return subject.SubjectId;
    //    }

    //    public async Task UpdateSubjectAsync(SubjectDto subjectDto)
    //    {
    //        var subject = await _subjectRepository.GetByIdAsync(subjectDto.SubjectId);
    //        if (subject == null)
    //            throw new Exception("Subject not found");

    //        subject.SubjectName = subjectDto.SubjectName;
    //        await _subjectRepository.UpdateAsync(subject);
    //    }

    //    public async Task DeleteSubjectAsync(Guid subjectId)
    //    {
    //        await _subjectRepository.DeleteAsync(subjectId);
    //    }
    //}
}
