using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Service;
using SystemGroup.General.IPartyManagement.Common;

namespace SystemGroup.General.CourseEnrollment.Common
{
    public class SemesterCoursePlanItemProfessorProjection : EntityProjection<SemesterCoursePlanItem>
    {
        #region Methods

        public override IQueryable Project(IQueryable<SemesterCoursePlanItem> inputs)
        {
            return from item in inputs
                   join course in ServiceFactory.Create<ICourseBusiness>().FetchAll()
                   on item.CourseRef equals course.ID
                   join coursePlan in ServiceFactory.Create<ISemesterCoursePlanBusiness>()
                   .FetchAll(LoadOptions.With<SemesterCoursePlan>(i => i.Major))
                   on item.SemesterCoursePlanRef equals coursePlan.ID
                   select new { 
                       item.ID, 
                       item.PartyRef,
                       item.CourseRef,
                       item.Capacity,
                       item.SemesterCoursePlanRef,
                       CourseName = course.Name, 
                       Major = coursePlan.Major.Name,
                   };
        }

        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new EntityColumnInfo<SemesterCoursePlanItem>("PartyRef"));
            columns.Add(new EntityColumnInfo<SemesterCoursePlanItem>("CourseRef"));
            columns.Add(new EntityColumnInfo<SemesterCoursePlanItem>("SemesterCoursePlanRef"));
            columns.Add(new TextColumnInfo("CourseName", "Labels_Course"));
            columns.Add(new TextColumnInfo("Major", "Labels_Major"));
            columns.Add(new EntityColumnInfo<SemesterCoursePlanItem>("Capacity"));
        }

        #endregion
    }
}
