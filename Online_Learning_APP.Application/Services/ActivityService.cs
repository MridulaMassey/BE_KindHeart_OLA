using System;
using System.Threading.Tasks;
using AutoMapper; // Assuming you're using AutoMapper
//using Online_Learning_App.Application.DTOs;
//using Online_Learning_App.Application.Interfaces;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;
using Online_Learning_App.Infrastructure.Migrations;
using Online_Learning_App.Infrastructure.Repository;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_APP.Application.Services;
//using Online_Learning_App.Infrastructure.Persistence.Interfaces; // Assuming a repository

namespace Online_Learning_App.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private IFileUploadService _uploadService;
        private readonly IClassGroupSubjectRepository _classGroupSubjectRepository;
        private readonly IClassGroupSubjectActivityRepository _classGroupSubjectActivityRepository;

        private readonly IGradeService _gradeService;

        public ActivityService(IActivityRepository activityRepository, IMapper mapper, IFileUploadService uploadService, IClassGroupSubjectRepository classGroupSubjectRepository, IClassGroupSubjectActivityRepository classGroupSubjectActivityRepository, IGradeService gradeService)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
            _uploadService = uploadService;
            _classGroupSubjectRepository = classGroupSubjectRepository;
            _classGroupSubjectActivityRepository=classGroupSubjectActivityRepository;
            _gradeService = gradeService;
        }

        public async Task<ActivityDto> CreateActivityAsync(CreateActivityDto createActivityDto)
        {
            byte[] filebytetest = Convert.FromBase64String(createActivityDto.PdfFileBase64);
            var response = await _uploadService.UploadFileAsync(filebytetest, createActivityDto.FileName);
            //    var subjectidrespnse= await  _classGroupSubjectRepository.GetByClassGroupIdAsync(createActivityDto.ClassGroupId.Value);
            // var subjectID= subjectidrespnse.FirstOrDefault().SubjectId;
            //   var classGroupid= subjectidrespnse.FirstOrDefault()?.ClassGroupId;

            // Fetch existing activities for the subject and class
            var existingActivities = await _activityRepository.GetBySubjectAndClassAsync(createActivityDto.SubjectId, createActivityDto.ClassGroupId.Value);

            // Calculate total weightage including the new activity
            double totalWeightage = existingActivities.Sum(a => a.WeightagePercent) + createActivityDto.WeightagePercent;

            if (totalWeightage > 100)
            {
                throw new InvalidOperationException("Total weightage percent cannot exceed 100 percent.");
            }
            var classgroupsubjectid = Guid.NewGuid();
            var classGroupSubject = new ClassGroupSubject
            {
                ClassGroupSubjectId = classgroupsubjectid,
                ClassGroupId = createActivityDto.ClassGroupId.Value,
                SubjectId = createActivityDto.SubjectId
            };

            await _classGroupSubjectRepository.AddAsync(classGroupSubject);

            var activity = _mapper.Map<Activity>(createActivityDto);
            activity.Feedback = createActivityDto.Feedback;
            activity.HasFeedback = createActivityDto?.HasFeedback.Value;
            activity.ActivityId = Guid.NewGuid(); // Generate a new ID
            activity.Id = activity.ActivityId; // If Id is needed.
            activity.SubjectId = createActivityDto.SubjectId;
            activity.ClassGroupId = createActivityDto.ClassGroupId;
            activity.ClassLevel = "Four";
            activity.PdfUrl = response.ToString();
            activity.ClassGroupSubjectId = classgroupsubjectid;
            var classgrpactivity = Guid.NewGuid();
            //await _classGroupSubjectRepository.AddAsync(classGroupSubject);
            activity.TeacherId = Guid.Parse("F7400196-CDEB-49ED-11BA-08DD64CD7D35");
            var classGroupSubjectActivity = new ClassGroupSubjectActivity
            {
                ClassGroupSubjectActivityId= classgrpactivity,
                ClassGroupSubjectId = classgroupsubjectid,
             ActivityId= activity.ActivityId,
            };
          // await _classGroupSubjectActivityRepository.CreateAsync(classGroupSubjectActivity);
              await _activityRepository.AddAsync(activity);
           await _classGroupSubjectActivityRepository.CreateAsync(classGroupSubjectActivity);
            return _mapper.Map<ActivityDto>(activity);
        }


        public async Task<ActivityDto> UpdateTeacherActivityAsync(UpdateTeacherSubmissionDto createActivityDto)
        {
          
            var classgroupsubjectid = Guid.NewGuid();
            //var classGroupSubject = new ClassGroupSubject
            //{
            //    ClassGroupSubjectId = classgroupsubjectid,
            //    ClassGroupId = createActivityDto.ClassGroupId.Value,
            //    SubjectId = createActivityDto.SubjectId
            //};

            //await _classGroupSubjectRepository.AddAsync(classGroupSubject);

            //var activity = _mapper.Map<Activity>(createActivityDto);
            var activity = await _activityRepository.GetByIdAsync(createActivityDto.ActivityId);
            var studentGuid = new Guid("845DB027-2D1D-46D5-5634-08DD65188216");
            activity.Feedback = createActivityDto.Feedback;
            activity.HasFeedback = true;
            var activityGrade = new ActivityGradeDto
            {
                //ActivityGradeId = Guid.NewGuid(),
                StudentId = studentGuid,
                ActivityId = createActivityDto.ActivityId,
                Score = createActivityDto.Grade.Value,
            };
           await _gradeService.AssignGradeToActivityTeacher(activityGrade);


            await _activityRepository.UpdateAsync(activity);
         //   await _classGroupSubjectActivityRepository.CreateAsync(classGroupSubjectActivity);
            return _mapper.Map<ActivityDto>(activity);
        }
        public async Task<ActivityDto> GetActivityByIdAsync(Guid activityId)
        {
            var activity = await _activityRepository.GetByIdAsync(activityId);
            return activity == null ? null : _mapper.Map<ActivityDto>(activity);
        }

        // ✅ Read All Activities
        public async Task<IEnumerable<ActivityDto>> GetAllActivitiesAsync()
        {
            var activities = await _activityRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        // ✅ Update Activity by teacher
        public async Task<ActivityDto> UpdateActivityAsync(Guid activityId, UpdateActivityDto updateActivityDto)
        {
            var activity = await _activityRepository.GetByIdAsync(activityId);
            if (activity == null)
            {
                return null;
            }

            //   this for student
            // Update properties
            //activity.ActivityName = updateActivityDto.ActivityName ?? activity.ActivityName;
            //activity.Description = updateActivityDto.Description ?? activity.Description;


            // byte array
            byte[] filebytetest = Convert.FromBase64String(updateActivityDto.FileBase64);
            var response = await _uploadService.UploadFileAsync(filebytetest, updateActivityDto.FileName);
            activity.StudentPdfUrl = response.ToString();
            //     activity.StudentPdfUrl = updateActivityDto.FileBase64 ?? activity.Description;


            await _activityRepository.UpdateAsync(activity);
            return _mapper.Map<ActivityDto>(activity);
        }

        // ✅ Delete Activity
        public async Task<bool> DeleteActivityAsync(Guid activityId)
        {
            var activity = await _activityRepository.GetByIdAsync(activityId);
            if (activity == null)
            {
                return false;
            }

            await _activityRepository.DeleteAsync(activity.Id);
            return true;
        }

    }
}