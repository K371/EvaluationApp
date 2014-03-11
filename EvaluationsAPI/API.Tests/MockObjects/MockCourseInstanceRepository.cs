﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIServices.Repositories;
using APIServices.Models;

namespace API.Tests.MockObjects
{
    /// <summary>
    /// Mock repository for testing of the public functions in CoursesServiceProvider
    /// </summary>
    class MockCourseInstanceRepository : ICoursesRepository
    {
        List<CourseInstance> _coursesList = new List<CourseInstance>();
        List<SchoolConstant> _schoolConstants = new List<SchoolConstant>();
        List<StudentRegistration> _coursesInstancesRegistrationStudentList = new List<StudentRegistration>();
        List<TeachersRegistration> _coursesInstancesRegistrationTeacherList = new List<TeachersRegistration>();
        List<Person> _courseInstanceStudentList = new List<Person>();
        List<Group> _courseInstanceGroupList = new List<Group>();
        List<Booking> _coursesInstancesScheduleList = new List<Booking>();
        List<CourseInstanceMoreInformation> _courseInstancesMoreInformation = new List<CourseInstanceMoreInformation>();


        public MockCourseInstanceRepository()
        {
            _coursesList.Add(new CourseInstance { ID = 1, Name = "Vefforritun II", CourseID = "T-427-WEPO", DateBegin = DateTime.Now, Semester = "20131"});
            _coursesList.Add(new CourseInstance { ID = 1, Name = "Vefforritun I", CourseID = "T-213-VEFF", DateBegin = DateTime.Now, Semester = "20131" });
            _coursesList.Add(new CourseInstance { ID = 1, Name = "Forritun", CourseID = "T-111-PROG", DateBegin = DateTime.Now, Semester = "20131" });
            _coursesList.Add(new CourseInstance { ID = 1, Name = "Inngangur að tölvunarfræði", CourseID = "T-109-INTO", DateBegin = DateTime.Now, Semester = "20121" });
            _coursesList.Add(new CourseInstance { ID = 1, Name = "Gangaskipan", CourseID = "T-201-GSKI", DateBegin = DateTime.Now, Semester = "20121" });
            _coursesList.Add(new CourseInstance { ID = 1, Name = "Reiknirit", CourseID = "T-301-REIR", DateBegin = DateTime.Now, Semester = "20121" });

            _schoolConstants.Add(new SchoolConstant { ID = 1, Key = "sSemesterNow", Value = "20131"});
        }

        public MockCourseInstanceRepository(List<CourseInstance> courses)
        {            
            _coursesList = courses;
            _schoolConstants.Add(new SchoolConstant { ID = 1, Key = "sSemesterNow", Value = "20131" });
        }

        public IQueryable<APIServices.Models.CourseInstance> courseInstances()
        {
            return _coursesList.AsQueryable();
        }

        public IQueryable<APIServices.Models.SchoolConstant> schoolConstants()
        {
            return _schoolConstants.AsQueryable();
        }

        public IQueryable<APIServices.Models.Booking> getScheduleForCourseSemester()
        {
            return _coursesInstancesScheduleList.AsQueryable();
        }

        public IQueryable<APIServices.Models.StudentRegistration> courseInstanceStudentRegistrations()
        {
            return _coursesInstancesRegistrationStudentList.AsQueryable();
        }

        public IQueryable<APIServices.Models.TeachersRegistration> teachersRegistrations()
        {
            return _coursesInstancesRegistrationTeacherList.AsQueryable();
        }
        
        public IQueryable<APIServices.Models.Person> students()
        {
            return _courseInstanceStudentList.AsQueryable();
        }

        public IQueryable<APIServices.Models.Group> groups() 
        {
            return _courseInstanceGroupList.AsQueryable();
        }

        public void addGroup(Group item)
        {
            //var test = Group { CourseInstId = 1 GroupName = "Test Group", MaxStudentNumber = 30 };
        }

        public void save()
        {

        }

        public IQueryable<APIServices.Models.CourseInstanceMoreInformation> courseInstancesMoreInformation() 
        {
            return _courseInstancesMoreInformation.AsQueryable();
        }
    }
}
