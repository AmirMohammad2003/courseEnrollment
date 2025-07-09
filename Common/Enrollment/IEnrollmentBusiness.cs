using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.MetaData.Mapping;
using SystemGroup.Framework.Security;
using SystemGroup.Framework.Service;

namespace SystemGroup.General.CourseEnrollment.Common
{
    [ServiceInterface]
    public interface IEnrollmentBusiness : IBusinessBase<Enrollment>
    {
        [EntityView("AllCurrentUserEnrollments", "Labels_StudentEnrollments", typeof(EnrollmentSimpleProjection), "SemesterName", IsDefaultView = true, SearchInProjection = true, SecurityKey = "CourseEnrollment.Enrollment.Edit")]
        IQueryable<Enrollment> FetchAllCurrentUserEnrollments();

        [EntityView("AllCurrentUserStudentEnrollments", "Labels_StudentsEnrollment", typeof(EnrollmentProjection), "FullName", SearchInProjection = true, SecurityKey = "CourseEnrollment.Enrollment.Approval")]
        IQueryable<Enrollment> FetchAllCurrentUserStudentEnrollments();

        [EntityView("AllEnrollment", "Labels_Enrollments", typeof(EnrollmentSimpleProjection), "SemesterName", ShowInViewList = false, SearchInProjection = true)]
        new IQueryable<Enrollment> FetchAll();

        float GetGPA(Enrollment enrollment);

    }
}
