using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Lookup;
using SystemGroup.Framework.MetaData;
using SystemGroup.Framework.MetaData.Mapping;
using SystemGroup.Framework.Service;
using SystemGroup.Framework.StateManagement;
using SystemGroup.General.IPartyManagement.Common;

namespace SystemGroup.General.CourseEnrollment.Common
{
    [Serializable]
    [DetailOf(typeof(Enrollment), "EnrollmentRef")]
    partial class EnrollmentItem : Entity
    {
        #region Properties 

        public string CourseName { get; set; }
        public string PartyName { get; set; }
        public string TimeTables { get; set; }

        #endregion

        #region Methods

        public override string GetEntityName()
        {
            return "Labels_EnrolledCourse";
        }

        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new NumericColumnInfo("Score", "Labels_Score", NumericType.FloatingPoint));
            columns.Add(new ReferenceColumnInfo("EnrollmentRef", "_"));
            columns.Add(new ReferenceColumnInfo("SemesterCoursePlanItemRef", "_"));
        }

        public static string formatTime(int time)
        {
            var hour = time / 60;
            var minute = time % 60;
            if (minute < 10)
            {
                return hour + ":0" + minute;
            }
            return hour + ":" + minute;
        }

        public static void FillPartyName(IList<EnrollmentItem> list)
        {
            if (list == null || list.Count == 0)
            {
                return;
            }

            var itemsDetail = (from item in list
                               join party in ServiceFactory.Create<IPartyManagementService>().FetchParties()
                               on item.Enrollment.PartyRef equals party.ID
                               select new
                               {
                                   item.ID,
                                   PartyName = party.FullName,
                               }).Distinct().ToDictionary(i => i.ID);

            foreach (var item in list)
            {
                item.PartyName = itemsDetail[item.ID].PartyName;
            }
        }

        public static void FillExtraProperties(IList<EnrollmentItem> list)
        {
            if (list == null || list.Count == 0)
            {
                return;
            }

            var itemsDetail = (from item in list
                               join planItem in ServiceFactory.Create<ISemesterCoursePlanBusiness>().
                               FetchDetail<SemesterCoursePlanItem>(LoadOptions
                               .With<SemesterCoursePlanItem>(i => i.Course)
                               .With<SemesterCoursePlanItem>(i => i.TimeTables))
                               on item.SemesterCoursePlanItemRef equals planItem.ID
                               join party in ServiceFactory.Create<IPartyManagementService>().FetchParties()
                               on planItem.PartyRef equals party.ID
                               select new
                               {
                                   item.ID,
                                   CourseName = planItem.Course.Name,
                                   PartyName = party.FullName,
                                   SemesterCoursePlanItem = planItem
                               }).Distinct().ToDictionary(i => i.ID);

            foreach (var item in list)
            {
                item.CourseName = itemsDetail[item.ID].CourseName;
                item.PartyName = itemsDetail[item.ID].PartyName;

                var timetables = itemsDetail[item.ID].SemesterCoursePlanItem.TimeTables;

                item.TimeTables = string.Join(", ", timetables.Select(timetable =>
                    $"{LookupService.Lookup(timetable.DayOfTheWeek).Value} {formatTime(timetable.Start)}-{formatTime(timetable.End)}"));
            }
        }


        #endregion
    }
}
