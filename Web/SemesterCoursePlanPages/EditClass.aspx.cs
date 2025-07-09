using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.Localization;
using SystemGroup.Framework.Service;
using SystemGroup.Framework.Utilities;
using SystemGroup.General.CourseEnrollment.Common;
using SystemGroup.Web.UI;
using SystemGroup.Web.UI.Controls;
using SystemGroup.Web.UI.Localization;

namespace SystemGroup.General.CourseEnrollment.Web.SemesterCoursePlanPages
{
    public partial class EditClass : SgPage
    {
        #region Properties 

        private SgEntityDataSource<EnrollmentItem> dsParties;

        private SemesterCoursePlanItem semesterCoursePlanItem;

        static public IEnrollmentBusiness EnrollmentBusiness { get; } = ServiceFactory.Create<IEnrollmentBusiness>();

        private new long ID;

        #endregion

        #region Methods

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            string idText = Request.QueryString["id"] ?? throw this.CreateException("Bad Request");
            ID = Convert.ToInt64(idText); 

            semesterCoursePlanItem = ServiceFactory.Create<ISemesterCoursePlanBusiness>()
                .FetchDetail<SemesterCoursePlanItem>()
                .Where(i => i.ID == ID).First();

            SemesterCoursePlanItem.FillExtraProperties([semesterCoursePlanItem]);

            dsParties = new()
            {
                ID = "edsParties",
                OperationalOnClientSide = true,
                AllowDelete = false,
                AllowInsert = false,
                AllowUpdate = true,
            };
            
            Form.Controls.Add(dsParties);


            HotKeyList.Add(new HotKey(HotKeys.StandardSave, this.Translate("Labels_DefaultSharing_Insert"), "Save"));
            OnClientHotKeyPress = "onHotKeyPress";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                Title = this.Translate("Labels_Scoring") + " " + semesterCoursePlanItem.CourseName;
                InitializeRecords();
            }
        }

        protected void InitializeRecords()
        {
            var parties = EnrollmentBusiness
                .FetchDetail<EnrollmentItem>(LoadOptions.With<EnrollmentItem>(i => i.Enrollment))
                .Where(i => i.SemesterCoursePlanItemRef == ID && i.Enrollment.State == EnrollmentStatus.Approved)
                .ToList();

            EnrollmentItem.FillPartyName(parties);
            dsParties.Entities = parties;
        }

        protected void GrdParties_Command(object sender, SgGridCommandEventArgs e)
        {
            switch (e.UniqueName)
            {
                case "Save":
                    var entitiesForSave = dsParties.Entities.ToList();
                    Dictionary<long, Dictionary<long, EnrollmentItem>> entitiesByEnrollment = [];
                    foreach (var entity in entitiesForSave)
                    {
                        if (!entitiesByEnrollment.TryGetValue(entity.EnrollmentRef, out Dictionary<long, EnrollmentItem> value))
                        {
                            value = [];
                            entitiesByEnrollment.Add(entity.EnrollmentRef, value);
                        }
                        value.Add(entity.ID, entity);
                    }

                    bool globalChanged = false;
                    bool rangeViolation = false;
                    foreach (var kv in entitiesByEnrollment)
                    {
                        long id = kv.Key;
                        var enrollment = EnrollmentBusiness
                            .FetchByID(id, LoadOptions.With<Enrollment>(i => i.EnrollmentItems))
                            .First();

                        bool changed = false;
                        foreach (var item in enrollment.EnrollmentItems)
                        {
                            var newItem = kv.Value.TryGetOrNull(item.ID) as EnrollmentItem;
                            if (newItem == null || item.Score == newItem.Score)
                            {
                                continue;
                            }

                            if (newItem.Score <= 20 && newItem.Score >= 0)
                            {
                                changed = true;
                                item.Score = newItem.Score;
                            }
                            else
                            {
                                rangeViolation = true;
                            }
                        }


                        if (changed)
                        {
                            globalChanged = true;
                            enrollment.GPA = EnrollmentBusiness.GetGPA(enrollment);
                            EnrollmentBusiness.Save(ref enrollment);
                        }
                    }

                    if (globalChanged)
                    {
                        SgMessageBox.Show("General.CourseEnrollment:Messages_ScoresSavedSuccessfuly", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }

                    if (rangeViolation)
                    {
                        SgMessageBox.Show("General.CourseEnrollment:Messages_ScoreRangeViolation", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }

                    break;

                case "Reload":
                    InitializeRecords();

                    break;
            }
        }

        #endregion
    }
}