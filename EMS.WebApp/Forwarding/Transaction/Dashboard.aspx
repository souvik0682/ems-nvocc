<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="EMS.WebApp.Farwarding.Transaction.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <script type="text/javascript">
        function popWin(id) {
            alert("test");
            window.open("FileUpload.aspx?Id=" + id, "", "height=300px,toolbar=0,menubar=0,resizable=1,status=1,scrollbars=1"); return false;
        }
        function update(val) {
            alert("File: " + val + " successfully uploaded!");
        }
    </script>
    <!-- Le styles -->
      <link rel="stylesheet" href="css/bootstrap.min.css">
      <link rel="stylesheet" href="css/bootstrap-theme.min.css">
      <link rel="stylesheet" href="css/style.css">
      <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
      <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
      <![endif]-->
      <style type="text/css">
         .bs-example{
            margin: 20px;
         }
         h3{
	        margin-top: 10px;
        }
        .panel-heading {
	        padding: 8px 15px;
        }
        .panel-default > .panel-heading
        {
            background-color: #CDCDCD;
        }
        .btn_close
        {
            padding-left: 8px;
            padding-right: 8px;
        }
        .table {
        width: 100%;
        max-width: 100%;
        margin-bottom: 10px;
        }
        .table>thead>tr>th, .table>tbody>tr>th, .table>tfoot>tr>th, .table>thead>tr>td, .table>tbody>tr>td, .table>tfoot>tr>td {
        padding: 4px;
        line-height: 1.42857143;
        vertical-align: top;
        border-top: 1px solid #ddd;
        }

      </style>
      <!--[if lt IE 9]>
      <script type="text/javascript" src="http://info.template-help.com/files/ie6_warning/ie6_script_other.js"></script>
      <script type="text/javascript" src="js/html5.js"></script>
      <![endif]-->
    <div id="headercaption">
        FORWARDING DASHBOARD</div>
    <center>
        <div style="width: 100%">
            <fieldset style="width: 80%;">
                <!--legend>Dashboard</legend-->
                <asp:UpdatePanel ID="upBooking" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="maincontainer">
                            <!--start body-->
                            <div class="body">
                                <h3>
                                    Job Details</h3>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <table class="table table-bordered">
                                            <tr>
                                                <td>
                                                    <strong> Job Date </strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblJobDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Job Number </strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblJobNumber" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Cargo Source </strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCargoSource" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Ops. Controlled by </strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblOps" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Doc. Controlled by </strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDoc" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Sales Controlled by </strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSales" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Approver </strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblApprover" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Closed By </strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblClosed" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-sm-4">
                                        <table class="table table-bordered">
                                            <tr>
                                                <td>
                                                    <strong>Job Type</strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblJobType" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Job Scope</strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblJobScope" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Shipping Mode</strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblShipping" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Prime Docs</strong>
                                                </td>
                                                <td>
                                                    <asp:HiddenField ID="hdnCustID" runat="server"/>
                                                    <asp:Label ID="lblPrimeDocs" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table class="table" style="margin-bottom: 0px;">
                                                        <tr>
                                                            <td style="border: 0px;">
                                                                <strong>TTL 20'</strong>
                                                            </td>
                                                            <td style="border: 0px;">
                                                                <strong>TTL 40'</strong>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblTTL20" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTTL40" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table class="table" style="margin-bottom: 0px;">
                                                        <tr>
                                                            <td style="border: 0px;">
                                                                <strong>Weight (Kgs.)</strong>
                                                            </td>
                                                            <td style="border: 0px;">
                                                                <strong>Revenue Ton</strong>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblWeight" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRevenue" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-sm-4">
                                        <table class="table table-bordere" style="width:auto;"> 
                                            <tr>
                                                <td>
                                                    <strong>Place of Recv.</strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPlaceReceive" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <strong>POL</strong>
                                                </td>
                                                <td style="width:30%;">
                                                    <asp:Label ID="lblPOL" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Place of Delv.</strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPlaceDelivery" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <strong>POD</strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPOD" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Carrier</strong>
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="lblCarrier" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Customer</strong>
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="lblCustomer" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Customs Ag</strong>
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="lblCustomerAgent" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Transporter</strong>
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="lblTransporter" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Overseas Ag</strong>
                                                </td>
                                                <td colspan="3">
                                                    <asp:Label ID="lblOverseas" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <h3>
                                    Job Summary</h3>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <table class="table table-bordered">
                                            <tr>
                                                <td>
                                                    <strong>Estimate Payable (INR)</strong>
                                                </td>
                                                <td width="35%">
                                                    <asp:Label ID="lblTotalEstimatePayable" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Paid (INR)</strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTotalPaid" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-sm-4">
                                        <table class="table table-bordered">
                                            <tr>
                                                <td>
                                                    <strong>Estimate Recieveable (INR)</strong>
                                                </td>
                                                <td width="35%">
                                                    <asp:Label ID="lblTotalEstimateReceiveable" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Received (INR)</strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTotalReceived" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-sm-3">
                                        <table class="table table-bordered">
                                            <tr>
                                                <td>
                                                    <strong>Projected Gross Profit</strong>
                                                </td>
                                                <td width="35%">
                                                    <asp:Label ID="lblProjectedGrossProfit" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <strong>Achieved Gross Profit</strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblArchievedGrossProfit" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-sm-1" style="padding-left:0px; padding-right:0px;">
                                        <table class="tabl e table-bor dered">
                                            <tr>
                                                <td style="padding-bottom:6px;">
                                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-primary btn_close btn-xs col-sm-12"
                                                        OnClick="btnApprove_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnCloseJob" runat="server" Text="Close Job" CssClass="btn btn-primary btn_close btn-xs col-sm-12"
                                                        OnClick="btnCloseJob_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="bs-exa mple">
                                    <div class="panel-group" id="accordion">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                            <div class="row">
                                                <div class="col-sm-8">
                                                 <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">Estimate Payable
                                                        Particulars</a>
                                                </h4>
                                               </div>                
                                                <div class="col-sm-4 text-right">
                                                    <asp:Button ID="Button1" runat="server" Text="Advance Payment" CssClass="btn btn-primary"
                                                        OnClick="btnAdvPayment_Click" />
                                                    <asp:Button ID="Button2" runat="server" Text="Add Payable" CssClass="btn btn-primary"
                                                        OnClick="btnAddPayable_Click" />
                                                  </div>
                                               </div>
                                                
                                            </div>
                                            <div id="collapseTwo" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <!--div class="row">
                                                        <div class="col-sm-4 col-sm-offset-8 text-right">
                                                            <asp:Button ID="btnAdvPayment" runat="server" Text="Advance Payment" CssClass="btn btn-primary"
                                                                OnClick="btnAdvPayment_Click" />
                                                            <asp:Button ID="btnAddPayable" runat="server" Text="Add Payable" CssClass="btn btn-primary"
                                                                OnClick="btnAddPayable_Click" />
                                                        </div>
                                                    </div-->
                                                    <div class="row" style="overflow-x:auto;">
                                                        <asp:GridView ID="gvEstimatePayable" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                            BorderStyle="None" BorderWidth="0" Width="100%" OnRowDataBound="gvEstimatePayable_RowDataBound"
                                                            OnRowCommand="gvEstimatePayable_RowCommand">
                                                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                            <EmptyDataTemplate>
                                                                No Record(s) Found</EmptyDataTemplate>
                                                            <Columns>
 <%--                                                               <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblUnitType" runat="server" Text="Unit Type"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblBillFrom" runat="server" Text="Bill From"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblTotalUnit" runat="server" Text="Total Units"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                
