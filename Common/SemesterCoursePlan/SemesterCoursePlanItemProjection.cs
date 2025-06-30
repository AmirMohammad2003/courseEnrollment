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
    public class SemesterCoursePlanItemProjection : EntityProjection<SemesterCoursePlanItem>
    {
        #region Methods

        public override IQueryable Project(IQueryable<SemesterCoursePlanItem> inputs)
        {
            return from item in inputs
                   join course in ServiceFactory.Create<ICourseBusiness>().FetchAll()
                   on item.CourseRef equals course.ID
                   join party in ServiceFactory.Create<IPartyManagementService>().FetchParties()
                   on item.PartyRef equals party.ID
                   select new { 
                       item.ID, 
                       item.PartyRef,
                       item.CourseRef,
                       CourseName = course.Name, 
                       PartyName = party.FullName
                   };
        }
        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new EntityColumnInfo<SemesterCoursePlanItem>("PartyRef"));
            columns.Add(new EntityColumnInfo<SemesterCoursePlanItem>("CourseRef"));
            columns.Add(new TextColumnInfo("CourseName", "نام درس"));
            columns.Add(new TextColumnInfo("PartyName", "نام استاد"));
        }
        

        #endregion
    }
}
