using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Service;
using SystemGroup.General.IPartyManagement.Common;

namespace SystemGroup.General.CourseEnrollment.Common
{
    public class SemesterCoursePlanProjection : EntityProjection<SemesterCoursePlan>
    {
        #region Methods

        public override IQueryable Project(IQueryable<SemesterCoursePlan> inputs)
        {
            return from input in inputs
                   join semester in ServiceFactory.Create<ISemesterBusiness>().FetchAll()
                   on input.SemesterRef equals semester.ID
                   join major in ServiceFactory.Create<IMajorBusiness>().FetchAll()
                   on input.MajorRef equals major.ID
                   select new
                   {
                       input.ID,
                       input.SemesterRef,
                       SemesterName= semester.Name,
                       input.MajorRef,
                       MajorName= major.Name,
                   };
        }

        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new EntityColumnInfo<SemesterCoursePlan>("SemesterRef"));
            columns.Add(new TextColumnInfo("SemesterName", "Labels_Semester"));
            columns.Add(new EntityColumnInfo<SemesterCoursePlan>("MajorRef"));
            columns.Add(new TextColumnInfo("MajorName", "Labels_Major"));
        }

        #endregion
    }
}
