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
    public interface ISemesterCoursePlanBusiness : IBusinessBase<SemesterCoursePlan>
    {
        [EntityView("AllSemesterCoursePlan", "Labels_SemesterCoursePlans", typeof(SemesterCoursePlanProjection), "SemesterName", IsDefaultView = true, SearchInProjection = true)]
        new IQueryable<SemesterCoursePlan> FetchAll();

        [EntityView("AllUserEligibleSemesterCoursePlan", "Labels_UserEligibleSemesterCoursePlans", typeof(SemesterCoursePlanProjection), "SemesterName", ShowInViewList = false, SearchInProjection = true)]
        IQueryable<SemesterCoursePlan> FetchAllUserEligibleSemesterCoursePlan();

        [EntityView("AllSemesterCoursePlanItems", "Labels_EnrolledCourses", typeof(SemesterCoursePlanItemProjection), "CourseName", ShowInViewList = false, SearchInProjection = true)]
        IQueryable<SemesterCoursePlanItem> FetchAllSemesterCoursePlanItems(long id);

        [EntityView("AllProfessorSemesterCoursePlanItems", "Labels_TeachingCourses", typeof(SemesterCoursePlanItemProfessorProjection), "CourseName", SearchInProjection = true, SecurityKey = "CourseEnrollment.Enrollment.Approval")]
        IQueryable<SemesterCoursePlanItem> FetchAllProfessorSemesterCoursePlanItems();

    }
}
