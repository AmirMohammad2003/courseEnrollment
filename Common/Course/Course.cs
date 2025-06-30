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
    [Master(typeof(ICourseBusiness))]
    [SearchFields("Name", "Units")]
    [DataNature(DataNature.MasterData)]
    partial class Course : Entity
    {
        #region Properties

        public override DetailLoadOptions DeleteLoadOptions
        {
            get
            {
                return LoadOptions.With<Course>(i => i.Prerequisites)
                    .With<Course>(i => i.Prerequisites1)
                    .With<Course>(i => i.MajorCourses);
            }
        }

        #endregion

        #region Methods

        public override string GetEntityName()
        {
            return "درس"; 
        }
        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new TextColumnInfo("Name", "نام درس"));
            columns.Add(new NumericColumnInfo("Units", "تعداد واحد", NumericType.Integer));
        }

        #endregion
    }
}
