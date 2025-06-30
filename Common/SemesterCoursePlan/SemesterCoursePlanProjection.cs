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
            columns.Add(new TextColumnInfo("SemesterName", "ترم"));
            columns.Add(new EntityColumnInfo<SemesterCoursePlan>("MajorRef"));
            columns.Add(new TextColumnInfo("MajorName", "رشته تحصیلی"));
        }

        #endregion
    }
}

            //return from input in inputs
            //       join semester in ServiceFactory.Create<ISemesterBusiness>().FetchAll()
            //       on input.SemesterRef equals semester.ID
            //       join course in ServiceFactory.Create<ICourseBusiness>().FetchAll()
            //       on input.CourseRef equals course.ID
            //       join party in ServiceFactory.Create<IPartyManagementService>().FetchParties()
            //       on input.PartyRef equals party.ID
            //       select new
            //       {
            //           input.ID,
            //           input.SemesterRef,
            //           SemesterName= semester.Name,
            //           input.CourseRef,
            //           CourseName= course.Name,
            //           input.PartyRef,
            //           PartyName= party.FullName,
            //           input.Capacity,
            //           input.Taken
            //       };


            //columns.Add(new EntityColumnInfo<SemesterCoursePlan>("PartyRef"));
            //columns.Add(new TextColumnInfo("PartyName", "نام استاد"));
            //columns.Add(new EntityColumnInfo<SemesterCoursePlan>("CourseRef"));
            //columns.Add(new TextColumnInfo("CourseName", "نام درس"));
            //columns.Add(new EntityColumnInfo<SemesterCoursePlan>("Capacity"));
            //columns.Add(new EntityColumnInfo<SemesterCoursePlan>("Taken"));
