<%@ page language="C#" autoeventwireup="true" codebehind="Edit.aspx.cs"
    inherits="SystemGroup.General.CourseEnrollment.Web.CoursePages.Edit"
    title="Labels_Course" %>

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
                        <sg:sgfieldlayout runat="server">
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" textkey="Labels_Name" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgtextbox runat="server" id="txtName" width="135px"></sg:sgtextbox>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="txtName"
                                        errormessagekey="Messages_EnterCourseName"></sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" textkey="Labels_Units" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgnumerictextbox runat="server" id="txtUnits" value="1" minvalue="0" maxvalue="99"
                                        numberformat-decimaldigits="0" width="135px" datatype="System.Int" />
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="txtUnits"
                                        errormessagekey="Messages_EnterUnits"></sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                        </sg:sgfieldlayout>
                    </sg:sgfieldset>
                    <sg:sgtabstrip id="tbsTab" runat="server" multipageid="mpgMultiPage">
                        <tabs>
                            <sg:sgtab runat="server" textkey="Labels_Prerequisites" pageviewid="rpvGrid" />
                        </tabs>
                    </sg:sgtabstrip>
                    <sg:sgmultipage id="mpgMultiPage" runat="server">
                        <telerik:radpageview id="rpvGrid" runat="server">

                            <sg:sggrid runat="server" id="grdPrerequisites" gridtype="ClientSide" allowscroll="true"
                                allowedit="true" allowdelete="true" allowinsert="true"
                                datasourceid=".Prerequisites" validationgroup="vgGrid">
                                <columns>
                                    <sg:sgselectorgridcolumn propertyname="CourseName" headertext="Labels_Prerequisite">
                                        <edititemtemplate>
                                            <sg:sgselector runat="server" id="sltCourse"
                                                componentname="SystemGroup.General.CourseEnrollment"
                                                entityname="Course" viewname="AllCourse" width="128px"
                                                cbselectedid="{binding PrerequisiteCourseRef}"
                                                onclientselectedindexchanged="sltCourse_selectedIndexChanged"
                                                onclientitemsrequesting="sltCourse_itemsRequesting"
                                                onitemsrequested="sltCourse_ItemsRequested">
                                                <properties>
                                                    <sg:sgselectorproperty name="Name" clientside="true" />
                                                </properties>
                                            </sg:sgselector>
                                            <sg:sgrequiredfieldvalidator runat="server" controltovalidate="sltCourse"
                                                errormessage="Messages_ChoosePrerequisite" validationgroup="vgGrid" />
                                        </edititemtemplate>
                                    </sg:sgselectorgridcolumn>
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
