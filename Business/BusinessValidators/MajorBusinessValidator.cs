using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Exceptions;
using SystemGroup.Framework.Localization;
using SystemGroup.Framework.Service;
using SystemGroup.General.CourseEnrollment.Common;


namespace SystemGroup.General.CourseEnrollment.Business
{
    public class MajorBusinessValidator : BusinessValidator<Major>
    {
        public override void Validate(Major record, EntityActionType action)
        {
            base.Validate(record, action);

            HashSet<long> ids = [];

            var items = record.MajorCourses;

            var courses = from item in items
                          join course in ServiceFactory.Create<ICourseBusiness>()
                          .FetchAll(LoadOptions.With<Course>(i => i.Prerequisites))
                          on item.CourseRef equals course.ID
                          select course;

            foreach (var course in courses)
            {
                ids.Add(course.ID);
            }

            foreach (var course in courses)
            {
                foreach (var prerequisite in course.Prerequisites)
                {
                    if (!ids.Contains(prerequisite.PrerequisiteCourseRef))
                    {
                        throw this.CreateException("پیشنیاز های دروس رعایت نشده است.");
                    }
                }
            }
        }

    }
}
