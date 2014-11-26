<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Job.aspx.cs" Inherits="EMS.WebApp.Forwarding.Transaction.Job" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="EMS.WebApp" Namespace="EMS.WebApp.CustomControls" TagPrefix="cc2" %>
<%@ Register Src="~/CustomControls/AC_Port.ascx" TagName="AC_Port" TagPrefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
        ADD / EDIT JOB</div>
    <center>
        <div style="width: 100%">
            <fieldset style="width: 80%;">
                <legend>Add / Edit Job</legend>
                <asp:UpdatePanel ID="upBooking" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div>
                            <table border="0" cellpadding="1" cellspacing="0" width="100%" class="custtable">
                                <tr>
                                    <td>
                                        Job Type:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlJobType" runat="server" CssClass="dropdownlist" 
                                            TabIndex="60" AutoPostBack="true" 
                                            onselectedindexchanged="ddlJobType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <%--<asp:SqlDataSource ID="JobTypeDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                                            SelectCommand="SELECT 0 [pk_JobTypeID], '-- Select --' [JobType]
UNION
SELECT DISTINCT pk_JobTypeID, JobType FROM fwd.mstJobType"></asp:SqlDataSource>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlJobType"
                                            ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>

                                    <td>
                                        Job Scope:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlJobScope" runat="server" CssClass="dropdownlist" TabIndex="60"
                                            DataSourceID="JobScopeDs" DataTextField="JobScope" DataValueField="pk_JobScopeID">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="JobScopeDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                                            SelectCommand="SELECT 0 [pk_JobScopeID], '-- Select --' [JobScope]
UNION
SELECT pk_JobScopeID, JobScope FROM fwd.mstJobScope"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlJobScope"
                                            ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Job Date:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJobDate" runat="server" CssClass="textboxuppercase" MaxLength="15"
                                            Width="250px" TabIndex="12" AutoPostBack="True"></asp:TextBox>
                                        <cc1:CalendarExtender ID="ceBLDate" TargetControlID="txtJobDate" runat="server" Format="dd-MM-yyyy"
                                            Enabled="True" />
                                        <asp:RequiredFieldValidator ID="rfvJobDate" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="txtJobDate" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
 
                                    <td style="width: 20%;">
                                        Job No:
                                    </td>
                                    <td style="width: 28%;">
                                        <asp:TextBox ID="txtJobNo" runat="server" CssClass="textboxuppercase" MaxLength="8"
                                            Width="250px" TabIndex="13" Enabled="False" BackColor="Gray" ForeColor="White"
                                            Text="000000"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ops Controlled By:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlOpsControlled" runat="server" CssClass="dropdownlist" TabIndex="60"
                                            DataSourceID="OpsDs" DataTextField="LocName" DataValueField="pk_LocID">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="OpsDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                                            SelectCommand="SELECT 0 [pk_LocID], '-- Select --' [LocName]
UNION
SELECT pk_LocID, LocName FROM fwd.mstFLocation"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlOpsControlled"
                                            ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>

                                    <td>
                                        Doc Controlled By:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDocControlled" runat="server" CssClass="dropdownlist" TabIndex="60"
                                            DataSourceID="DocDs" DataTextField="LocName" DataValueField="pk_LocID">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="DocDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                                            SelectCommand="SELECT 0 [pk_LocID], '-- Select --' [LocName]
UNION
SELECT pk_LocID, LocName FROM fwd.mstFLocation"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDocControlled"
                                            ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Sales Controlled By:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSalesControlled" runat="server" CssClass="dropdownlist"
                                            TabIndex="60" DataSourceID="SalesDs" DataTextField="UserName" DataValueField="pk_UserID">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SalesDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                                            SelectCommand="SELECT 0 [pk_UserID], '-- Select --' [UserName]
UNION
SELECT pk_UserID, FirstName + ' ' + LastName + '(' + UserName + ')' AS UserName FROM mstUsers WHERE (fk_RoleID = 4)"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSalesControlled"
                                            ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>

                                    <td>
                                        Shipment Mode:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlShipmentMode" runat="server" CssClass="dropdownlist" TabIndex="60">
                                           <%-- DataSourceID="ShipmentDs" DataTextField="ShippingMode" DataValueField="pk_SModeID">--%>
                                        </asp:DropDownList>
<%--                                        <asp:SqlDataSource ID="ShipmentDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                                            SelectCommand="SELECT 0 [pk_SModeID], '-- Select --' [ShippingMode]
UNION
SELECT pk_SModeID, ShippingMode FROM fwd.mstShippingMode"></asp:SqlDataSource>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlShipmentMode"
                                            ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">
                                        Prime Docs:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPrimeDocs" runat="server" CssClass="dropdownlist" TabIndex="60"
                                            DataSourceID="PrimeDocsDs" DataTextField="DocName" DataValueField="pk_PrDocID">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="PrimeDocsDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                                            SelectCommand="SELECT 0 [pk_PrDocID], '-- Select --' [DocName]
