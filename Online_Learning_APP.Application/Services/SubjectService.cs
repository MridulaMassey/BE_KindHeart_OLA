using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;
using Online_Learning_App.Infrastructure.Repository;
using System.Diagnostics;
using AutoMapper;

namespace Online_Learning_APP.Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        //public async Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync()
        //{
        //    var subjects = await _subjectRepository.GetAllAsync();
        //    return subjects.Select(s => new SubjectDto
        //    {
        //        SubjectId = s.SubjectId,
        //        SubjectName = s.SubjectName
        //    }).ToList();
        //}

        //public async Task<SubjectDto> GetSubjectByIdAsync(Guid subjectId)
        //{
        //    var subject = await _subjectRepository.GetByIdAsync(subjectId);
        //    if (subject == null)
        //        return null;

        //    return new SubjectDto
        //    {
        //        SubjectId = subject.SubjectId,
        //        SubjectName = subject.SubjectName
        //    };
        //}

        //var activity = _mapper.Map<Activity>(createActivityDto);
        //activity.ActivityId = Guid.NewGuid(); // Generate a new ID
        //    activity.Id = activity.ActivityId; //If Id is needed.
        //    activity.ActivityName=activity.ActivityName;
        //  //  activity.Validate(); // Domain validation

        //    await _activityRepository.AddAsync(activity);
        //    return _mapper.Map<ActivityDto>(activity);

        public async Task<Guid> CreateSubjectAsync(SubjectDto subjectDto)
        {
            var subject = _mapper.Map<Subject>(subjectDto);
            subject.SubjectId = Guid.NewGuid(); // Generate a new ID

            //var subject = new Subject
            //{
            //    SubjectId = Guid.NewGuid(),
            //    SubjectName = subjectDto.SubjectName
            //};

            await _subjectRepository.AddAsync(subject);
           // return _mapper.Map<SubjectDto>(subject);
            return subject.SubjectId;
        }

        //public async Task UpdateSubjectAsync(SubjectDto subjectDto)
        //{
        //    var subject = await _subjectRepository.GetByIdAsync(subjectDto.SubjectId);
        //    if (subject == null)
        //        throw new Exception("Subject not found");

        //    subject.SubjectName = subjectDto.SubjectName;
        //    await _subjectRepository.UpdateAsync(subject);
        //}

        //public async Task DeleteSubjectAsync(Guid subjectId)
        //{
        //    await _subjectRepository.DeleteAsync(subjectId);
        //}
    }
}
