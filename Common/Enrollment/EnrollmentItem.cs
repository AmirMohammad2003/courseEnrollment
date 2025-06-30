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
    [AssociatedWith(typeof(SemesterCoursePlanItem), "SemesterCoursePlanItemRef", AssociationType.ManyToOne)]
    partial class EnrollmentItem : Entity
    {
        #region Properties 
        public string CourseName { get; set; }
        public string PartyName { get; set; }
        #endregion

        #region Methods

        public override string GetEntityName()
        {
            return "درس ثبت نام شده"; 
        }
        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new ReferenceColumnInfo("EnrollmentRef", "_"));
            columns.Add(new ReferenceColumnInfo("SemesterCoursePlanItemRef", "_"));
        }

        public static void FillExtraProperties(IList<EnrollmentItem> list)
        {
            if (list == null || list.Count == 0)
            {
                return;
            }
            var itemsDetail = (from item in list
            join planItem in ServiceFactory.Create<ISemesterCoursePlanBusiness>().
            FetchDetail<SemesterCoursePlanItem>(LoadOptions.With<SemesterCoursePlanItem>(i => i.Course))
            on item.SemesterCoursePlanItemRef equals planItem.ID
            join party in ServiceFactory.Create<IPartyManagementService>().FetchParties()
            on planItem.PartyRef equals party.ID
            select new { item.ID, CourseName= planItem.Course.Name, PartyName= party.FullName })
            .Distinct().ToDictionary(i => i.ID);

            foreach (var item in list)
            {
                item.CourseName = itemsDetail[item.ID].CourseName;
                item.PartyName = itemsDetail[item.ID].PartyName;
            }
        }

        #endregion
    }
}
