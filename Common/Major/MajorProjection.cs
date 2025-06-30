using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Common;

namespace SystemGroup.General.CourseEnrollment.Common
{
    public class MajorProjection : EntityProjection<Major>
    {
        #region Methods

        public override IQueryable Project(IQueryable<Major> inputs)
        {
            return inputs;
        }
        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new EntityColumnInfo<Major>("Name"));
            columns.Add(new EntityColumnInfo<Major>("Units"));
        }

        #endregion
    }
}
