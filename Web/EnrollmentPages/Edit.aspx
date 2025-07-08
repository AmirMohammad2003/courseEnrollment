<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs"
    Inherits="SystemGroup.General.CourseEnrollment.Web.EnrollmentPages.Edit" 
    Title="Labels_RegisterTitle"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <sg:sgscriptmanager runat="server" id="scriptManager">
            <scripts>
                <asp:ScriptReference Path="Edit.js" />
            </scripts>
        </sg:sgscriptmanager>
        <sg:sgupdatepanel runat="server" id="updMain">
            <contenttemplate>
                <div runat="server" id="dvMain">
                    <sg:sgfieldset id="fldsetMain" runat="server">
                        <sg:sgfieldlayout runat="server" labelcellwidth="130px">
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" textkey="Labels_SemesterCoursePlan" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgselector runat="server" id="sltSemesterCoursePlan"
                                        componentname="SystemGroup.General.CourseEnrollment"
                                        entityname="SemesterCoursePlan" viewname="AllUserEligibleSemesterCoursePlan"
                                        OnClientSelectedIndexChanged="sltCoursePlan_selectedIndexChanged"
                                        OnClientSelectedIndexChanging="sltCoursePlan_selectedIndexChanging">
                                    </sg:sgselector>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="sltSemesterCoursePlan"
                                        errormessagekey="Messages_ChooseSemesterCoursePlan">
                                    </sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                        </sg:sgfieldlayout>
                    </sg:sgfieldset>
                    <sg:sgtabstrip id="tbsTab" runat="server" multipageid="mpgMultiPage">
                        <tabs>
                            <sg:sgtab runat="server" text="دروس منتخب" pageviewid="rpvGrid" />
                        </tabs>
                    </sg:sgtabstrip>
                    <sg:sgmultipage id="mpgMultiPage" runat="server">
                        <telerik:radpageview id="rpvGrid" runat="server">
                            <sg:sggrid runat="server" id="grdCourses" gridtype="ClientSide" allowscroll="true"
                                allowedit="true" allowdelete="true" allowinsert="true"
                                datasourceid=".EnrollmentItems" validationgroup="vgGrid"
                                Width="780px" ContentWidth="1280px" Height="240px">
                                <columns>
                                    <sg:sgselectorgridcolumn propertyname="CourseName" headertext="Labels_Class">
                                        <edititemtemplate>
                                            <sg:sgselector runat="server" id="sltCourse"
                                                componentname="SystemGroup.General.CourseEnrollment"
                                                entityname="SemesterCoursePlanItem" viewname="AllSemesterCoursePlanItems"
                                                cbselectedid="{binding SemesterCoursePlanItemRef}"
                                                onclientselectedindexchanged="sltCourse_selectedIndexChanged"
                                                onclientselectedindexchanging="sltCourse_selectedIndexChanging"
                                                onclientitemsrequesting="sltCourse_itemsRequesting"
                                                onitemsrequested="sltCourse_ItemsRequested">
                                                <properties>
                                                    <sg:sgselectorproperty name="CourseName" clientside="true" />
                                                    <sg:sgselectorproperty name="PartyName" clientside="true" />
                                                </properties>
                                                <viewparameters>
                                                    <sg:sgviewparameter name="id" />
                                                </viewparameters>
                                            </sg:sgselector>
                                            <sg:sgrequiredfieldvalidator runat="server" controltovalidate="sltCourse"
                                                errormessagekey="Messages_ChooseSemesterCoursePlanItem" validationgroup="vgGrid" />
                                        </edititemtemplate>
                                    </sg:sgselectorgridcolumn>
                                    <sg:sgtextgridcolumn propertyname="PartyName" headertext="Labels_Professor" allowedit="false" />
                                    <sg:sgtextgridcolumn propertyname="TimeTables" headertext="Labels_TimeTable" allowedit="false" />
                                </columns>
                            </sg:sggrid>
                        </telerik:radpageview>
                    </sg:sgmultipage>
                </div>
            </contenttemplate>
        </sg:sgupdatepanel>
    </form>
</body>
</html>
