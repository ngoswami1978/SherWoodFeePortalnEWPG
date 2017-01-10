<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="SherWoodFeePortal._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div class="form-horizontal">
    <fieldset>
        <legend>Child Details</legend>
        <br />
        <div class="form-group">
            <label class="col-sm-2 control-label" for="card-holder-name">
                Your Email</label>
            <div class="col-sm-4">
                <%-- <input maxlength="100" type="email" required="required" class="form-control" placeholder="Enter Email" />--%>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter Email"
                    required="required"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label" for="card-holder-name">
                Roll No</label>
            <div class="col-sm-4">
                <input maxlength="100" type="text" required="required" class="form-control" placeholder="Roll No" />
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label" for="card-holder-name">
                Class
            </label>
            <div class="col-sm-4">
                <input maxlength="100" type="text" required="required" class="form-control" placeholder="Class" />
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label" for="card-holder-name">
                Father Name
            </label>
            <div class="col-sm-4">
                <input maxlength="100" type="text" required="required" class="form-control" placeholder="Father Name" />
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-2 control-label" for="card-holder-name">
                Amount
            </label>
            <div class="col-sm-4">
                <input maxlength="100" type="text" required="required" class="form-control" placeholder="Amount" />
            </div>
        </div>
    </fieldset>

    <fieldset>
        <legend>Enter the Child Details</legend>
        <br />
        <asp:Button ID="btnSubmit" class="btn btn-primary nextBtn btn-lg pull-center" runat="server" Text="Submit" />
        </fieldset>

        </div>
</asp:Content>
