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
    partial class Semester : Entity
    {
        #region Methods

        public override string GetEntityName()
        {
            return "ترم"; 
        }
        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new TextColumnInfo("Name", "نام"));
            columns.Add(new DateTimeColumnInfo("StartDate", "زمان شروع ترم"));
            columns.Add(new DateTimeColumnInfo("EndDate", "زمان پایان ترم"));
            columns.Add(new DateTimeColumnInfo("EnrollmentStartTime", "زمان شروع انتخاب واحد"));
            columns.Add(new DateTimeColumnInfo("EnrollmentEndTime", "زمان پایان انتخاب واحد"));
            columns.Add(new StateColumnInfo("State", "وضعیت ترم", typeof(Semester)));
        }

        #endregion
    }
}
