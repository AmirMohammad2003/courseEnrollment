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

namespace SystemGroup.General.CourseEnrollment.Common
{
    [Serializable]
    [Master(typeof(ISemesterBusiness))]
    [DataNature(DataNature.MasterData)]
    partial class Semester : Entity
    {
        #region Methods

        public override string GetEntityName()
        {
            return "Labels_Semester"; 
        }
        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new TextColumnInfo("Name", "Labels_Name"));
            columns.Add(new DateTimeColumnInfo("StartDate", "Labels_SemesterStartDate"));
            columns.Add(new DateTimeColumnInfo("EndDate", "Labels_SemesterEndDate"));
            columns.Add(new DateTimeColumnInfo("EnrollmentStartTime", "Labels_EnrollmentStartTime"));
            columns.Add(new DateTimeColumnInfo("EnrollmentEndTime", "Labels_EnrollmentEndTime"));
            columns.Add(new StateColumnInfo("State", "Labels_SemesterState", typeof(Semester)));
        }

        #endregion
    }
}