<%--                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblCurrency" runat="server" Text="Currency"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblChargeAmt" runat="server" Text="Charge Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblRoe" runat="server" Text="ROE"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblPaymentBy" runat="server" Text="Payment By"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblAmt" runat="server" Text="Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnBillReceipt" runat="server" CommandName="BillReceipt" ImageUrl="~/Images/status.jpg"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnUpload" runat="server" CommandName="Upload" ImageUrl="~/Images/add.jpeg"
                                                                            Height="16" Width="16"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png"
                                                                            Height="16" Width="16"  OnClientClick="javascript:return confirm('Are you sure about delete?');"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                               <div class="row">
                                                <div class="col-sm-8">
                                                 <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseThree">Estimate Receivable
                                                        Particulars</a>
                                                </h4>
                                               </div>                
                                                <div class="col-sm-4 text-right">
                                                    <asp:Button ID="Button3" runat="server" Text="Advance Receipt" CssClass="btn btn-primary"
                                                                OnClick="btnAdvanceReceipt_Click" />
                                                            <asp:Button ID="Button4" runat="server" Text="Add Recovery" CssClass="btn btn-primary"
                                                                OnClick="btnAddRecovery_Click" />
                                                  </div>
                                               </div>
                                                
                                            </div>
                                            <div id="collapseThree" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <!--div class="row">
                                                        <div class="col-sm-4 col-sm-offset-8 text-right">
                                                            <asp:Button ID="btnAdvanceReceipt" runat="server" Text="Advance Receipt" CssClass="btn btn-primary"
                                                                OnClick="btnAdvanceReceipt_Click" />
                                                            <asp:Button ID="btnAddRecovery" runat="server" Text="Add Recovery" CssClass="btn btn-primary"
                                                                OnClick="btnAddRecovery_Click" />
                                                        </div>
                                                    </div-->
                                                    <div class="row" style="overflow-x:auto;">
                                                        <asp:GridView ID="gvEstimateReceivable" runat="server" AutoGenerateColumns="false"
                                                            AllowPaging="false" BorderStyle="None" BorderWidth="0" Width="100%" OnRowDataBound="gvEstimateReceivable_RowDataBound"
                                                            OnRowCommand="gvEstimateReceivable_RowCommand">
                                                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                            <EmptyDataTemplate>
                                                                No Record(s) Found</EmptyDataTemplate>
                                                            <Columns>
