using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Common;

namespace SystemGroup.General.CourseEnrollment.Common
{
    public class SemesterProjection : EntityProjection<Semester>
    {
        #region Methods

        public override IQueryable Project(IQueryable<Semester> inputs)
        {
            return inputs;
        }
        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new EntityColumnInfo<Semester>("Name"));
            columns.Add(new EntityColumnInfo<Semester>("StartDate"));
            columns.Add(new EntityColumnInfo<Semester>("EndDate"));
            columns.Add(new EntityColumnInfo<Semester>("EnrollmentStartTime"));
            columns.Add(new EntityColumnInfo<Semester>("EnrollmentEndTime")); 
            columns.Add(new EntityColumnInfo<Semester>("State")); 
        }

        #endregion
    }
}