UNION
SELECT pk_PrDocID, DocName FROM fwd.mstPrimeDocs"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlPrimeDocs"
                                            ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                     <td>
                                        Shipping Line / Airlines:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlShippingLine" runat="server" CssClass="dropdownlist" TabIndex="60">
<%--                                            DataSourceID="LineDs" DataTextField="LineName" DataValueField="pk_FLineID">--%>
                                        </asp:DropDownList>
                                        <%--<asp:SqlDataSource ID="LineDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                                            SelectCommand="SELECT 0 [pk_FLineID], '-- Select --' [LineName]
UNION
SELECT pk_FLineID, LineName FROM fwd.mstFLine"></asp:SqlDataSource>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlShippingLine"
                                            ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>
                                        Customer:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="dropdownlist" TabIndex="60" Width="250px">
<%--                                            DataSourceID="CustomerDs" DataTextField="CustName" DataValueField="pk_CustID">--%>
                                        </asp:DropDownList>
<%--                                        <asp:SqlDataSource ID="CustomerDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbDSRConnectionString %>"
                                            SelectCommand="SELECT 0 [pk_CustID], '-- Select --' [CustName]
UNION
SELECT pk_fwPartyID pk_CustID, PartyName CustName FROM fwd.mstParty WHERE (PartyType IN '2') ORDER BY PartyName"></asp:SqlDataSource>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlCustomer"
                                            ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 20%;">
                                        Credit Period
                                    </td>
                                    <td style="width: 28%;">
                                        <cc2:CustomTextBox ID="txtCreditDays" runat="server" CssClass="numerictextbox" TabIndex="13"
                                            Width="250px" Type="Numeric" MaxLength="5"></cc2:CustomTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">
                                        TTL 20&#39;:<span class="errormessage">*</span>
                                    </td>
                                    <td style="width: 28%;">
                                        <cc2:CustomTextBox ID="txtTTL20" runat="server" CssClass="numerictextbox" TabIndex="13"
                                            Width="250px" Type="Numeric" MaxLength="5"></cc2:CustomTextBox>
<%--                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="txtTTL20" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </td>

                                    <td style="width: 20%;">
                                        TTL 40&#39;:<span class="errormessage">*</span>
                                    </td>
                                    <td style="width: 28%;">
                                        <cc2:CustomTextBox ID="txtTTL40" runat="server" CssClass="numerictextbox" TabIndex="13"
                                            Width="250px" Type="Numeric" MaxLength="5"></cc2:CustomTextBox>
<%--                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="txtTTL40" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">
                                        Gr. Weight (Kg):<span class="errormessage">*</span>
                                    </td>
                                    <td style="width: 28%;">
                                        <cc2:CustomTextBox ID="txtGrossWeight" runat="server" CssClass="numerictextbox" TabIndex="13"
                                            Width="250px" Type="Decimal" Precision="10" Scale="3" MaxLength="14"></cc2:CustomTextBox>
                                        <asp:RequiredFieldValidator ID="rfvGrossWeight" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="txtGrossWeight" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>

                                    <td style="width: 20%;">
                                        Vol. Weight (KG):<span class="errormessage">*</span>
                                    </td>
                                    <td style="width: 28%;">
                                        <cc2:CustomTextBox ID="txtVolumeWeight" runat="server" CssClass="numerictextbox"
                                            TabIndex="13" Width="250px" Type="Decimal" Precision="10" Scale="3" MaxLength="14"></cc2:CustomTextBox>
                                        <asp:RequiredFieldValidator ID="rfvVolumeWeight" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="txtVolumeWeight" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">
                                        Weight (MT):<span class="errormessage">*</span>
                                    </td>
                                    <td style="width: 28%;">
                                        <cc2:CustomTextBox ID="txtMTWeight" runat="server" CssClass="numerictextbox" TabIndex="13"
                                            Width="250px" Type="Decimal" Precision="10" Scale="3" MaxLength="14"></cc2:CustomTextBox>
                                        <asp:RequiredFieldValidator ID="rfvMTWeight" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="txtMTWeight" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>

                                    <td style="width: 20%;">
                                        Volume (CBM):<span class="errormessage">*</span>
                                    </td>
                                    <td style="width: 28%;">
                                        <cc2:CustomTextBox ID="txtCBMVolume" runat="server" CssClass="numerictextbox" TabIndex="13"
                                            Width="250px" Type="Decimal" Precision="10" Scale="3" MaxLength="14"></cc2:CustomTextBox>
                                        <asp:RequiredFieldValidator ID="rfvCBMVolume" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="txtCBMVolume" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">
                                        Revenue Ton:<span class="errormessage">*</span>
                                    </td>
                                    <td style="width: 28%;">
                                        <cc2:CustomTextBox ID="txtRevenue" runat="server" CssClass="numerictextbox" TabIndex="13"
                                            Width="250px" Type="Decimal" Precision="10" Scale="3" MaxLength="14"></cc2:CustomTextBox>
                                        <asp:RequiredFieldValidator ID="rfvRevenue" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="txtRevenue" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>

                                   <td>
                                        Print HBL
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkPrintHBL" runat="server" AutoPostBack="true"
                                            oncheckedchanged="chkPrintHBL_CheckedChanged" />
        
                                        <asp:DropDownList ID="ddlHblFormat" runat="server" CssClass="dropdownlist" TabIndex="60"
                                        DataSourceID="LineDs" DataTextField="LineName" DataValueField="pk_FLineID">

                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="LineDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                                            SelectCommand="SELECT 0 [pk_FLineID], '-- Select --' [LineName]
