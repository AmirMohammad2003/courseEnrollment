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
    public class EnrollmentItemProjection : EntityProjection<EnrollmentItem>
    {
        #region Methods

        public override IQueryable Project(IQueryable<EnrollmentItem> inputs)
        {
            return from input in inputs
                   join planItem in ServiceFactory.Create<ISemesterCoursePlanBusiness>().
                   FetchDetail<SemesterCoursePlanItem>(LoadOptions.With<SemesterCoursePlanItem>(i => i.Course))
                   on input.SemesterCoursePlanItemRef equals planItem.ID
                   join party in ServiceFactory.Create<IPartyManagementService>().FetchParties()
                   on planItem.PartyRef equals party.ID
                   select new
                   {
                       input.ID, 
                       input.EnrollmentRef,
                       input.SemesterCoursePlanItemRef,
                       CourseName= planItem.Course.Name,
                       PartyName= party.FullName,
                   };

        }
        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new EntityColumnInfo<EnrollmentItem>("EnrollmentRef"));
            columns.Add(new EntityColumnInfo<EnrollmentItem>("SemesterCoursePlanItemRef"));
            columns.Add(new EntityColumnInfo<EnrollmentItem>("Score"));
            columns.Add(new TextColumnInfo("CourseName", "نام درس"));
            columns.Add(new TextColumnInfo("PartyName", "نام استاد"));
        }

        #endregion
    }
}