<%--                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblUnitType" runat="server" Text="Unit Type"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblBillFrom" runat="server" Text="Bill From"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblTotalUnit" runat="server" Text="Total Units"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>

<%--                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblCurrency" runat="server" Text="Currency"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblChargeAmt" runat="server" Text="Charge Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblRoe" runat="server" Text="ROE"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblPaymentBy" runat="server" Text="Payment By"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblAmt" runat="server" Text="Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnGenInv" runat="server" CommandName="GenInv" ImageUrl="~/Images/status.jpg"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnUpload" runat="server" CommandName="Upload" ImageUrl="~/Images/add.jpeg"
                                                                            Height="16" Width="16"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png"
                                                                            Height="16" Width="16"  OnClientClick="javascript:return confirm('Are you sure about delete?');"/>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                            <div class="row">
                                                <div class="col-sm-8">
                                                 <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFour">Creditors Invoice</a>
                                                </h4>
                                               </div>                
                                                <div class="col-sm-4 text-right">
                                                    <asp:Button ID="Button5" runat="server" Text="Add Invoice" CssClass="btn btn-primary"
                                                                OnClick="btnAddInvoiceCred_Click" />
                                                  </div>
                                               </div>
                                                
                                            </div>
                                            <div id="collapseFour" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <!--div class="row">
                                                        <div class="col-sm-4 col-sm-offset-8 text-right">
                                                            <asp:Button ID="btnAddInvoiceCred" runat="server" Text="Add Invoice" CssClass="btn btn-primary"
                                                                OnClick="btnAddInvoiceCred_Click" />
                                                        </div>
                                                    </div-->
                                                    <div class="row" style="overflow-x:auto;">
                                                        <asp:GridView ID="gvCreditors" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                            BorderStyle="None" BorderWidth="0" Width="100%" OnRowDataBound="gvCreditors_RowDataBound"
                                                            OnRowCommand="gvCreditors_RowCommand">
                                                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                            <EmptyDataTemplate>
                                                                No Record(s) Found</EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblPartyName" runat="server" Text="Party Name"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInvoiceType" runat="server" Text="Invoice Type"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInvoiceAmt" runat="server" Text="Invoice Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblROE" runat="server" Text="ROE"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblINRAmt" runat="server" Text="INR Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png"
                                                                            Height="16" Width="16" OnClientClick="javascript:return confirm('Are you sure about delete?');" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnPayment" runat="server" CommandName="Payment" ImageUrl="~/Images/edit.png"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                            <div class="row">
                                                <div class="col-sm-8">
                                                 <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFive">Debtors Invoice</a>
                                                </h4>
                                               </div>                
                                                <div class="col-sm-4 text-right">
                                                    <asp:Button ID="Button6" runat="server" Text="Add Invoice" CssClass="btn btn-primary"
                                                                OnClick="btnAddInvoiceDebt_Click" />
                                                  </div>
                                               </div>
                                                
                                            </div>
                                            <div id="collapseFive" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <!--div class="row">
                                                        <div class="col-sm-4 col-sm-offset-8 text-right">
                                                            <asp:Button ID="btnAddInvoiceDebt" runat="server" Text="Add Invoice" CssClass="btn btn-primary"
                                                                OnClick="btnAddInvoiceDebt_Click" />
                                                        </div>
                                                    </div-->
                                                    <div class="row" style="overflow-x:auto;">
                                                        <asp:GridView ID="gvDebtors" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                                            BorderStyle="None" BorderWidth="0" Width="100%" OnRowDataBound="gvDebtors_RowDataBound"
                                                            OnRowCommand="gvDebtors_RowCommand">
                                                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                                            <EmptyDataTemplate>
                                                                No Record(s) Found</EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInvoiceType" runat="server" Text="Invoice Type"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkInvoice" runat="server" CommandName="Invoice" ImageUrl="~/Images/remove.png"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInvoiceAmt" runat="server" Text="Invoice Amt"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblReceivedAmt" runat="server" Text="Received Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblCRNAmt" runat="server" Text="CRN Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="10%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblBalanceAmt" runat="server" Text="Balance Amount"></asp:Label>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnPrint" runat="server" CommandName="Print" ImageUrl="~/Images/remove.png"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnAddMR" runat="server" CommandName="AddMR" ImageUrl="~/Images/edit.png"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle CssClass="gridviewheader" />
                                                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnAddCRN" runat="server" CommandName="AddCRN" ImageUrl="~/Images/edit.png"
                                                                            Height="16" Width="16" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end footer-->
                            <!-- Le javascript -->    
      <script src="js/jquery.min.js"></script>
      <script src="js/bootstrap.min.js"></script>
      <script>
          $('.carousel').carousel({
              interval: 5000
          })
      </script>
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


