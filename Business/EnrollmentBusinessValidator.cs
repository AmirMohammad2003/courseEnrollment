using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Exceptions;
using SystemGroup.Framework.Localization;
using SystemGroup.Framework.Logging;
using SystemGroup.Framework.Service;
using SystemGroup.General.CourseEnrollment.Common;


namespace SystemGroup.General.CourseEnrollment.Business
{
    public class EnrollmentBusinessValidator : BusinessValidator<Enrollment>
    {

        public override void Validate(Enrollment record, EntityActionType action)
        {
            base.Validate(record, action);

            record.Load<SemesterCoursePlan>(i => ((Enrollment)i).SemesterCoursePlan);
            var coursePlan = record.SemesterCoursePlan;
            coursePlan.Load<Semester>(i => ((SemesterCoursePlan)i).Semester);
            var semester = coursePlan.Semester;

            if (semester.State != SemesterStatus.Registering ||
                semester.EnrollmentStartTime > DateTime.Today ||
                semester.EnrollmentEndTime < DateTime.Today)
            {
                throw this.CreateException("ثبت نام ترم فقط در زمان مشخص شده مجاز است.");
            }

            var enrollmentBusiness = ServiceFactory.Create<IEnrollmentBusiness>();

            var allEnrollmentItems = from item in enrollmentBusiness.FetchDetail<EnrollmentItem>()
                                     join enrollment in enrollmentBusiness.FetchAllCurrentUserEnrollments()
                                     on item.EnrollmentRef equals enrollment.ID
                                     join coursePlanItem in ServiceFactory.Create<ISemesterCoursePlanBusiness>()
                                     .FetchDetail<SemesterCoursePlanItem>()
                                     on item.SemesterCoursePlanItemRef equals coursePlanItem.ID
                                     where item.Score >= 10 && item.Enrollment.PartyRef == record.PartyRef
                                     select item;

            record.Load<EnrollmentItem>(i => ((Enrollment)i).EnrollmentItems);
            var enrollmentItems = record.EnrollmentItems;
            var courses = from item in enrollmentItems
                          join coursePlanItem in ServiceFactory.Create<ISemesterCoursePlanBusiness>()
                          .FetchDetail<SemesterCoursePlanItem>(LoadOptions
                          .With<SemesterCoursePlanItem>(i => i.Course)
                          .With<SemesterCoursePlanItem>(i => i.EnrollmentItems)
                          .With<Course>(i => i.Prerequisites))
                          on item.SemesterCoursePlanItemRef equals coursePlanItem.ID
                          select new
                          {
                              EnrollmentItem = item,
                              coursePlanItem,
                              coursePlanItem.Course,
                              coursePlanItem.Course.Prerequisites
                          };

            var eligleCoursesOnPrerequisites = from course in courses
                                               where course.Prerequisites.Count == 0 ||
                                                     course.Prerequisites.Any(i => allEnrollmentItems.Any(e => e.SemesterCoursePlanItem.CourseRef == i.ID))
                                               select new
                                               {
                                                   course.Course,
                                                   course.EnrollmentItem
                                               };

            // TODO: List courses that are not eligible due to prerequisites
            if (eligleCoursesOnPrerequisites.Count() != courses.Count())
            {
                throw this.CreateException("پیش نیازهای دروس انتخاب شده رعایت نشده است.");
            }

            var passedIDs = from item in allEnrollmentItems
                            select item.SemesterCoursePlanItem.CourseRef;

            var eligleCoursesOnCourses = from course in courses
                                         where passedIDs.Contains(course.Course.ID) == false
                                         select new
                                         {
                                             course.Course,
                                             course.EnrollmentItem
                                         };
            SgLogger.LogDebug(eligleCoursesOnCourses.Count().ToString());
            SgLogger.LogDebug(courses.Count().ToString());
            // TODO: List courses that are already taken
            if (eligleCoursesOnCourses.Count() != courses.Count())
            {
                throw this.CreateException("درسی که انتخاب شده است، قبلا گذرانده شده است.");
            }

            var distinctCourses = courses.Select(i => i.Course).Distinct();
            if (distinctCourses.Count() != courses.Count())
            {
                throw this.CreateException("درسی که انتخاب شده است، تکراری است.");
            }

            var newCourses = courses.Where(i => i.EnrollmentItem.EntityModificationState == Entity.EntityState.New);
            var ineligibleCoursesOnCapacity = from course in newCourses
                                            let coursePlanItem = course.coursePlanItem
                                            where coursePlanItem.Capacity <= coursePlanItem.EnrollmentItems.Count()
                                            select course;
            if (ineligibleCoursesOnCapacity.Any())
            {
                var fullOnCapacity = from item in ineligibleCoursesOnCapacity
                                     select item.Course.Name;

                StringBuilder sb = new();
                foreach (var item in fullOnCapacity)
                {
                    sb.Append(item + " ");
                }
                                    
                throw this.CreateException($"ظرفیت {sb} پر است.");
            }
            // TODO: CHECK TIMETABLES

        }

    }
}
