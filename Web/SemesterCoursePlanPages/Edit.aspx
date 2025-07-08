<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs"
    Inherits="SystemGroup.General.CourseEnrollment.Web.SemesterCoursePlanPages.Edit" 
    Title="Labels_SemesterCoursePlan"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <sg:SgScriptManager runat="server" ID="scriptManager">
            <Scripts>
                <asp:ScriptReference Path="Edit.js" />
            </Scripts>
        </sg:SgScriptManager>
        <sg:SgUpdatePanel runat="server" ID="updMain">
            <ContentTemplate>
                <div runat="server" id="dvMain">
                    <sg:sgfieldset id="fldsetMain" runat="server">
                        <sg:sgfieldlayout runat="server" LabelCellWidth="130px">
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" textkey="Labels_Semester" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgselector runat="server" id="sltSemester"
                                        componentname="SystemGroup.General.CourseEnrollment"
                                        entityname="Semester" viewname="AllSemester" >
                                    </sg:sgselector>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="sltSemester" 
                                        errormessagekey="Messages_ChooseSemester"></sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" textkey="Labels_Major" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgselector runat="server" id="sltMajor"
                                        componentname="SystemGroup.General.CourseEnrollment"
                                        entityname="Major" viewname="AllMajor" >
                                    </sg:sgselector>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="sltMajor" 
                                        errormessagekey="Messages_ChooseMajor"></sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                        </sg:sgfieldlayout>
                    </sg:sgfieldset>
                    <sg:sggrid runat="server" id="grdCourses" gridtype="ClientSide" allowscroll="true"
                        allowedit="true" allowdelete="true" allowinsert="true" isMaster="true"
                        datasourceid=".SemesterCoursePlanItems" validationgroup="vgGrid">
                        <Columns>
                            <sg:SgSelectorGridColumn PropertyName="CourseName" HeaderText="Labels_Course">
                                <EditItemTemplate>
                                    <sg:SgSelector runat="server" ID="sltCourse"
                                        ComponentName="SystemGroup.General.CourseEnrollment"
                                        EntityName="Course" ViewName="AllMajorCourses" 
                                        CbSelectedID="{binding CourseRef}" 
                                        OnClientSelectedIndexChanged="sltCourse_selectedIndexChanged"
                                        OnClientItemsRequesting="sltCourse_itemsRequesting"
                                        OnItemsRequested="sltCourse_ItemsRequested">
                                        <Properties>
                                            <sg:SgSelectorProperty Name="Name" ClientSide="true"/>
                                        </Properties>
                                        <ViewParameters>
                                            <sg:SgViewParameter Name="id" />
                                        </ViewParameters>

                                    </sg:SgSelector>
                                    <sg:SgRequiredFieldValidator runat="server" ControlToValidate="sltCourse"
                                        errormessagekey="Messages_ChooseCourse" ValidationGroup="vgGrid"/>
                                </EditItemTemplate>
                            </sg:SgSelectorGridColumn>
                            <sg:SgSelectorGridColumn PropertyName="PartyName" HeaderText="Labels_Professor">
                                <EditItemTemplate>
                                    <sg:SgSelector runat="server" ID="sltParty"
                                        ComponentName="SystemGroup.General.IPartyManagement"
                                        EntityName="IParty" ViewName="AllProfessorParties" 
                                        CbSelectedID="{binding PartyRef}"
                                        OnClientSelectedIndexChanged="sltParty_selectedIndexChanged">
                                        <Properties>
                                            <sg:SgSelectorProperty Name="FullName" ClientSide="true"/>
                                        </Properties>
                                    </sg:SgSelector>
                                    <sg:SgRequiredFieldValidator runat="server" ControlToValidate="sltCourse"
                                        errormessagekey="Messages_ChooseProfessor" ValidationGroup="vgGrid"/>
                                </EditItemTemplate>
                            </sg:SgSelectorGridColumn>
                            <sg:SgNumericGridColumn PropertyName="Capacity" HeaderText="Labels_Capacity">
                                <EditItemTemplate>
                                    <sg:sgnumerictextbox runat="server" id="txtCapacity" value="1" minvalue="0" maxvalue="9999999"
                                        numberformat-decimaldigits="0" datatype="System.Int" CbValue="{binding Capacity}" />
                                </EditItemTemplate>
                            </sg:SgNumericGridColumn>
                            <sg:SgTextGridColumn PropertyName="Taken" HeaderText="Labels_Taken" AllowEdit="false" />
                        </Columns>
                    </sg:SgGrid>
                    <sg:sggrid runat="server" id="grdTimeTables" gridtype="ClientSide" 
                        allowscroll="true" allowedit="true" allowdelete="true" allowinsert="true"
                        datasourceid=".SemesterCoursePlanItems.TimeTables" validationgroup="vgTimeGrid">
                        <Columns>
                            <Sg:SgLookupGridColumn 
                                PropertyName="UI_DayOfTheWeek" 
                                HeaderText="Labels_DayOfTheWeek">
                                <EditItemTemplate>
                                    <sg:sglookup
                                        id="lkpDayOfTheWeek"
                                        runat="server"
                                        lookuptype="DayOfTheWeek" 
                                        CbSelectedCode="{binding DayOfTheWeek}" 
                                        OnClientSelectedCodeChanged="lkpDayOfTheWeek_SelectedCodeChanged" />
                                </EditItemTemplate>
                            </Sg:SgLookupGridColumn>
                            <sg:SgTimePickerGridColumn PropertyName="Start" HeaderText="Labels_StartTime"> 
                                <EditItemTemplate>
                                    <sg:SgTimePicker runat="server" id="tpStart" CbSelectedTime="{binding Start}" />
                                </EditItemTemplate>
                            </sg:SgTimePickerGridColumn>
                            <sg:SgTimePickerGridColumn propertyname="End" headertext="Labels_EndTime">
                                <EditItemTemplate>
                                    <sg:SgTimePicker runat="server" id="tpEnd" CbSelectedTime="{binding End}" />
                                </EditItemTemplate>
                            </sg:SgTimePickerGridColumn>
                        </Columns>
                    </sg:SgGrid>
                </div>
            </ContentTemplate>
        </sg:SgUpdatePanel>
    </form>
</body>
</html>
