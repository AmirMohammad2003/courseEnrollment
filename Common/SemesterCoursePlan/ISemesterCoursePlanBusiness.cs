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
        [EntityView("AllSemesterCoursePlan", "برنامه های تحصیلی", typeof(SemesterCoursePlanProjection), "SemesterName", IsDefaultView = true)]
        new IQueryable<SemesterCoursePlan> FetchAll();

        [EntityView("AllUserEligibleSemesterCoursePlan", "برنامه های تحصیلی مجاز برای دانشجو", typeof(SemesterCoursePlanProjection), "SemesterName", ShowInViewList = false)]
        IQueryable<SemesterCoursePlan> FetchAllUserEligibleSemesterCoursePlan();

        [EntityView("AllSemesterCoursePlanItems", "دروس ثبت نامی", typeof(SemesterCoursePlanItemProjection), "CourseName", ShowInViewList = false)]
        IQueryable<SemesterCoursePlanItem> FetchAllSemesterCoursePlanItems(long id);

    }
}
