using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Eventing;
using SystemGroup.Framework.Exceptions;
using SystemGroup.Framework.Host;
using SystemGroup.Framework.Localization;
using SystemGroup.Framework.Logging;
using SystemGroup.Framework.Service;
using SystemGroup.Framework.Service.Attributes;
using SystemGroup.General.CourseEnrollment.Common;

namespace SystemGroup.General.CourseEnrollment.Business
{
    [Service]
    public class CourseBusiness : BusinessBase<Course>, ICourseBusiness
    {

        [ServiceDependency]
        public virtual IMajorBusiness MajorBusiness { get; set; }


        [SubscribeTo(typeof(IHostService), "HostStarted")]
        public void OnHostStarted(object sender, EventArgs e)
        {
            BusinessValidationProvider.RegisterValidator<Course>(new CourseBusinessValidator());
        }

        public virtual IQueryable<Course> FetchAllMajorCourses(long id)
        {
            var majorCourses = MajorBusiness.FetchDetail<MajorCourse>().Select(i => i.CourseRef);

            return from course in FetchAll()
                   join mc in majorCourses on course.ID equals mc
                   select course;
        }
    }
}
