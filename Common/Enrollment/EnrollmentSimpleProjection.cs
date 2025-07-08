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
    public class EnrollmentSimpleProjection : EntityProjection<Enrollment>
    {
        #region Methods

        public override IQueryable Project(IQueryable<Enrollment> inputs)
        {
            return from item in inputs
                   join coursePlan in ServiceFactory.Create<ISemesterCoursePlanBusiness>()
                   .FetchAll(LoadOptions.With<SemesterCoursePlan>(i => i.Semester))
                   on item.SemesterCoursePlanRef equals coursePlan.ID
                   select new
                   {
                       item.ID,
                       item.GPA,
                       item.PartyRef,
                       item.SemesterCoursePlanRef,
                       item.State,
                       SemesterName = coursePlan.Semester.Name,
                   };
        }
        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new TextColumnInfo("SemesterName", "Labels_Semester"));
            columns.Add(new EntityColumnInfo<Enrollment>("State"));
            columns.Add(new EntityColumnInfo<Enrollment>("GPA"));
            columns.Add(new EntityColumnInfo<Enrollment>("SemesterCoursePlanRef"));
            columns.Add(new EntityColumnInfo<Enrollment>("PartyRef"));
        }

        #endregion
    }
}