UNION
SELECT pk_FLineID, LineName FROM fwd.mstFLine"></asp:SqlDataSource>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">
                                        Place of Receipt:<span class="errormessage">*</span>
                                    </td>
                                    <td style="width: 28%;">
                                        <asp:TextBox ID="txtPlaceReciept" runat="server" CssClass="textboxuppercase" MaxLength="300"
                                            Width="250px" TabIndex="13"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="txtPlaceReciept" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>

                                    <td>
                                        Port of Loading:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <uc7:ac_port id="AC_Port2" runat="server" TabIndex="13"/>
                                        <asp:Label ID="errPortOfLoading" runat="server" CssClass="errormessage"></asp:Label>
                                        <asp:HiddenField ID="hdnPortLoading" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Port of Discharge:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <uc7:ac_port id="AC_Port3" runat="server" TabIndex="13"/>
                                        <asp:Label ID="errPortOfDischarge" runat="server" CssClass="errormessage"></asp:Label>
                                        <asp:HiddenField ID="hdnPortDischarge" runat="server" />
                                    </td>

                                    <td style="width: 20%;">
                                        Place of Delivery:<span class="errormessage">*</span>
                                    </td>
                                    <td style="width: 28%;">
                                        <asp:TextBox ID="txtDelivery" runat="server" CssClass="textboxuppercase" MaxLength="300"
                                            Width="250px" TabIndex="13"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" CssClass="errormessage"
                                            ErrorMessage="This field is required" ControlToValidate="txtDelivery" ValidationGroup="Save"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                
      
                                <tr>
                                    <td>
                                        Customs Agent:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCustomsAgent" runat="server" CssClass="dropdownlist" TabIndex="60"
                                            DataSourceID="AgentDs" DataTextField="PartyName" DataValueField="pk_fwPartyID">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="AgentDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                                            SelectCommand="SELECT 0 [pk_fwPartyID], '-- Select --' [PartyName]
UNION
SELECT pk_fwPartyID, PartyName FROM fwd.mstParty WHERE (PartyType = '2')"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlCustomsAgent"
                                            ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>

                                    <td>
                                        Transporter:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTransporter" runat="server" CssClass="dropdownlist" TabIndex="60"
                                            DataSourceID="TransporterDs" DataTextField="PartyName" DataValueField="pk_fwPartyID">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="TransporterDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                                            SelectCommand="SELECT 0 [pk_fwPartyID], '-- Select --' [PartyName]
UNION
SELECT pk_fwPartyID, PartyName FROM fwd.mstParty WHERE (PartyType = '1')"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlTransporter"
                                            ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Overseas Agent:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlOverseasAgent" runat="server" CssClass="dropdownlist" TabIndex="60"
                                            DataSourceID="OverseasDs" DataTextField="PartyName" DataValueField="pk_fwPartyID">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="OverseasDs" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                                            SelectCommand="SELECT 0 [pk_fwPartyID], '-- Select --' [PartyName]
UNION
SELECT pk_fwPartyID, PartyName FROM fwd.mstParty WHERE (PartyType = '3')"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlOverseasAgent"
                                            ErrorMessage="This field is required" InitialValue="0" CssClass="errormessage"
                                            ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>

                                    <td>
                                        Cargo Source:<span class="errormessage">*</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCargoSource" runat="server" CssClass="dropdownlist" TabIndex="60">
                                            <asp:ListItem Text="Nomination" Value="N" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Self Generated" Value="S"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding-top: 10px;">
                                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" TabIndex="70"
                                            OnClick="btnSave_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnBack" runat="server" CssClass="button" TabIndex="71" OnClientClick="javascript:if(!confirm('Want to Quit?')) return false;"
                                            Text="Back" OnClick="btnBack_Click" />
                                        <br />
                                        <asp:Label ID="lblError" runat="server" CssClass="errormessage"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
            <asp:UpdateProgress ID="uProgressBooking" runat="server" AssociatedUpdatePanelID="upBooking">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="image">
                            <img src="../../Images/PleaseWait.gif" alt="" /></div>
                        <div id="text">
                            Please Wait...</div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </center>
</asp:Content>
