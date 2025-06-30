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
        [EntityView("AllCurrentUserEnrollments", "ثبت نام های دانشجو", typeof(EnrollmentSimpleProjection), "Name", IsDefaultView = true, SecurityKey = "CourseEnrollment.Enrollment.Edit")]
        IQueryable<Enrollment> FetchAllCurrentUserEnrollments();

        [EntityView("AllCurrentUserStudentEnrollments", "ثبت نام های دانشجوها", typeof(EnrollmentProjection), "Name", SecurityKey = "CourseEnrollment.Enrollment.Approval")]
        IQueryable<Enrollment> FetchAllCurrentUserStudentEnrollments();

        [EntityView("AllCurrentUserStudentNotApprovedEnrollments", "ثبت نام های تایید نشده دانشجوها", typeof(EnrollmentProjection), "Name", IsDefaultView = false, SecurityKey = "CourseEnrollment.Enrollment.Approval")]
        IQueryable<Enrollment> FetchAllCurrentUserStudentNotApprovedEnrollments();

        [EntityView("AllEnrollment", "ثبت نام ها", typeof(EnrollmentSimpleProjection), "Name", ShowInViewList = false)]
        new IQueryable<Enrollment> FetchAll();

    }
}
