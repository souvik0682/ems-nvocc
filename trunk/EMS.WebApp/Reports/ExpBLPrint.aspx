<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpBLPrint.aspx.cs" MasterPageFile="~/Site.Master"
    Inherits="EMS.WebApp.Reports.ExpBLPrint" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Import Namespace="EMS.Utilities" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Print() {

            var mainContentPrint = $('#mainContentPrint').html();
            $(mainContentPrint).filter('.bgimg').each(function () {
                $(this).attr('style', 'height: 1772px; width: 1159px;');
            });
            var newhtml = '<html><head><title>popup</title></head><body >' + mainContentPrint + '</body></html>';
            var newWin = window.open('', 'thePopup', 'width=1159,height=1772');
            newWin.document.write(newhtml);
            newWin.window.location.reload();    // this is the secret ingredient
            newWin.focus();                     // not sure if this line is necessary
            newWin.print();
            newWin.document.close();
            return false;
        }
    </script>
    <style type="text/css">
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <center>
        <fieldset style="padding: 5px; width: 55%">
            <table style="width: 100%" cellpadding="1" cellspacing="0">
                <tr id="main">
                    <td align="left" style="width: 15%">
                        Location:<span class="errormessage">*</span>
                    </td>
                    <td align="left" style="width: 35%">
                        <asp:DropDownList ID="ddlLine" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLine_SelectedIndexChanged"
                            Width="172px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlLine" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        Line / NVOCC::<span class="errormessage" style="width: 10%">*</span>
                    </td>
                    <td align="left" style="width: 35%">
                        <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" Width="70px">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlLocation" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="trCar1">
                    <td>
                        BL No.:<span class="errormessage">*</span>
                    </td>
                    <td align="left" colspan="3">
                        <asp:DropDownList ID="ddlBlNo" runat="server" Width="172px">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvLocation" runat="server" CssClass="errormessage"
                            ControlToValidate="ddlBlNo" InitialValue="0" ValidationGroup="Report" Display="Dynamic"
                            ErrorMessage="[Required]"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="left" style="padding: 5px 5px 5px 0">
                        <asp:Button ID="btnReport" runat="server" Text="View Report" ValidationGroup="Report"
                            OnClick="btnReport_Click" />
                        <asp:Button ID="btnPrint" runat="server" Visible="true" Text="Print" ValidationGroup="Report" OnClientClick="return Print()" />
                    </td>
                </tr>
            </table>
        </fieldset>
         <fieldset>
         <div style="padding-left:5px;width:100%;"> 
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%" EnableExternalImages="True" EnableHyperlinks="True"></rsweb:ReportViewer>        
        </div>    
        </fieldset>
        <fieldset>
            <% if (Model != null && Model.Count>0) %>
            <%{ %>
            <%btnPrint.Visible = true; %>
            <div id="mainContentPrint">
            <%foreach (var model in Model)%>
            <%{ %>

            <div class="bgimg" style="height: 1772px; width: 1163px;  background-image: url(../Styles/Ufal.png);padding-top:10px">
                <div style="margin-top: 60px; margin-left: 110px;">
                    <div id="ShipperName" style="height: 110px; width: 480px;  float: left; padding: 20px 10px 10px 10px">
                        <%=  model.BLPrint.ShipperName %>
                        <br />
                        <%=   model.BLPrint.Shipper%>
                    </div>
                    <div style="height: 140px; width: 500px; float: left">
                        <div style="height: 30px; width: 480px;  float: left; padding: 20px 10px 10px 10px;
                            text-align: right">
                            &nbsp;
                        </div>
                        <div style="height: 30px; width: 480px; float: left; padding: 0px 70px 10px 10px;
                            text-align: right">
                            <%=  model.BLPrint.ExpBLNo%>
                        </div>
                    </div>
                </div>
                <div style="clear: both">
                </div>
                <div id="ConsigneeName" style="height: 110px; width: 480px;  margin-top: 0px;
                    margin-left: 110px; padding: 30px 10px 10px 10px">
                    <%=  model.BLPrint.ConsigneeName%>
                    <br />
                    <%=  model.BLPrint.Consignee%>
                </div>
                <div id="NotifyName" style="height: 100px; width: 480px;  margin-top: 0px;
                    margin-left: 110px; padding: 30px 10px 10px 10px;">
                    <%=  model.BLPrint.NotifyName%>
                    <br />
                    <%=  model.BLPrint.Notify%>
                </div>
                <div style="margin-left: 110px;">
                    <div style="height: 10px; width: 230px;  margin-top: 0px;
                        float: left; padding: 25px 10px 10px 10px;">
                        &nbsp;
                    </div>
                    <div style="height: 10px; width: 230px;  margin-top: 0px;
                        float: left; padding: 25px 10px 10px 10px;">
                        <%=  model.BLPrint.PlaceOfReceipt%>
                    </div>
                </div>
                <div style="margin-left: 110px; clear: both">
                    <div style="height: 10px; width: 230px;  margin-top: 0px;
                        float: left; padding: 30px 10px 10px 10px;">
                        <%=  model.BLPrint.VesselName%>
                        /<%=  model.BLPrint.VoyageNo%>
                    </div>
                    <div style="height: 10px; width: 230px;  margin-top: 0px;
                        float: left; padding: 30px 10px 10px 10px;">
                        <%=  model.BLPrint.PlaceofLoading%>
                    </div>
                </div>
                <div style="margin-left: 110px; clear: both">
                    <div style="height: 10px; width: 230px;  margin-top: 0px;
                        float: left; padding: 30px 10px 10px 10px;">
                        <%=  model.BLPrint.PlaceofDischarge%>
                    </div>
                    <div style="height: 10px; width: 230px;  margin-top: 0px;
                        float: left; padding: 30px 10px 10px 10px;">
                        <%=  model.BLPrint.FinalDelivery%>
                    </div>
                </div>
                <div style="height: 400px; width: 1000px;  margin-top: 90px;
                    margin-left: 110px">
                    <table style="width: 100%; margin-top: 20px"  cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 20%; padding: 5px 3px 3px 3px; height: 150px;vertical-align:top">
                                <%=   model.BLPrint.MarksNumbers%>
                            </td>
                            <td style="width: 54%; padding: 5px 3px 3px 3px; height: 150px;vertical-align:top">
                                <%=  model.BLPrint.GoodsDescription%>
                            </td>
                            <td style="width: 13%; padding: 5px 3px 3px 3px; height: 150px;vertical-align:top">
                                <%=  model.BLPrint.GRWT%>
                                <br />
                                NET WEIGHT <br />
                                <%=  model.BLPrint.NetWt%>
                            </td>
                            <td style="width: 13%; padding: 5px 3px 3px 3px; height: 150px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%; padding: 5px 3px 3px 3px;">
                                <%=   model.BLPrint.ShipmentType%>
                                <%=   model.BLPrint.ShipmentMode%>
                            </td>
                            <td style="width: 54%; padding: 5px 3px 3px 3px;">
                                &nbsp;
                            </td>
                            <td style="width: 13%; padding: 5px 3px 3px 3px;">
                                &nbsp;
                            </td>
                            <td style="width: 13%; padding: 5px 3px 3px 3px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <% if (model.ItemDetails != null && model.ItemDetails.Count>0)%>
                    <%{ %>
                   <%=GetTable(model.ItemDetails)%>
                       
                    <%} %>
                </div>
                <div style="height: 295px; width: 480px;  margin-top: 30px;
                    margin-left: 110px; padding: 25px 10px 10px 10px;">
                    <%=  model.BLPrint.AgentName%>
                    <br />
                    <%=  model.BLPrint.AgentAddress%>
                    <br />
                    <%=  model.BLPrint.FreightPrePayToPay%>
                </div>
                <div style="height: 30px; width: 480px;  margin-top: 10px;
                    margin-left: 110px; padding: 20px 10px 10px 10px;">
                    <%=  model.BLPrint.BLClause%>
                </div>
                <div style="margin-left: 110px">
                    <table style="width: 100%; margin-top: 20px"  cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 35%">
                                &nbsp;
                            </td>
                            <td style="width: 25%; padding-left: 10px;">
                                <%=  model.BLPrint.FreightPayableAt%>
                            </td>
                            <td style="padding-left: 10px;">
                                <%=  model.BLPrint.LocationName%>
                                &nbsp; &nbsp;
                                <%=  model.BLPrint.ExpBLDate%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 150px; padding-left: 10px;" valign="top">
                                &nbsp;
                            </td>
                            <td style="height: 150px; padding-left: 10px;padding-top: 20px;" valign="top">
                                <%=  model.BLPrint.NoofBLs%>
                            </td>
                            <td style="height: 150px; padding-left: 10px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <% if (StartCount == 5)%>
                    <%{ %>
                       <div style="width: 1000px;  margin-top: 60px;
                    margin-left: 110px">
                    ATTACHED SHEET AGAINST B/L NO: <%= model.BLPrint.ExpBLNo%> DATED  <%= model.BLPrint.ExpBLDate%>
                    LIST OF ITEMS
                   <%=GetTable(model.ItemDetails)%>    </div>                   
                    <%} %>
            <%} %>
              </div>
            <%} %>
           
        </fieldset>
    </center>
</asp:Content>
