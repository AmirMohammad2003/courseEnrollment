using System;
using System.Collections.Generic;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Localization;
using SystemGroup.Framework.Lookup;
using SystemGroup.Framework.MetaData;

namespace SystemGroup.General.CourseEnrollment.Common
{
    [Serializable]
    [DetailOf(typeof(SemesterCoursePlanItem), "SemesterCoursePlanItemRef")]
    partial class TimeTable : Entity
    {

        #region Properties 

        public string UI_DayOfTheWeek { get; set; }

        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            this.DayOfTheWeek = DayOfTheWeek.Saturday;
        }

        #endregion

        #region Methods

        public override string GetEntityName()
        {
            return "Labels_TimeTable";
        }

        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new ReferenceColumnInfo("SemesterCoursePlanItemRef", "_"));
            columns.Add(new LookupColumnInfo("DayOfTheWeek", "Labels_DayOfTheWeek", nameof(DayOfTheWeek)));
            columns.Add(new TimeColumnInfo("Start", "Labels_StartTime"));
            columns.Add(new TimeColumnInfo("End", "Labels_EndTime"));
        }

        public static void FillExtraProperties(IList<TimeTable> list)
        {
            foreach (var item in list)
            {
                item.UI_DayOfTheWeek = LookupService.Lookup<DayOfTheWeek>(item.DayOfTheWeek).Value;
            }
        }

        #endregion
    }
}
